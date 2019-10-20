using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microservices.GeneratingChaos.Services.Api.Domain.Models;
using Microservices.GeneratingChaos.Services.Api.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.GeneratingChaos.Services.Api.Controllers
{
    /// <summary>
    /// Class CityController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CityController : ControllerBase
    {
        /// <summary>
        /// The city repository
        /// </summary>
        private readonly ICityRepository _cityRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityController"/> class.
        /// </summary>
        /// <param name="cityRepository">The city repository.</param>
        /// <exception cref="ArgumentNullException">cityRepository</exception>
        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<City>))]
        public async Task<IActionResult> Get()
        {
            var cities = await _cityRepository.GetAllAsync().ConfigureAwait(false);
            return Ok(cities);
        }
    }
}
