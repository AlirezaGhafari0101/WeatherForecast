using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json;
using WeatherForecasting.Entities;
using WeatherForecasting.Repository;

namespace WeatherForecasting.Services
{
    public class WeatherService
    {
        private readonly IRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherService(IRepository repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<WeatherData?> GetWeathedDataAsync(double lat, double lng, CancellationToken cancellationToken)
        {
            try
            {
                var webServiceResponse = await GetWeatherDataFromWebServiceAsync(lat, lng, cancellationToken);

                if (webServiceResponse == null)
                {
                    var databaseResponse = await GetWeatherDataFromDataBaseAsync(lat, lng, cancellationToken);

                    return databaseResponse;
                }

                return webServiceResponse;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<WeatherData?> GetWeatherDataFromWebServiceAsync(double lat, double lng, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient("open-meteo");

            client.Timeout = TimeSpan.FromSeconds(3.5);

            try
            {
                var uri = $"/v1/forecast?latitude={lat.ToString(CultureInfo.InvariantCulture)}&longitude={lng.ToString(CultureInfo.InvariantCulture)}&hourly=temperature_2m,relativehumidity_2m,windspeed_10m";
                var response = await client.GetStringAsync(uri, cancellationToken);

                var deserializedResponse = JsonSerializer.Deserialize<WeatherData>(response);

                if (deserializedResponse != null)
                {
                    await AddWeatherDataToDataBaseAsync(deserializedResponse, cancellationToken);
                }

                return deserializedResponse;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<WeatherData?> GetWeatherDataFromDataBaseAsync(double lat, double lng, CancellationToken cancellationToken)
        {
            try
            {
                var locationMainData = await _repository.GetLocationMainDataAsync(lat, lng);

                var hourlyUnit = await _repository.GetHourlyUnitAsync();

                if (locationMainData == null) return null;

                //var weatherData = new WeatherData
                //{
                //    elevation = locationMainData.Elevation,
                //    latitude = locationMainData.Latitude,
                //    generationtime_ms = locationMainData.GenerationtimeMs,
                //    hourly = locationMainData.WeatherHourlyDatas.WeatherHourlyDataToHourly(),
                //    hourly_units = hourlyUnit != null ? hourlyUnit.WeatherHourlyUnitToHourlyUnits() : new HourlyUnits(),
                //    longitude = locationMainData.Longitude,
                //    timezone = locationMainData.Timezone,
                //    timezone_abbreviation = locationMainData.TimezoneAbbreviation,
                //    utc_offset_seconds = locationMainData.UtcOffsetSeconds,
                //};

                var weatherData = locationMainData.LocationMainDataToWeatherData(hourlyUnit);

                return weatherData;
            }
            catch (Exception)
            {

                return null;
            }
        }

        private async Task AddWeatherDataToDataBaseAsync(WeatherData data, CancellationToken cancellationToken)
        {
            try
            {
                var LocationMainData = await _repository.GetLocationMainDataAsync(data.latitude, data.longitude);

                if (LocationMainData == null)
                {
                    var newLocationMainData = new LocationMainData
                    {
                        Elevation = data.elevation,
                        Latitude = data.latitude,
                        GenerationtimeMs = data.generationtime_ms,
                        Longitude = data.longitude,
                        Timezone = data.timezone,
                        TimezoneAbbreviation = data.timezone_abbreviation,
                        UtcOffsetSeconds = data.utc_offset_seconds,
                        WeatherHourlyDatas = data.hourly.HourlyToWeatherHourlyData(null),
                    };

                    var weatherDataUnits = data.hourly_units.HourlyUnitToWeatherHourlyUnits();

                    await _repository.AddLocationMainDataAsync(newLocationMainData);

                    if (!await _repository.IsHourlyUnitExistAsync())
                        await _repository.AddHourlyUnitAsync(weatherDataUnits);

                    await _repository.SaveChangesAsync();
                }
                else
                {
                    _repository.DeleteHourlyDataList(LocationMainData.WeatherHourlyDatas);

                    await _repository.AddHourlyDataListAsync(data.hourly.HourlyToWeatherHourlyData(LocationMainData.Id));
                }
            }
            catch (Exception)
            {

                return;
            }
        }
    }
}
