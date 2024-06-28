using WeatherForecasting.Entities;

namespace WeatherForecasting.Repository
{
    public interface IRepository
    {
        Task AddLocationMainDataAsync(LocationMainData mainData);
        Task AddHourlyUnitAsync(WeatherHourlyUnit unit);
        Task<bool> IsHourlyUnitExistAsync();
        Task<bool> IsLocationMainDataExistAsync(double lat, double lng);
        Task AddHourlyDataListAsync(List<WeatherHourlyData> data);
        Task<LocationMainData?> GetLocationMainDataAsync(double lat, double lng);
        Task<long?> GetMainLocationDataIdAsync(double lat, double lng);
        void DeleteHourlyDataList(List<WeatherHourlyData> data);
        Task<List<WeatherHourlyData>> GetHourlyDataByMainLocationDataIdAsync(long id);
        Task<WeatherHourlyUnit?> GetHourlyUnitAsync();
        Task SaveChangesAsync();
    }
}
