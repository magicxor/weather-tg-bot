using System.Text.Json.Serialization;

namespace WeatherTgBot.Models;

public sealed class Forecast
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("generationtime_ms")]
    public double GenerationtimeMs { get; set; }

    [JsonPropertyName("utc_offset_seconds")]
    public int UtcOffsetSeconds { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("timezone_abbreviation")]
    public string? TimezoneAbbreviation { get; set; }

    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    [JsonPropertyName("current_units")]
    public CurrentUnits? CurrentUnits { get; set; }

    [JsonPropertyName("current")]
    public Current? Current { get; set; }

    [JsonPropertyName("daily_units")]
    public DailyUnits? DailyUnits { get; set; }

    [JsonPropertyName("daily")]
    public Daily? Daily { get; set; }
}
