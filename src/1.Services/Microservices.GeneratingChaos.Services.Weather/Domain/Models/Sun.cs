using System;

namespace Microservices.GeneratingChaos.Services.Weather.Domain.Models
{
    public class Sun
    {
        public Guid CityId { get; set; }
        public DateTime CurrentDate {get;set; }
        public DateTime SunRise { get; set; }
        public DateTime Sunset { get; set; }

    }
}
