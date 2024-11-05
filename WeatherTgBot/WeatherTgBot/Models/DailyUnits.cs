using System.Text.Json.Serialization;

namespace WeatherTgBot.Models;

public sealed class DailyUnits
{
    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonPropertyName("temperature_2m_max")]
    public string? Temperature2MMax { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public string? Temperature2MMin { get; set; }

    [JsonPropertyName("precipitation_sum")]
    public string? PrecipitationSum { get; set; }

    [JsonPropertyName("precipitation_probability_max")]
    public string? PrecipitationProbabilityMax { get; set; }

    [JsonPropertyName("shortwave_radiation_sum")]
    public string? ShortwaveRadiationSum { get; set; }
}
