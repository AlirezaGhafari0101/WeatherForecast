using WeatherForecasting.Entities;

namespace WeatherForecasting
{
    public static class Mapper
    {
        public static HourlyUnits WeatherHourlyUnitToHourlyUnits(this WeatherHourlyUnit unit)
        {
            return new HourlyUnits {
            relativehumidity_2m = unit.Relativehumidity_2m,
            temperature_2m = unit.Temperature_2m,
            time = unit.Time,
            windspeed_10m = unit.Windspeed_10m,
            };

        }

        public static Hourly WeatherHourlyDataToHourly(this List<WeatherHourlyData> list)
        {

            var newHourly = new Hourly ();

            foreach (var item in list)
            {
                newHourly.time.Add(item.Time);
                newHourly.temperature_2m.Add(item.Temperature_2m);
                newHourly.relativehumidity_2m.Add(item.Relativehumidity_2m);
                newHourly.windspeed_10m.Add(item.Windspeed_10m);
            }

            return newHourly;

        }

        public static List<WeatherHourlyData> HourlyToWeatherHourlyData(this Hourly hourly, long? LocationMainDataId)
        {
            var listWeatherHourlyData = new List<WeatherHourlyData>();

            for (int i = 0; i < hourly.time.Count; i++)
            {
                var newWeatherHourlyData = new WeatherHourlyData();

                newWeatherHourlyData.Time = hourly.time[i];
                newWeatherHourlyData.Windspeed_10m = hourly.windspeed_10m[i];
                newWeatherHourlyData.Relativehumidity_2m = hourly.relativehumidity_2m[i];
                newWeatherHourlyData.Temperature_2m = hourly.temperature_2m[i];
                newWeatherHourlyData.LocationMainDataId = LocationMainDataId  ?? 0;

                listWeatherHourlyData.Add(newWeatherHourlyData);
            }

            return listWeatherHourlyData;
        }

        public static WeatherHourlyUnit HourlyUnitToWeatherHourlyUnits(this HourlyUnits unit)
        {
            return new WeatherHourlyUnit
            {
                Relativehumidity_2m = unit.relativehumidity_2m,
                Temperature_2m = unit.temperature_2m,
                Time = unit.time,
                Windspeed_10m = unit.windspeed_10m,
            };
        }

        public static WeatherData LocationMainDataToWeatherData(this LocationMainData locationMainData, WeatherHourlyUnit? hourlyUnit)
        {
            return new WeatherData
            {
                elevation = locationMainData.Elevation,
                latitude = locationMainData.Latitude,
                generationtime_ms = locationMainData.GenerationtimeMs,
                hourly = locationMainData.WeatherHourlyDatas.WeatherHourlyDataToHourly(),
                hourly_units = hourlyUnit != null ? hourlyUnit.WeatherHourlyUnitToHourlyUnits() : new HourlyUnits(),
                longitude = locationMainData.Longitude,
                timezone = locationMainData.Timezone,
                timezone_abbreviation = locationMainData.TimezoneAbbreviation,
                utc_offset_seconds = locationMainData.UtcOffsetSeconds,
            };
        }

        public static LocationMainData WeatherDataToLocationMainData(this WeatherData data)
        {
            return new LocationMainData
            {
                Elevation = data.elevation,
                GenerationtimeMs = data.generationtime_ms,
                Latitude = data.latitude,
                Longitude = data.longitude,
                Timezone = data.timezone,
                TimezoneAbbreviation = data.timezone_abbreviation,
                UtcOffsetSeconds = data.utc_offset_seconds,
                WeatherHourlyDatas = data.hourly.HourlyToWeatherHourlyData(null)
            };
        }
    }
}
