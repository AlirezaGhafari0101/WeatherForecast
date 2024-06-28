using Microsoft.EntityFrameworkCore;
using WeatherForecasting.Entities;

namespace WeatherForecasting.Context
{
    public class WeatherDBContext: DbContext
    {
        public WeatherDBContext(DbContextOptions<WeatherDBContext> options) : base(options)
        {

        }


        public DbSet<LocationMainData> LocationMainDatas { get; set; }
        public DbSet<WeatherHourlyUnit> WeatherHourlyUnits { get; set; }
        public DbSet<WeatherHourlyData> WeatherHourlyDatas { get; set; }
    }
}
