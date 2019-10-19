using System;
using Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.Generators.Interfaces;
using Microservices.GeneratingChaos.Services.Weather.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Microservices.GeneratingChaos.Services.Weather.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SunController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDate _date;

        public SunController(ILogger<WeatherForecastController> logger, IDate date)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _date = date ?? throw new ArgumentNullException(nameof(date));
        }

        [HttpGet]
        public IActionResult Get(Guid cityId)
        {
            if (cityId == Guid.Empty)
            {
                return BadRequest();
            }
            var rng = new Random();
            var result = new Sun
            {
                CityId = cityId,
                CurrentDate = _date.Now(),
                SunRise = _date.Now().AddHours(rng.Next(-1, -12)),
                Sunset = _date.Now().AddHours(rng.Next(1, 12))
            };
            return Ok(result);
        }
    }
}
