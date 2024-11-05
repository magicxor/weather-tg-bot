using System.Text.Json.Serialization;

namespace WeatherTgBot.Models;

public sealed class Daily
{
    [JsonPropertyName("time")]
    public List<DateTime>? Time { get; set; }

    [JsonPropertyName("temperature_2m_max")]
    public List<double>? Temperature2MMax { get; set; }

    [JsonPropertyName("temperature_2m_min")]
    public List<double>? Temperature2MMin { get; set; }

    [JsonPropertyName("precipitation_sum")]
    public List<double>? PrecipitationSum { get; set; }

    [JsonPropertyName("precipitation_probability_max")]
    public List<int>? PrecipitationProbabilityMax { get; set; }

    [JsonPropertyName("shortwave_radiation_sum")]
    public List<double>? ShortwaveRadiationSum { get; set; }
}
