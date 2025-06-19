using System.Net.Http.Json;
using WeatherCLI.Models;

namespace WeatherCLI.Services;

public class WeatherApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public WeatherApiClient(string baseUrl)
    {
        _httpClient = new HttpClient();
        _baseUrl = baseUrl;
    }

    public async Task<WeatherResponse?> GetCurrentWeatherAsync(string zipcode, string units, string baseurl)
    {
        string url;
        // check if the base url was passed as an option. If note, use the default value.
        if (baseurl == null)
        {
            url = $"{_baseUrl}/Weather/Current/{zipcode}?units={units}";
        }
        else
        {
            url = $"{baseurl}/Weather/Current/{zipcode}?units={units}";
        }

        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<WeatherResponse>();

        throw new HttpRequestException($"Error {response.StatusCode}: Unable to get current weather.");
    }

    public async Task<WeatherResponse?> GetAverageWeatherAsync(string zipcode, string units, int days, string baseurl)
    {
        string url;
        // check if the base url was passed as an option. If note, use the default value.
        if (baseurl == null)
        {
            url = $"{_baseUrl}/Weather/Average/{zipcode}?units={units}&timePeriod={days}";
        } else
        {
            url = $"{baseurl}/Weather/Average/{zipcode}?units={units}&timePeriod={days}";
        }

        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<WeatherResponse>();

        throw new HttpRequestException($"Error {response.StatusCode}: Unable to get average weather.");
    }
}
