using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;

using WeatherAPI.Services;


namespace WeatherAPI.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeather _weather;

        public WeatherController(IWeather service)
        {
            _weather = service;
        }

        [HttpGet("Current/{zipcode}")]
        public async Task<IActionResult> GetCurrent(string zipcode, [FromQuery] string units = "fahrenheit")
        {
            try
            {
                var result = await _weather.GetCurrentWeather(zipcode, units);
                return Ok(result);
            }
            catch (ArgumentException)
            {
                return BadRequest("Invalid zipcode.");
            }
            catch
            {
                return StatusCode(500, "Error fetching weather.");
            }
        }

        [HttpGet("Average/{zipcode}")]
        public async Task<IActionResult> GetAverage(string zipcode, [FromQuery] string units = "fahrenheit", [FromQuery] int timePeriod = 3)
        {
            if (timePeriod < 2 || timePeriod > 5)
            {
                return BadRequest("timePeriod must be between 2 and 5.");
            }
            try
            {
                var result = await _weather.GetAverageWeather(zipcode, units, timePeriod);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Error fetching average weather.");
            }
        }
    }

}
