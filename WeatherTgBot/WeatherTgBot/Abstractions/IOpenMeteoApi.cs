using Refit;

namespace WeatherTgBot.Abstractions;

public interface IOpenMeteoApi
{
    // see https://open-meteo.com/en/docs/
    [Get("/v1/forecast?latitude={latitudes}&longitude={longitudes}&timezone={timezones}&current=temperature_2m,relative_humidity_2m,precipitation,rain,showers,snowfall,cloud_cover,wind_speed_10m&hourly=&daily=&forecast_days=1")]
    Task<string> GetWeatherAsync(string latitudes, string longitudes, string timezones, CancellationToken cancellationToken);
}
