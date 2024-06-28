namespace WeatherForecasting.Entities
{
    public class WeatherHourlyData
    {
        public long Id { get; set; }
        public string Time { get; set; }
        public double Temperature_2m { get; set; }
        public double Relativehumidity_2m { get; set; }
        public double Windspeed_10m { get; set; }

        public long LocationMainDataId  { get; set; }
        public LocationMainData LocationMainData { get; set; }
    }
}
