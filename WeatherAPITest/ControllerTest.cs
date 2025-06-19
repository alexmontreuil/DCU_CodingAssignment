using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherAPI;
using WeatherAPI.Controllers;
using WeatherAPI.Models;
using WeatherAPI.Services;
using Xunit;

public class WeatherControllerTests
{
    private readonly Mock<IWeather> _mockProvider;
    private readonly WeatherController _controller;

    public WeatherControllerTests()
    {
        _mockProvider = new Mock<IWeather>();
        _controller = new WeatherController(_mockProvider.Object);
    }

    [Fact]
    public async Task GetCurrent_ReturnsOk()
    {
        var fakeResponse = new WeatherResponse { CurrentTemperature = 75, Unit = "F", Lat = 10, Lon = 10, RainPossibleToday = false };
        _mockProvider.Setup(p => p.GetCurrentWeather("10001", "fahrenheit")).ReturnsAsync(fakeResponse);

        var result = await _controller.GetCurrent("10001", "fahrenheit");

        var okResult = Assert.IsType<OkObjectResult>(result);
        var data = Assert.IsType<WeatherResponse>(okResult.Value);
        Assert.Equal(75, data.CurrentTemperature);
    }

    [Fact]
    public async Task GetAverage_InvalidPeriod_ReturnsBadRequest()
    {
        var result = await _controller.GetAverage("10001", "fahrenheit", 6);
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetAverage_Valid_ReturnsOk()
    {
        var fakeResponse = new WeatherResponse { CurrentTemperature = 65, Unit = "F", Lat = 5, Lon = 5, RainPossibleToday = true };
        _mockProvider.Setup(p => p.GetAverageWeather("10001", "fahrenheit", 3)).ReturnsAsync(fakeResponse);

        var result = await _controller.GetAverage("10001", "fahrenheit", 3);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var data = Assert.IsType<WeatherResponse>(okResult.Value);
        Assert.Equal(65, data.CurrentTemperature);
    }
}
