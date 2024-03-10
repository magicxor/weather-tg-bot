using System.Globalization;
using WeatherTgBot.Models;

namespace WeatherTgBot.Extensions;

public static class CityForecastExtensions
{
    private static string GetSkyIcon(this Forecast forecast)
    {
        return forecast.Current?.CloudCover is not null
            ? forecast.Current.CloudCover > 50
                ? forecast.Current.Precipitation > 0
                    ? forecast.Current.Snowfall > 0
                        ? "🌨"
                        : forecast.Current.Rain > 0 || forecast.Current.Showers > 0
                            ? "🌧"
                            : "☁"
                    : forecast.Current.CloudCover > 80
                        ? "☁"
                        : "⛅"
                : forecast.Current.IsDay != 0
                    ? "🌞"
                    : "🌜"
            : string.Empty;
    }

    private static string GetTemperature(this Forecast forecast)
    {
        var temperature = forecast.Current?.Temperature2M;
        if (temperature == null)
        {
            return string.Empty;
        }

        var temperatureString = temperature.Value.ToString("0.#", CultureInfo.InvariantCulture);

        if (temperature > 0)
        {
            temperatureString = $"+{temperatureString}";
        }

        return $"{temperatureString}{forecast.CurrentUnits?.Temperature2M}";
    }

    public static string GetCityForecastString(this CityForecast cityForecast)
    {
        if (cityForecast.Forecast == null)
        {
            return string.Empty;
        }

        return $"""
                {cityForecast.City.GetInfo()}
                Weather: {cityForecast.Forecast.GetSkyIcon()} {cityForecast.Forecast.GetTemperature()}
                """;
    }

    public static string GetShortInfo(this CityForecast cityForecast)
    {
        var city = cityForecast.City;
        var population = $"{city.Population.ToString("N0", CultureInfo.InvariantCulture)}";
        var result = $"{city.Name}, {city.CountryName} 👤{population} {cityForecast.Forecast?.GetSkyIcon()} {cityForecast.Forecast?.GetTemperature()}".Trim();
        return result;
    }
}
