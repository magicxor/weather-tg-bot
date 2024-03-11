using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WeatherTgBot.Models;

public sealed class Current
{
    [JsonProperty("time")]
    [JsonPropertyName("time")]
    public DateTime? Time { get; set; }

    [JsonProperty("interval")]
    [JsonPropertyName("interval")]
    public int? Interval { get; set; }

    [JsonProperty("temperature_2m")]
    [JsonPropertyName("temperature_2m")]
    public double? Temperature2M { get; set; }

    [JsonProperty("relative_humidity_2m")]
    [JsonPropertyName("relative_humidity_2m")]
    public int? RelativeHumidity2M { get; set; }

    [JsonProperty("apparent_temperature")]
    [JsonPropertyName("apparent_temperature")]
    public double? ApparentTemperature { get; set; }

    [JsonProperty("is_day")]
    [JsonPropertyName("is_day")]
    public int? IsDay { get; set; }

    [JsonProperty("precipitation")]
    [JsonPropertyName("precipitation")]
    public double? Precipitation { get; set; }

    [JsonProperty("rain")]
    [JsonPropertyName("rain")]
    public double? Rain { get; set; }

    [JsonProperty("showers")]
    [JsonPropertyName("showers")]
    public double? Showers { get; set; }

    [JsonProperty("snowfall")]
    [JsonPropertyName("snowfall")]
    public double? Snowfall { get; set; }

    [JsonProperty("weather_code")]
    [JsonPropertyName("weather_code")]
    public int? WeatherCode { get; set; }

    [JsonProperty("cloud_cover")]
    [JsonPropertyName("cloud_cover")]
    public int? CloudCover { get; set; }

    [JsonProperty("pressure_msl")]
    [JsonPropertyName("pressure_msl")]
    public double? PressureMsl { get; set; }

    [JsonProperty("surface_pressure")]
    [JsonPropertyName("surface_pressure")]
    public double? SurfacePressure { get; set; }

    [JsonProperty("wind_speed_10m")]
    [JsonPropertyName("wind_speed_10m")]
    public double? WindSpeed10M { get; set; }

    [JsonProperty("wind_direction_10m")]
    [JsonPropertyName("wind_direction_10m")]
    public int? WindDirection10M { get; set; }

    [JsonProperty("wind_gusts_10m")]
    [JsonPropertyName("wind_gusts_10m")]
    public double? WindGusts10M { get; set; }
}
