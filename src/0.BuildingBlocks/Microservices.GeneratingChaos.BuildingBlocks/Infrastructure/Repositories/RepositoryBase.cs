using Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.SeedWork;
using MongoDB.Driver;

namespace Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.Repositories
{
    public class RepositoryBase<TEntity> where TEntity : Entity
    {
        protected readonly IMongoCollection<TEntity> _collection;

        /// <summary>
        /// Initializes a new instance of the repository base class.
        /// </summary>
        /// <param name="mongoCollection">The mongo collection.</param>
        protected RepositoryBase(IMongoCollection<TEntity> mongoCollection)
        {
            _collection = mongoCollection;
        }
    }
}
