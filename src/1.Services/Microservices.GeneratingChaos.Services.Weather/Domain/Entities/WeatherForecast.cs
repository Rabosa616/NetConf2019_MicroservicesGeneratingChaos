using System;
using Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.SeedWork;

namespace Microservices.GeneratingChaos.Services.Weather.Domain.Entities
{
    public class WeatherForecast : Entity
    {
        public Guid CityId { get; set; }

        public DateTime Date { get; set; }

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public double Pressure { get; set; }

        public string Icon { get; set; }
    }
}