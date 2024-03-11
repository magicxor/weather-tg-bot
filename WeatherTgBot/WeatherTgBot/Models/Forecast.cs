using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WeatherTgBot.Models;

public sealed class Forecast
{
    [JsonProperty("latitude")]
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonProperty("longitude")]
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonProperty("generationtime_ms")]
    [JsonPropertyName("generationtime_ms")]
    public double GenerationtimeMs { get; set; }

    [JsonProperty("utc_offset_seconds")]
    [JsonPropertyName("utc_offset_seconds")]
    public int UtcOffsetSeconds { get; set; }

    [JsonProperty("timezone")]
    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonProperty("timezone_abbreviation")]
    [JsonPropertyName("timezone_abbreviation")]
    public string? TimezoneAbbreviation { get; set; }

    [JsonProperty("elevation")]
    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    [JsonProperty("current_units")]
    [JsonPropertyName("current_units")]
    public CurrentUnits? CurrentUnits { get; set; }

    [JsonProperty("current")]
    [JsonPropertyName("current")]
    public Current? Current { get; set; }

    [JsonProperty("daily_units")]
    [JsonPropertyName("daily_units")]
    public DailyUnits? DailyUnits { get; set; }

    [JsonProperty("daily")]
    [JsonPropertyName("daily")]
    public Daily? Daily { get; set; }
}
