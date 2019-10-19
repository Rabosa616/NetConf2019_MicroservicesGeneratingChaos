using Microservices.GeneratingChaos.Services.Api.Domain.Models;
using Microservices.GeneratingChaos.Services.Api.Infrastructure.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microservices.GeneratingChaos.Services.Api.Infrastructure.Services
{
    /// <summary>
    /// Class SunService.
    /// Implements the <see cref="Microservices.GeneratingChaos.Services.Api.Infrastructure.Services.Interfaces.ISunService" />
    /// </summary>
    /// <seealso cref="Microservices.GeneratingChaos.Services.Api.Infrastructure.Services.Interfaces.ISunService" />
    public class SunService : ISunService
    {
        /// <summary>
        /// The HTTP client
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SunService" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <exception cref="ArgumentNullException">httpClient</exception>
        public SunService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Gets the by city asynchronous.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>Task&lt;Sun&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Sun> GetByCityAsync(Guid cityId)
        {
            var responseString = await _httpClient.GetStringAsync($"v1/sun/{cityId}")
                                                          .ConfigureAwait(false);

            var sunResponse = JsonConvert.DeserializeObject<Sun>(responseString);
            return sunResponse;
        }
    }
}
