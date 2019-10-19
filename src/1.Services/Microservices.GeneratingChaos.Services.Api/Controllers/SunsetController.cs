using System;
using System.Collections.Generic;
using System.Linq;
using Microservices.GeneratingChaos.Services.Api.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.GeneratingChaos.Services.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SunsetController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(Guid cityId)
        {
            var rng = new Random();
            return Ok(Enumerable.Range(1, 1).Select(index => new Sun
            {
                CityId = cityId,
                CurrentDate = DateTime.Now.AddDays(index),
                SunRise = DateTime.Now.AddDays(index),
                Sunset = DateTime.Now.AddDays(index)
            })
            .ToArray());
        }
    }
}
