namespace WeatherForecasting
{
    public class WeatherData
    {

        public double latitude { get; set; }
        public double longitude { get; set; }
        public double generationtime_ms { get; set; }
        public double utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public double elevation { get; set; }

        public HourlyUnits hourly_units { get; set; }

        public Hourly hourly { get; set; }
    }

    public class HourlyUnits
    {
        public string time { get; set; }
        public string temperature_2m { get; set; }
        public string relativehumidity_2m { get; set; }
        public string windspeed_10m { get; set; }
    }

    public class Hourly
    {
        public List<string> time { get; set; } = new List<string>();
        public List<double> temperature_2m { get; set; } = new List<double>();
        public List<double> relativehumidity_2m { get; set; } = new List<double>();
        public List<double> windspeed_10m { get; set; } = new List<double>();
    }
}
