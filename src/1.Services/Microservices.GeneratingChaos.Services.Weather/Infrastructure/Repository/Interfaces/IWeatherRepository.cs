using Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.Repositories;
using Microservices.GeneratingChaos.Services.Weather.Domain.Entities;

namespace Microservices.GeneratingChaos.Services.Weather.Infrastructure.Repository.Interfaces
{
    public interface IWeatherRepository : IRepository<WeatherForecast>
    { }
}
