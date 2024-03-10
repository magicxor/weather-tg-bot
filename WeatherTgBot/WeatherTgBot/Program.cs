using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using WeatherTgBot.Enums;
using WeatherTgBot.Exceptions;
using WeatherTgBot.Extensions;
using WeatherTgBot.Models;
using WeatherTgBot.Services;
using Telegram.Bot;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace WeatherTgBot;

public static class Program
{
    private static readonly LoggingConfiguration LoggingConfiguration = new XmlLoggingConfiguration("nlog.config");
    private static readonly IEnumerable<TimeSpan> Delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);
    private static readonly IAsyncPolicy<HttpResponseMessage> HttpRetryPolicy = HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(Delay);

    public static void Main(string[] args)
    {
        // NLog: setup the logger first to catch all errors
        LogManager.Configuration = LoggingConfiguration;
        try
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((_, config) =>
                {
                    config
                        .AddEnvironmentVariables("WEATHERBOT_")
                        .AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                    loggingBuilder.AddNLog(LoggingConfiguration);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddOptions<WeatherTgBotOptions>()
                        .Bind(hostContext.Configuration.GetSection(nameof(OptionSections.WeatherTgBot)))
                        .ValidateDataAnnotations()
                        .ValidateOnStart();

                    services.AddHttpClient();
                    services.AddHttpClient(nameof(HttpClientTypes.WaitAndRetryOnTransientHttpError))
                        .AddPolicyHandler(HttpRetryPolicy);

                    var telegramBotApiKey = hostContext.Configuration.GetTelegramBotApiKey()
                                            ?? throw new ServiceException("Telegram bot API key is missing");
                    services.AddScoped<ITelegramBotClient, TelegramBotClient>(s => new TelegramBotClient(telegramBotApiKey,
                        s.GetRequiredService<IHttpClientFactory>()
                            .CreateClient(nameof(HttpClientTypes.WaitAndRetryOnTransientHttpError))));

                    services.AddScoped<WeatherProvider>();
                    services.AddScoped<TelegramBotService>();
                    services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
        catch (Exception ex)
        {
            // NLog: catch setup errors
            LogManager.GetCurrentClassLogger().Error(ex, "Stopped program because of exception");
            throw;
        }
        finally
        {
            // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
            LogManager.Shutdown();
        }
    }
}
