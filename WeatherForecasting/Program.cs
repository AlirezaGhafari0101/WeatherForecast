using Microsoft.EntityFrameworkCore;
using WeatherForecasting.Context;
using WeatherForecasting.Repository;
using WeatherForecasting.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WeatherDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

builder.Services.AddHttpClient("open-meteo", (ServiceProvider, client) =>
{
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.BaseAddress = new Uri("https://api.open-meteo.com");
});

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<WeatherService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
