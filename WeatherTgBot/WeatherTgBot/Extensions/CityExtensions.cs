using System.Globalization;
using TimeZoneConverter;
using WeatherTgBot.Models;

namespace WeatherTgBot.Extensions;

public static class CityExtensions
{
    public static string? GetCountryNameWithState(this City city)
    {
        return city.CountryName == "United States"
            ? string.IsNullOrEmpty(city.Admin1Code)
                ? city.CountryName
                : $"{city.CountryName}, {city.Admin1Code}"
            : city.CountryName;
    }

    public static string GetInfo(this City city)
    {
        var population = $"{city.Population.ToString("N0", CultureInfo.InvariantCulture)}";

        var timezoneId = city.Timezone ?? "Etc/UTC";
        TimeZoneInfo? tzInfo = null;
        try
        {
            tzInfo = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
        }
        catch (Exception)
        {
            if (TZConvert.TryGetTimeZoneInfo(timezoneId, out var tz))
            {
                tzInfo = tz;
            }
        }

        tzInfo ??= TimeZoneInfo.FindSystemTimeZoneById("Etc/UTC");

        var timestamp = city.Timezone is null
            ? "N/A"
            : TimeZoneInfo
                .ConvertTime(DateTimeOffset.UtcNow, tzInfo)
                .ToString("HH:mm (yyyy-MM-dd)", CultureInfo.InvariantCulture);
        var result = $"""
                      📍{city.Name}, {GetCountryNameWithState(city)}
                      👤{population}
                      🕑{timestamp}
                      """;
        return result;
    }

    private static string GetCoordinate(this City city, int index)
    {
        var coordinates = city.Coordinates?.Split(", ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (coordinates is null || coordinates.Length < 2)
        {
            return string.Empty;
        }

        return coordinates[index];
    }

    public static string GetLatitude(this City city)
    {
        return city.GetCoordinate(0);
    }

    public static string GetLongitude(this City city)
    {
        return city.GetCoordinate(1);
    }
}
