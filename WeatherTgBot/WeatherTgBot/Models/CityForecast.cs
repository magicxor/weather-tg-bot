namespace WeatherTgBot.Models;

public class CityForecast
{
    public required City City { get; init; }
    public required Forecast? Forecast { get; init; }
}
