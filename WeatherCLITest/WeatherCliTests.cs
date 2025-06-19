using Xunit;
using Moq;
using WeatherCLI.Models;
using WeatherCLI.Helpers;
using System.IO;
using System;

[CollectionDefinition("Sequential", DisableParallelization = true)]
public class WeatherCliTests
{
    private StringWriter _stringWriter;
    private TextWriter _originalOut;

    public WeatherCliTests()
    {
        _originalOut = Console.Out;
        _stringWriter = new StringWriter();
        //Console.SetOut(_stringWriter);
    }

    private void Dispose()
    {
        Console.SetOut(_originalOut);
        _stringWriter.Dispose();
        _stringWriter = new StringWriter();
    }

    
    [Fact]
    public void OutputFormatter_PrintsTextCorrectly()
    {
        var response = new WeatherResponse
        {
            CurrentTemperature = 60,
            Unit = "F",
            Lat = 45.67,
            Lon = 54.36,
            RainPossibleToday = true
        };



        Console.SetOut(_stringWriter);

        try
        {
            OutputFormatter.Print(response, "text");
            var output = _stringWriter.ToString();


            Assert.Contains("Temperature: 60", output);
            Assert.Contains("Rain Possible: Yes", output);
        }
        finally
        {
            Dispose();
        }
    }
   
    
    
    [Fact]
    public void OutputFormatter_PrintsJsonCorrectly()
    {
        var response = new WeatherResponse
        {
            CurrentTemperature = 60,
            Unit = "F",
            Lat = 45.67,
            Lon = 54.36,
            RainPossibleToday = true
        };


        Console.SetOut(_stringWriter);
        try { 
            OutputFormatter.Print(response, "json");
            var output = _stringWriter.ToString();


            Assert.Contains("\"CurrentTemperature\": 60,", output);
        }
        finally
        {
            Dispose();
        }

    }

    
    
    [Fact]
    public void OutputFormatter_PrintsYamlCorrectly()
    {
        var response = new WeatherResponse
        {
            CurrentTemperature = 60,
            Unit = "F",
            Lat = 45.67,
            Lon = 54.36,
            RainPossibleToday = true
        };


        Console.SetOut(_stringWriter);

        try { 
            OutputFormatter.Print(response, "yaml");
            var output = _stringWriter.ToString();


            Assert.Contains("Temperature: 60", output);
        }
        finally
        {
            Dispose();
        }
    }
    
    

    [Fact]
    public void OutputFormatter_PrintsTableCorrectly()
    {
        var response = new WeatherResponse
        {
            CurrentTemperature = 60,
            Unit = "F",
            Lat = 45.67,
            Lon = 54.36,
            RainPossibleToday = true
        };


        Console.SetOut(_stringWriter);

        try { 
            OutputFormatter.Print(response, "table");
            var output = _stringWriter.ToString();
    

            Assert.Contains("--------------------", output);
        }
        finally
        {
            Dispose();
        }
    }
  
}
