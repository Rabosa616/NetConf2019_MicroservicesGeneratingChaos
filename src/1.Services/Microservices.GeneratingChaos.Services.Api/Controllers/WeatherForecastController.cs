using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.GeneratingChaos.Services.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Microservices.GeneratingChaos.Services.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(Guid cityId)
        {
            var rng = new Random();
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                CityId = cityId,
                Date = DateTime.Now.AddDays(index),
                Temperature = rng.Next(-20, 55),
                Humidity = rng.Next(-20, 55),
                Pressure = rng.Next(-20, 55)
            })
            .ToArray());
        }
    }
}
