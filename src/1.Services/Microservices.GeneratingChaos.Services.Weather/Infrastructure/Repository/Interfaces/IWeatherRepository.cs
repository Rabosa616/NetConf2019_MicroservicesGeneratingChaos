using Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.Repositories;
using Microservices.GeneratingChaos.Services.Weather.Domain.Entities;

namespace Microservices.GeneratingChaos.Services.Weather.Infrastructure.Repository.Interfaces
{
    internal interface IWeatherRepository : IRepository<WeatherForecast>
    { }
}
