using System.Globalization;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using WeatherTgBot.Extensions;

namespace WeatherTgBot.Services;

public sealed class TelegramBotService
{
    private static readonly ReceiverOptions ReceiverOptions = new()
    {
        AllowedUpdates = [UpdateType.InlineQuery],
    };

    private readonly ILogger<TelegramBotService> _logger;
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly WeatherProvider _weatherProvider;

    public TelegramBotService(ILogger<TelegramBotService> logger,
        ITelegramBotClient telegramBotClient,
        WeatherProvider weatherProvider)
    {
        _logger = logger;
        _telegramBotClient = telegramBotClient;
        _weatherProvider = weatherProvider;
    }

    private Task HandleUpdateAsync(ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        _logger.LogDebug("Received update with type={Update}", update.Type.ToString());

        // ReSharper disable once AsyncVoidLambda
        ThreadPool.QueueUserWorkItem(async _ => await HandleUpdateFunctionAsync(botClient, update, cancellationToken));

        return Task.CompletedTask;
    }

    private async Task HandleUpdateFunctionAsync(ITelegramBotClient botClient,
        Update update,
        CancellationToken cancellationToken)
    {
        try
        {
            if (update.InlineQuery is { } inlineQuery)
            {
                _logger.LogInformation("Inline query received. Query (length: {QueryLength}): {Query}",
                    inlineQuery.Query.Length,
                    inlineQuery.Query);

                var cities = await _weatherProvider.GetWeatherInCitiesAsync(inlineQuery.Query.Trim(), 10, cancellationToken);
                var inlineResults = cities
                    .Select((cf, i) => new InlineQueryResultArticle(
                        $"{i}_{cf.City.Coordinates}_{DateTime.UtcNow.ToString("yyyy-MM-dd_HH", CultureInfo.InvariantCulture)}",
                        cf.GetShortInfo(),
                        new InputTextMessageContent(cf.GetCityForecastString())))
                    .ToList();

                await botClient.AnswerInlineQueryAsync(inlineQuery.Id, inlineResults, 3600, false, cancellationToken: cancellationToken);
                _logger.LogInformation("Inline query answered. Sent {Count} results", inlineResults.Count);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while handling update");
        }
    }

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is ApiRequestException apiRequestException)
        {
            _logger.LogError(exception,
                @"Telegram API Error. ErrorCode={ErrorCode}, RetryAfter={RetryAfter}, MigrateToChatId={MigrateToChatId}",
                apiRequestException.ErrorCode,
                apiRequestException.Parameters?.RetryAfter,
                apiRequestException.Parameters?.MigrateToChatId);
        }
        else
        {
            _logger.LogError(exception, @"Telegram API Error");
        }

        return Task.CompletedTask;
    }

    public void Start(CancellationToken cancellationToken)
    {
        _telegramBotClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: ReceiverOptions,
            cancellationToken: cancellationToken
        );
    }
}
