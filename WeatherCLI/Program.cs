using System;
using System.CommandLine;
using WeatherCLI.Helpers;
using WeatherCLI.Services;

var baseUrl = "http://localhost:5257"; // Replace if hosted elsewhere
var apiClient = new WeatherApiClient(baseUrl);

// get-current-weather command
var currentCmd = new Command("get-current-weather", "Retrieve current weather")
{
    new Argument<string>("zipcode"),
    new Argument<string>("units"),
    new Option<string>("--output", () => "text", "Output format: json|yaml|text|table"),
    new Option<string>("--baseurl")
};

currentCmd.SetHandler(async (string zipcode, string units, string output, string baseurl) =>
{
    try
    {
        var result = await apiClient.GetCurrentWeatherAsync(zipcode, units, baseurl);
        OutputFormatter.Print(result!, output);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}, (System.CommandLine.Binding.IValueDescriptor<string>)currentCmd.Arguments[0], (System.CommandLine.Binding.IValueDescriptor<string>)currentCmd.Arguments[1], (System.CommandLine.Binding.IValueDescriptor<string>)currentCmd.Options[0], (System.CommandLine.Binding.IValueDescriptor<string>)currentCmd.Options[1]);

// get-average-weather command
var averageCmd = new Command("get-average-weather", "Retrieve average weather")
{
    new Argument<string>("zipcode"),
    new Argument<string>("units"),
    new Argument<int>("timePeriod"),
    new Option<string>("--output", () => "text", "Output format: json|yaml|text|table"),
    new Option<string>("--baseurl")
};

averageCmd.SetHandler(async (string zipcode, string units, int timePeriod, string output, String baseurl) =>
{
    try
    {
        var result = await apiClient.GetAverageWeatherAsync(zipcode, units, timePeriod, baseurl);
        OutputFormatter.Print(result!, output);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}, (System.CommandLine.Binding.IValueDescriptor<string>)averageCmd.Arguments[0], (System.CommandLine.Binding.IValueDescriptor<string>)averageCmd.Arguments[1], (System.CommandLine.Binding.IValueDescriptor<int>)averageCmd.Arguments[2], (System.CommandLine.Binding.IValueDescriptor<string>)averageCmd.Options[0], (System.CommandLine.Binding.IValueDescriptor<string>)averageCmd.Options[1]);

var root = new RootCommand("Weather CLI");
root.AddCommand(currentCmd);
root.AddCommand(averageCmd);

await root.InvokeAsync(args);
