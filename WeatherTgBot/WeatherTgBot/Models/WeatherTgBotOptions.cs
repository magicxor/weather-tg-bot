using System.ComponentModel.DataAnnotations;

namespace WeatherTgBot.Models;

public sealed class WeatherTgBotOptions
{
    [Required]
    [RegularExpression(@".*:.*")]
    public required string TelegramBotApiKey { get; init; }

    [Required]
    [Url]
    public required string OpenMeteoApiUrl { get; init; }
}
