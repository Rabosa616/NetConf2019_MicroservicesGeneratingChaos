using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.GeneratingChaos.Services.Weather.Domain.Models;
using Microservices.GeneratingChaos.Services.Weather.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Microservices.GeneratingChaos.Services.Weather.Controllers
{
    /// <summary>
    /// Class WeatherForecastController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("v1/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherRepository _weatherRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="weatherRepository">The weather repository.</param>
        /// <exception cref="ArgumentNullException">logger</exception>
        /// <exception cref="ArgumentNullException">weatherRepository</exception>
        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
                                         IWeatherRepository weatherRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _weatherRepository = weatherRepository ?? throw new ArgumentNullException(nameof(weatherRepository));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allWeatherForecast = await _weatherRepository.GetAllAsync().ConfigureAwait(false);
            return Ok(allWeatherForecast);

        }
    }
}
