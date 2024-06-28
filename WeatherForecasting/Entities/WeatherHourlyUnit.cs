namespace WeatherForecasting.Entities
{
    public class WeatherHourlyUnit
    {
        public long Id { get; set; }
        public string Time { get; set; }
        public string Temperature_2m { get; set; }
        public string Relativehumidity_2m { get; set; }
        public string Windspeed_10m { get; set; }
    }
}
