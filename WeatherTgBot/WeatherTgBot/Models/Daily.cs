using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WeatherTgBot.Models;

public sealed class Daily
{
    [JsonProperty("time")]
    [JsonPropertyName("time")]
    public List<DateTime>? Time { get; set; }

    [JsonProperty("temperature_2m_max")]
    [JsonPropertyName("temperature_2m_max")]
    public List<double>? Temperature2MMax { get; set; }

    [JsonProperty("temperature_2m_min")]
    [JsonPropertyName("temperature_2m_min")]
    public List<double>? Temperature2MMin { get; set; }

    [JsonProperty("precipitation_sum")]
    [JsonPropertyName("precipitation_sum")]
    public List<double>? PrecipitationSum { get; set; }

    [JsonProperty("precipitation_probability_max")]
    [JsonPropertyName("precipitation_probability_max")]
    public List<int>? PrecipitationProbabilityMax { get; set; }

    [JsonProperty("shortwave_radiation_sum")]
    [JsonPropertyName("shortwave_radiation_sum")]
    public List<double>? ShortwaveRadiationSum { get; set; }
}
