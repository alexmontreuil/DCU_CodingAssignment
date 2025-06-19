namespace WeatherAPI.Models
{
    public class WeatherResponse
    {
        public double CurrentTemperature { get; set; }
        public string Unit { get; set; } = "F";
        public double Lat { get; set; }
        public double Lon { get; set; }
        public bool RainPossibleToday { get; set; }
    }
}
