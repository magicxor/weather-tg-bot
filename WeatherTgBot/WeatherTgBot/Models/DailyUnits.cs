using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WeatherTgBot.Models;

public class DailyUnits
{
    [JsonProperty("time")]
    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonProperty("temperature_2m_max")]
    [JsonPropertyName("temperature_2m_max")]
    public string? Temperature2MMax { get; set; }

    [JsonProperty("temperature_2m_min")]
    [JsonPropertyName("temperature_2m_min")]
    public string? Temperature2MMin { get; set; }

    [JsonProperty("precipitation_sum")]
    [JsonPropertyName("precipitation_sum")]
    public string? PrecipitationSum { get; set; }

    [JsonProperty("precipitation_probability_max")]
    [JsonPropertyName("precipitation_probability_max")]
    public string? PrecipitationProbabilityMax { get; set; }

    [JsonProperty("shortwave_radiation_sum")]
    [JsonPropertyName("shortwave_radiation_sum")]
    public string? ShortwaveRadiationSum { get; set; }
}
