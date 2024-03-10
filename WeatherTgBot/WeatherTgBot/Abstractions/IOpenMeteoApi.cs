using Refit;

namespace WeatherTgBot.Abstractions;

public interface IOpenMeteoApi
{
    // see https://open-meteo.com/en/docs/
    [Get("/v1/forecast?latitude={latitudes}&longitude={longitudes}&timezone={timezones}&current=is_day,temperature_2m,relative_humidity_2m,precipitation,rain,showers,snowfall,cloud_cover,wind_speed_10m&daily=temperature_2m_max,temperature_2m_min,precipitation_sum,rain_sum,showers_sum,snowfall_sum,precipitation_hours,precipitation_probability_max,wind_speed_10m_max")]
    Task<string> GetWeatherAsync(string latitudes, string longitudes, string timezones, CancellationToken cancellationToken);
}
