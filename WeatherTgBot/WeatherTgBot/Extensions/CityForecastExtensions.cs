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

    private static string GetWind(this Forecast forecast)
    {
        return forecast.Current?.WindSpeed10M switch
        {
            null => "N/A",
            <= 2 => "🏡 Calm",
            <= 5 => "🜁 Light air",
            <= 11 => "🌬️ Light breeze",
            <= 19 => "🍃 Gentle breeze",
            <= 28 => "☴ Moderate breeze",
            <= 38 => "💨 Fresh breeze",
            <= 49 => "💨💨 Strong breeze",
            <= 61 => "💨🌪 High wind",
            <= 74 => "⚠️💨 Gale",
            <= 88 => "⚠️🌪 Strong gale",
            <= 102 => "⚠️🌀 Storm",
            <= 117 => "⚠️🌪️ Violent storm",
            _ => "⚠️💀 Hurricane",
        };
    }

    public static string GetCityForecastString(this CityForecast cityForecast)
    {
        var weatherString = cityForecast.Forecast == null
            ? "N/A"
            : $"""
               {cityForecast.Forecast.GetSkyIcon()}{cityForecast.Forecast.GetTemperature()}
               {cityForecast.Forecast.GetWind()}
               """;

        return $"""
                {cityForecast.City.GetInfo()}
                Weather: {weatherString}
                """;
    }

    public static string GetShortInfo(this CityForecast cityForecast)
    {
        var city = cityForecast.City;
        var population = $"{city.Population.ToString("N0", CultureInfo.InvariantCulture)}";
        var result = $"{city.Name}, {city.GetCountryNameWithState()} 👤{population} {cityForecast.Forecast?.GetSkyIcon()} {cityForecast.Forecast?.GetTemperature()}".Trim();
        return result;
    }
}
