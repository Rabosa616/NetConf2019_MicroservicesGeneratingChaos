using System;
using System.Threading.Tasks;
using Microservices.GeneratingChaos.Services.Api.Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.GeneratingChaos.Services.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities = await _cityRepository.GetAllAsync().ConfigureAwait(false);
            return Ok(cities);
        }
    }
}
