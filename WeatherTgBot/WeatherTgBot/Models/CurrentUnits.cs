using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WeatherTgBot.Models;

public sealed class CurrentUnits
{
    [JsonProperty("time")]
    [JsonPropertyName("time")]
    public string? Time { get; set; }

    [JsonProperty("interval")]
    [JsonPropertyName("interval")]
    public string? Interval { get; set; }

    [JsonProperty("temperature_2m")]
    [JsonPropertyName("temperature_2m")]
    public string? Temperature2M { get; set; }

    [JsonProperty("relative_humidity_2m")]
    [JsonPropertyName("relative_humidity_2m")]
    public string? RelativeHumidity2M { get; set; }

    [JsonProperty("apparent_temperature")]
    [JsonPropertyName("apparent_temperature")]
    public string? ApparentTemperature { get; set; }

    [JsonProperty("is_day")]
    [JsonPropertyName("is_day")]
    public string? IsDay { get; set; }

    [JsonProperty("precipitation")]
    [JsonPropertyName("precipitation")]
    public string? Precipitation { get; set; }

    [JsonProperty("rain")]
    [JsonPropertyName("rain")]
    public string? Rain { get; set; }

    [JsonProperty("showers")]
    [JsonPropertyName("showers")]
    public string? Showers { get; set; }

    [JsonProperty("snowfall")]
    [JsonPropertyName("snowfall")]
    public string? Snowfall { get; set; }

    [JsonProperty("weather_code")]
    [JsonPropertyName("weather_code")]
    public string? WeatherCode { get; set; }

    [JsonProperty("cloud_cover")]
    [JsonPropertyName("cloud_cover")]
    public string? CloudCover { get; set; }

    [JsonProperty("pressure_msl")]
    [JsonPropertyName("pressure_msl")]
    public string? PressureMsl { get; set; }

    [JsonProperty("surface_pressure")]
    [JsonPropertyName("surface_pressure")]
    public string? SurfacePressure { get; set; }

    [JsonProperty("wind_speed_10m")]
    [JsonPropertyName("wind_speed_10m")]
    public string? WindSpeed10M { get; set; }

    [JsonProperty("wind_direction_10m")]
    [JsonPropertyName("wind_direction_10m")]
    public string? WindDirection10M { get; set; }

    [JsonProperty("wind_gusts_10m")]
    [JsonPropertyName("wind_gusts_10m")]
    public string? WindGusts10M { get; set; }
}
