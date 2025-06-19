using System.Text.Json;
using YamlDotNet.Serialization;
using WeatherCLI.Models;

namespace WeatherCLI.Helpers;

public static class OutputFormatter
{
    public static void Print(WeatherResponse data, string format)
    {
        switch (format)
        {
            case "json":
                Console.WriteLine(JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
                break;
            case "yaml":
                var serializer = new Serializer();
                Console.WriteLine(serializer.Serialize(data));
                break;
            case "table":
                Console.WriteLine($"{"Field",-20}{"Value"}");
                Console.WriteLine($"{"--------------------"}---------");
                Console.WriteLine($"{"Temperature",-20}{data.CurrentTemperature}°{data.Unit}");
                Console.WriteLine($"{"Latitude",-20}{data.Lat}");
                Console.WriteLine($"{"Longitude",-20}{data.Lon}");
                Console.WriteLine($"{"Rain Possible",-20}{(data.RainPossibleToday ? "Yes" : "No")}");
                break;
            default: // plain text
                Console.WriteLine($"Temperature: {data.CurrentTemperature}°{data.Unit}");
                Console.WriteLine($"Location: ({data.Lat}, {data.Lon})");
                Console.WriteLine($"Rain Possible: {(data.RainPossibleToday ? "Yes" : "No")}");
                break;
        }
    }
}
