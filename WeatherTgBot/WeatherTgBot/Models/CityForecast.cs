namespace WeatherTgBot.Models;

public sealed class CityForecast
{
    public required City City { get; init; }
    public required Forecast? Forecast { get; init; }
}
