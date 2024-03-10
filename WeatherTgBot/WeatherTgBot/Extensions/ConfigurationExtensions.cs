using Microsoft.Extensions.Configuration;
using WeatherTgBot.Enums;
using WeatherTgBot.Models;

namespace WeatherTgBot.Extensions;

public static class ConfigurationExtensions
{
    public static string? GetTelegramBotApiKey(this IConfiguration configuration)
    {
        return configuration.GetSection(nameof(OptionSections.WeatherTgBot)).GetValue<string>(nameof(WeatherTgBotOptions.TelegramBotApiKey));
    }
}
