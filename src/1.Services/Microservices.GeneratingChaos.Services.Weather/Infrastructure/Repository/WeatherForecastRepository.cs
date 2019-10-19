
using Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.Generators.Interfaces;
using Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.Repositories;
using Microservices.GeneratingChaos.Services.Weather.Domain.Entities;
using Microservices.GeneratingChaos.Services.Weather.Infrastructure.Repository.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.GeneratingChaos.Services.Weather.Infrastructure.Repository
{
    /// <inheridoc />
    public class WeatherForecastRepository : RepositoryBase<WeatherForecast>, IWeatherRepository
    {
        private readonly IIdGenerator _idGenerator;
        private readonly IDate _date;

        public WeatherForecastRepository(IMongoCollection<WeatherForecast> mongoCollection,
                              IIdGenerator idGenerator,
                              IDate date) : base(mongoCollection)
        {
            _idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
            _date = date ?? throw new ArgumentNullException(nameof(date));
        }

        /// <inheridoc />
        public IMongoQueryable<WeatherForecast> Query { get => _collection.AsQueryable<WeatherForecast>(new AggregateOptions { AllowDiskUse = true }); }

        /// <inheridoc />
        public async Task<WeatherForecast> AddAsync(WeatherForecast entity)
        {
            entity.Id = _idGenerator.GenerateNewGuidId();
            entity.Created = _date.Now();
            entity.Modified = entity.Created;
            await _collection.InsertOneAsync(entity).ConfigureAwait(false);
            return entity;
        }

        /// <inheridoc />
        public async Task AddManyAsync(IEnumerable<WeatherForecast> entities)
        {
            foreach (var entity in entities)
            {
                var find = await _collection.FindAsync(item => item.CityId == entity.CityId &&
                                                               item.Date == entity.Date).ConfigureAwait(false);
                var findEntities = await find.ToListAsync();

                if (findEntities == null || !findEntities.Any())
                {
                    await AddAsync(entity).ConfigureAwait(false);
                }
                else
                {
                    await UpdateAsync(entity).ConfigureAwait(false);
                }
            }
        }

        /// <inheridoc />
        public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
        {
            var cursor = await _collection.FindAsync(c => true).ConfigureAwait(false);
            return await cursor.ToListAsync().ConfigureAwait(false);
        }

        /// <inheridoc />
        public async Task<WeatherForecast> GetByIdAsync(Guid id)
        {
            var cursor = await _collection.FindAsync(c => c.CityId == id).ConfigureAwait(false);
            return await cursor.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        /// <inheridoc />
        public async Task RemoveAsync(Guid id)
        {
            await _collection.DeleteManyAsync(c => c.CityId == id).ConfigureAwait(false);
        }

        /// <inheridoc />
        public async Task UpdateAsync(WeatherForecast entity)
        {
            entity.Modified = _date.Now();
            await _collection.FindOneAndReplaceAsync(c => c.Id == entity.Id, entity).ConfigureAwait(false);
        }
    }
}
