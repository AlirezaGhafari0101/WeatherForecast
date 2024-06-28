using Microsoft.EntityFrameworkCore;
using WeatherForecasting.Context;
using WeatherForecasting.Entities;

namespace WeatherForecasting.Repository
{
    public class Repository: IRepository
    {
        private readonly WeatherDBContext _ctx;

        public Repository(WeatherDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddLocationMainDataAsync(LocationMainData mainData)
        {
            await _ctx.LocationMainDatas.AddAsync(mainData);
        }

        public async Task AddHourlyUnitAsync(WeatherHourlyUnit unit)
        {
            await _ctx.WeatherHourlyUnits.AddAsync(unit);
        }

        public async Task<bool> IsHourlyUnitExistAsync()
        {
            return await _ctx.WeatherHourlyUnits.CountAsync() > 0;
        }

        public async Task<bool> IsLocationMainDataExistAsync(double lat ,double lng) 
        {
            return await _ctx.LocationMainDatas.FirstOrDefaultAsync(l => l.Latitude == lat && l.Longitude == lng) != null;
        }

        public async Task AddHourlyDataListAsync(List<WeatherHourlyData> data)
        {
            await _ctx.WeatherHourlyDatas.AddRangeAsync(data);
        }

        public async Task<LocationMainData?> GetLocationMainDataAsync(double lat, double lng)
        {
            return await _ctx.LocationMainDatas.Include(l => l.WeatherHourlyDatas).FirstOrDefaultAsync(l => l.Latitude == lat && l.Longitude == lng);
        }

        public async Task<long?> GetMainLocationDataIdAsync(double lat, double lng)
        {
            var locataionData =  await _ctx.LocationMainDatas.FirstOrDefaultAsync(l => l.Latitude == lat && l.Longitude == lng);

            return locataionData != null ? locataionData.Id : null;
        }

        public void  DeleteHourlyDataList(List<WeatherHourlyData> data)
        {
             _ctx.WeatherHourlyDatas.RemoveRange(data);
        }

        public async Task<List<WeatherHourlyData>> GetHourlyDataByMainLocationDataIdAsync(long id)
        {
            return await _ctx.WeatherHourlyDatas.Where(whd => whd.LocationMainDataId == id)
                .ToListAsync();
        }
        public async Task<WeatherHourlyUnit?> GetHourlyUnitAsync()
        {
            return await _ctx.WeatherHourlyUnits.FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
