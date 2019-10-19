using System;

namespace Microservices.GeneratingChaos.Services.Api.Domain.Models
{
    public class City
    {
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
    }
}
