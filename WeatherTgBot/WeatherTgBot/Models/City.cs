using CsvHelper.Configuration.Attributes;

namespace WeatherTgBot.Models;

[Delimiter(";")]
[CultureInfo("InvariantCulture")]
public sealed class City
{
    [Name("Name")]
    [Optional]
    public string? Name { get; set; }

    [Name("ASCII Name")]
    [Optional]
    public string? AsciiName { get; set; }

    [Name("Alternate Names")]
    [Optional]
    public string? AlternateNames { get; set; }

    [Name("Country name EN")]
    [Optional]
    public string? CountryName { get; set; }

    [Name("Admin1 Code")]
    [Optional]
    public string? Admin1Code { get; set; }

    [Name("Population")]
    [Optional]
    public int Population { get; set; }

    [Name("Timezone")]
    [Optional]
    public string? Timezone { get; set; }

    [Name("Coordinates")]
    [Optional]
    public string? Coordinates { get; set; }
}
