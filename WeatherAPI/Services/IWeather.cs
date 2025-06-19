using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public interface IWeather
    {
        Task<WeatherResponse> GetCurrentWeather(string zipcode, string units);
        Task<WeatherResponse> GetAverageWeather(string zipcode, string units, int days);
    }
}
