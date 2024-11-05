using System.Text.Json.Serialization;

namespace WeatherTgBot.Models;

public sealed class Current
{
    [JsonPropertyName("time")]
    public DateTime? Time { get; set; }

    [JsonPropertyName("interval")]
    public int? Interval { get; set; }

    [JsonPropertyName("temperature_2m")]
    public double? Temperature2M { get; set; }

    [JsonPropertyName("relative_humidity_2m")]
    public int? RelativeHumidity2M { get; set; }

    [JsonPropertyName("apparent_temperature")]
    public double? ApparentTemperature { get; set; }

    [JsonPropertyName("is_day")]
    public int? IsDay { get; set; }

    [JsonPropertyName("precipitation")]
    public double? Precipitation { get; set; }

    [JsonPropertyName("rain")]
    public double? Rain { get; set; }

    [JsonPropertyName("showers")]
    public double? Showers { get; set; }

    [JsonPropertyName("snowfall")]
    public double? Snowfall { get; set; }

    [JsonPropertyName("weather_code")]
    public int? WeatherCode { get; set; }

    [JsonPropertyName("cloud_cover")]
    public int? CloudCover { get; set; }

    [JsonPropertyName("pressure_msl")]
    public double? PressureMsl { get; set; }

    [JsonPropertyName("surface_pressure")]
    public double? SurfacePressure { get; set; }

    [JsonPropertyName("wind_speed_10m")]
    public double? WindSpeed10M { get; set; }

    [JsonPropertyName("wind_direction_10m")]
    public int? WindDirection10M { get; set; }

    [JsonPropertyName("wind_gusts_10m")]
    public double? WindGusts10M { get; set; }
}
