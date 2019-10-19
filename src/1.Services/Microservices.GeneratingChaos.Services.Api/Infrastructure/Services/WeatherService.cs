using Microservices.GeneratingChaos.Services.Api.Domain.Models;
using Microservices.GeneratingChaos.Services.Api.Infrastructure.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microservices.GeneratingChaos.Services.Api.Infrastructure.Services
{
    /// <summary>
    /// Class WeatherService.
    /// Implements the <see cref="Microservices.GeneratingChaos.Services.Api.Infrastructure.Services.Interfaces.IWeatherService" />
    /// </summary>
    /// <seealso cref="Microservices.GeneratingChaos.Services.Api.Infrastructure.Services.Interfaces.IWeatherService" />
    public class WeatherService : IWeatherService
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherService" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <exception cref="ArgumentNullException">httpClient</exception>
        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>Task&lt;IEnumerable&lt;WeatherForecast&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
        {
            var responseString = await _httpClient.GetStringAsync("v1/weatherforecast/")
                                                      .ConfigureAwait(false);
            //var item = await response.Content.ReadAsStringAsync();
            var weatherForecast = JsonConvert.DeserializeObject<List<WeatherForecast>>(responseString);
            return weatherForecast;
        }

        /// <summary>
        /// Gets the by city asynchronous.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>Task&lt;WeatherForecast&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<WeatherForecast>> GetByCityAsync(Guid cityId)
        {
            var responseString = await _httpClient.GetStringAsync($"/weather/{cityId}")
                                                  .ConfigureAwait(false);

            var weatherForecast = JsonConvert.DeserializeObject<List<WeatherForecast>>(responseString);
            return weatherForecast;
        }
    }
}
