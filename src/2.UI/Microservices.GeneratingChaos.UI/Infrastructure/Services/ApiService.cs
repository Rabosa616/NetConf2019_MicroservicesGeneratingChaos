using Microservices.GeneratingChaos.UI.Domain.Models;
using Microservices.GeneratingChaos.UI.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microservices.GeneratingChaos.UI.Infrastructure.Services
{
    /// <summary>
    /// Class ApiService.
    /// Implements the <see cref="Microservices.GeneratingChaos.UI.Infrastructure.Services.Interfaces.IApiService" />
    /// </summary>
    /// <seealso cref="Microservices.GeneratingChaos.UI.Infrastructure.Services.Interfaces.IApiService" />
    public class ApiService : IApiService
    {
        private readonly HttpClient Http;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiService"/> class.
        /// </summary>
        /// <param name="http">The HTTP.</param>
        /// <exception cref="ArgumentNullException">http</exception>
        public ApiService(HttpClient http)
        {
            Http = http ?? throw new ArgumentNullException(nameof(http));
        }

        /// <summary>
        /// get weather by city identifier as an asynchronous operation.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>Task&lt;IEnumerable&lt;WheatherForecastDto&gt;&gt;.</returns>
        public async Task<IEnumerable<WheatherForecastDto>> GetWeatherByCityIdAsync(Guid cityId)
        {
            return await Http.GetJsonAsync<WheatherForecastDto[]>($"api/v1/WeatherForecast/{cityId}").ConfigureAwait(false);
        }

        /// <summary>
        /// get cities as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;CityDto&gt;&gt;.</returns>
        public async Task<IEnumerable<CityDto>> GetCitiesAsync()
        {
            return  await Http.GetJsonAsync<CityDto[]>("api/v1/City").ConfigureAwait(false);
        }
    }
}
