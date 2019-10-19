using Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.SeedWork;
using System;

namespace Microservices.GeneratingChaos.Services.Api.Domain.Entities
{
    public class City : Entity
    {
        public Guid CityId { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
    }
}
