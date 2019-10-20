using Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.SeedWork;
using MongoDB.Driver;

namespace Microservices.GeneratingChaos.BuildingBlocks.Infrastructure.Repositories
{
    /// <summary>
    /// Class RepositoryBase.
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    public class RepositoryBase<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// The collection
        /// </summary>
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
