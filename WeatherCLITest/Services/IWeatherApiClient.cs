using WeatherCLI.Models;

public interface IWeatherApiClient
{
    Task<WeatherResponse?> GetCurrentWeatherAsync(string zipcode, string units);
    Task<WeatherResponse?> GetAverageWeatherAsync(string zipcode, string units, int days);
}