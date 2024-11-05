using System.Text.Json.Serialization;

namespace WeatherTgBot.Models;

public sealed class CurrentUnits
{
    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonPropertyName("interval")]
    public string? Interval { get; set; }

    [JsonPropertyName("temperature_2m")]
    public string? Temperature2M { get; set; }

    [JsonPropertyName("relative_humidity_2m")]
    public string? RelativeHumidity2M { get; set; }

    [JsonPropertyName("apparent_temperature")]
    public string? ApparentTemperature { get; set; }

    [JsonPropertyName("is_day")]
    public string? IsDay { get; set; }

    [JsonPropertyName("precipitation")]
    public string? Precipitation { get; set; }

    [JsonPropertyName("rain")]
    public string? Rain { get; set; }

    [JsonPropertyName("showers")]
    public string? Showers { get; set; }

    [JsonPropertyName("snowfall")]
    public string? Snowfall { get; set; }

    [JsonPropertyName("weather_code")]
    public string? WeatherCode { get; set; }

    [JsonPropertyName("cloud_cover")]
    public string? CloudCover { get; set; }

    [JsonPropertyName("pressure_msl")]
    public string? PressureMsl { get; set; }

    [JsonPropertyName("surface_pressure")]
    public string? SurfacePressure { get; set; }

    [JsonPropertyName("wind_speed_10m")]
    public string? WindSpeed10M { get; set; }

    [JsonPropertyName("wind_direction_10m")]
    public string? WindDirection10M { get; set; }

    [JsonPropertyName("wind_gusts_10m")]
    public string? WindGusts10M { get; set; }
}
