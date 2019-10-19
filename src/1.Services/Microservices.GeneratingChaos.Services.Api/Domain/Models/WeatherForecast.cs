using System;

namespace Microservices.GeneratingChaos.Services.Api.Domain.Models
{
    public class WeatherForecast
    {
        public Guid CityId { get; set; }

        public DateTime Date { get; set; }

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public double Pressure { get; set; }

        public string Icon { get; set; }
    }
}