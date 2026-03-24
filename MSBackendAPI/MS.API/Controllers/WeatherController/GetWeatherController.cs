using Microsoft.AspNetCore.Mvc;
using MS.Application.Services.WeatherServices.GetWeatherService;

namespace MS.API.Controllers.WeatherController
{
    /// <summary>
    /// API controller responsible for retrieving weather information
    /// </summary>
    [Route("api/v1/weather")]
    [ApiController]
    public class GetWeatherController : ControllerBase
    {
        private readonly IGetWeatherService _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">Weather service</param>
        public GetWeatherController(IGetWeatherService service)
        {
            _service = service;
        }

        /// <summary>
        /// Execute API to get weather by city
        /// </summary>
        /// <param name="city">City name</param>
        /// <returns>Weather information</returns>
        [HttpGet]
        public async Task<IActionResult> Execute([FromQuery] string city)
        {
            var request = new GetWeatherRequest
            {
                City = city
            };

            var result = await _service.Process(request);

            return Ok(result);
        }
    }
}
