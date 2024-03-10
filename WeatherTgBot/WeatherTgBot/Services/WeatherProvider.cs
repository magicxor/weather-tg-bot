using System.Collections.ObjectModel;
using System.Text.Json;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using WeatherTgBot.Abstractions;
using WeatherTgBot.Extensions;
using WeatherTgBot.Models;

namespace WeatherTgBot.Services;

public class WeatherProvider
{
    private readonly IOptions<WeatherTgBotOptions> _options;
    private readonly ILogger<WeatherProvider> _logger;

    private readonly List<City> _cities = [];
    private bool _initialized;

    public WeatherProvider(IOptions<WeatherTgBotOptions> options,
        ILogger<WeatherProvider> logger)
    {
        _options = options;
        _logger = logger;
    }

    private void Initialize()
    {
        if (_initialized)
            return;

        // see https://public.opendatasoft.com/explore/dataset/geonames-all-cities-with-a-population-1000/export/?disjunctive.cou_name_en&sort=name
        var config = CsvConfiguration.FromAttributes<City>();
        using (var reader = new StreamReader("geonames-all-cities-with-a-population-1000.csv"))
        using (var csv = new CsvReader(reader, config))
        {
            var data = csv.GetRecords<City>().ToList();
            _cities.AddRange(data
                .OrderByDescending(x => x.Population)
            );
        }

        _initialized = true;
    }

    public async Task<IReadOnlyList<CityForecast>> GetWeatherInCitiesAsync(string substring, int limit = 10, CancellationToken cancellationToken = default)
    {
        Initialize();

        if (string.IsNullOrWhiteSpace(substring))
        {
            return ReadOnlyCollection<CityForecast>.Empty;
        }

        var cities = _cities
            .Where(s => s.Name?.Contains(substring, StringComparison.InvariantCultureIgnoreCase) == true
                        || s.AsciiName?.Contains(substring, StringComparison.InvariantCultureIgnoreCase) == true
                        || s.AlternateNames?.Contains(substring, StringComparison.InvariantCultureIgnoreCase) == true)
            .Take(limit)
            .ToList();

        if (cities.Count == 0)
        {
            return ReadOnlyCollection<CityForecast>.Empty;
        }

        var latitudes = string.Join(',', cities.Select(c => c.GetLatitude()));
        var longitudes = string.Join(',', cities.Select(c => c.GetLongitude()));
        var timezones = string.Join(',', cities.Select(c => c.Timezone));

        var api = RestService.For<IOpenMeteoApi>(_options.Value.OpenMeteoApiUrl);
        var apiResponse = await api.GetWeatherAsync(latitudes, longitudes, timezones, cancellationToken);
        using var apiResponseJson = JsonDocument.Parse(apiResponse);

        var forecasts = apiResponseJson.RootElement.ValueKind == JsonValueKind.Array
            ? JsonSerializer.Deserialize<List<Forecast?>>(apiResponse)
            : new List<Forecast?> { JsonSerializer.Deserialize<Forecast>(apiResponse) };

        if (forecasts?.Count != cities.Count)
        {
            _logger.LogError("Number of forecasts ({ForecastsCount}) does not match number of cities ({CitiesCount})", forecasts?.Count ?? 0, cities.Count);
            return ReadOnlyCollection<CityForecast>.Empty;
        }

        return cities
            .Select((city, i) => new CityForecast
            {
                City = city,
                Forecast = forecasts[i],
            })
            .ToList();
    }
}
