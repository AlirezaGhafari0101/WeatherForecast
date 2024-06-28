using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherForecasting.Services;

namespace WeatherForecasting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] double lat, double lng)
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromSeconds(5));

            var result = await _weatherService.GetWeathedDataAsync(lat, lng, source.Token);

            return Ok(result);
        }
    }

    //public class ResponseTemplate
    //{
    //    public object? Result { get; set; } = null;
      
    //    public static ResponseTemplate Success(object data)
    //    {
    //        return new ResponseTemplate
    //        {
    //            Result = data,
    //        };
    //    }

    //}
}
