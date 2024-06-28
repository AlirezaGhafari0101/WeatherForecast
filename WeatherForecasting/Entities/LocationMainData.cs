namespace WeatherForecasting.Entities
{
    public class LocationMainData
    {
        public long Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double GenerationtimeMs { get; set; }
        public double UtcOffsetSeconds { get; set; }
        public string Timezone { get; set; }
        public string TimezoneAbbreviation { get; set; }
        public double Elevation { get; set; }

        public List<WeatherHourlyData> WeatherHourlyDatas { get; set; }
    }
}
