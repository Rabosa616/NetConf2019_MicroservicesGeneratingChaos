using Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.SeedWork;
using System;

namespace Microservices.GeneratingChaos.Services.Api.Domain.Entities
{
    public class Sun : Entity
    {
        public Guid CityId { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime SunRise { get; set; }
        public DateTime Sunset { get; set; }

    }
}
