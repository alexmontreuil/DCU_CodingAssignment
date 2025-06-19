namespace WeatherAPI.Services
{
    using System.Diagnostics;
    using System.Net.Http.Json;
    using System.Text.Json;
    using WeatherAPI.Models;

    public class WeatherService : IWeather
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _apiKey = _config["WeatherService:ApiKey"]!;
        }

        public async Task<WeatherResponse> GetCurrentWeather(string zipcode, string units)
        {
            var unitParam = units == "celsius" ? "metric" : "imperial";
            var url = $"https://api.openweathermap.org/data/2.5/weather?zip={zipcode}&appid={_apiKey}&units={unitParam}";

            JsonDocument? res = null; // Use nullable type to handle potential null values.  

            try
            {
                res = await _httpClient.GetFromJsonAsync<JsonDocument>(url);
                if (res == null)
                {
                    throw new Exception("Failed to fetch weather data.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching weather data.", ex);
            }

            //get the json root element to get easy access to the data
            JsonElement root = res.RootElement;

            return new WeatherResponse
            {
                CurrentTemperature = root.GetProperty("main").GetProperty("temp").GetDouble(),
                Unit = (units == "celsius") ? "C" : "F",
                Lat = root.GetProperty("coord").GetProperty("lat").GetDouble(),
                Lon = root.GetProperty("coord").GetProperty("lon").GetDouble(),
                RainPossibleToday = root.GetProperty("weather").EnumerateArray()
                    .Any(w => w.GetProperty("description").GetString()?.Contains("rain", StringComparison.OrdinalIgnoreCase) == true)
            };
        }

        public async Task<WeatherResponse> GetAverageWeather(string zipcode, string units, int days)
        {
            var unitParam = units == "celsius" ? "metric" : "imperial";
            var url = $"https://api.openweathermap.org/data/2.5/forecast?zip={zipcode}&appid={_apiKey}&units={unitParam}";


            JsonDocument? res = null; // Use nullable type to handle potential null values.  
            try
            {
                res = await _httpClient.GetFromJsonAsync<JsonDocument>(url);
                if (res == null)
                {
                    throw new Exception("Failed to fetch weather data.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching weather data.", ex);
            }

            //get the json root element to get easy access to the data
            JsonElement root = res.RootElement;
            var temps = new List<double>();
            var now = DateTime.UtcNow;

            bool rainToday = false;
            foreach (var entry in root.GetProperty("list").EnumerateArray())
            {
                var dt = DateTimeOffset.FromUnixTimeSeconds((long)entry.GetProperty("dt").GetUInt64()).UtcDateTime;
                if (dt < now.AddDays(days))
                {
                    temps.Add(entry.GetProperty("main").GetProperty("temp").GetDouble());
                }

                //does the description contain rain? 
                var weatherDescription = entry.GetProperty("weather").EnumerateArray()
                    .FirstOrDefault().GetProperty("description").GetString();

                //check if today's and only today's forcast contains rain.
                if (!string.IsNullOrEmpty(weatherDescription) && weatherDescription.Contains("rain", StringComparison.OrdinalIgnoreCase) && dt == now && rainToday == false)
                {
                    rainToday = true;
                }
            }

            return new WeatherResponse
            {
                CurrentTemperature = Math.Round(temps.Average(), 2),
                Unit = (units == "celsius") ? "C" : "F",
                Lat = root.GetProperty("city").GetProperty("coord").GetProperty("lat").GetDouble(),
                Lon = root.GetProperty("city").GetProperty("coord").GetProperty("lon").GetDouble(),
                RainPossibleToday = rainToday
            };
        }
    }

}
