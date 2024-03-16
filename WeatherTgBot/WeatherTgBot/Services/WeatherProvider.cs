using System.Collections.ObjectModel;
using System.Text.Json;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using WeatherTgBot.Abstractions;
using WeatherTgBot.Enums;
using WeatherTgBot.Extensions;
using WeatherTgBot.Models;

namespace WeatherTgBot.Services;

public sealed class WeatherProvider : IDisposable
{
    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

    private readonly IOptions<WeatherTgBotOptions> _options;
    private readonly ILogger<WeatherProvider> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly List<City> _cities = [];
    private bool _initialized;

    public WeatherProvider(IOptions<WeatherTgBotOptions> options,
        ILogger<WeatherProvider> logger,
        IHttpClientFactory httpClientFactory)
    {
        _options = options;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    private void Initialize()
    {
        _semaphoreSlim.Wait();

        try
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
        finally
        {
            _semaphoreSlim.Release();
        }
    }

    public async Task<IReadOnlyList<CityForecast>> GetWeatherInCitiesAsync(string substring, int limit = 10, CancellationToken cancellationToken = default)
    {
        Initialize();

        if (string.IsNullOrWhiteSpace(substring))
        {
            return ReadOnlyCollection<CityForecast>.Empty;
        }

        var cities = _cities
            .Where(city => city.Name?.Contains(substring, StringComparison.InvariantCultureIgnoreCase) == true
                        || city.AsciiName?.Contains(substring, StringComparison.InvariantCultureIgnoreCase) == true
                        || city.AlternateNames?.Contains(substring, StringComparison.InvariantCultureIgnoreCase) == true)
            .OrderByDescending(city => city.Name?.StartsWith(substring, StringComparison.InvariantCultureIgnoreCase) == true)
            .ThenByDescending(city => city.Population)
            .DistinctBy(city => city.Coordinates)
            .Take(limit)
            .ToList();

        if (cities.Count == 0)
        {
            return ReadOnlyCollection<CityForecast>.Empty;
        }

        var latitudes = string.Join(',', cities.Select(c => c.GetLatitude()));
        var longitudes = string.Join(',', cities.Select(c => c.GetLongitude()));
        var timezones = string.Join(',', cities.Select(c => c.Timezone));

        var httpClient = _httpClientFactory.CreateClient(nameof(HttpClientTypes.ExternalContent));
        httpClient.BaseAddress = new Uri(_options.Value.OpenMeteoApiUrl);
        var api = RestService.For<IOpenMeteoApi>(httpClient);
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

    public void Dispose()
    {
        _semaphoreSlim.Dispose();
    }
}
