using Pelorus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Pelorus.Core.Data
{
    /// <summary>
    /// Defines the base functionality of all data repositories with read only entities.
    /// </summary>
    public interface IBaseReadOnlyRepository : IDisposable
    {
        /// <summary>
        /// Returns an entity by Id.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <typeparam name="TKey">Type of the entity's key.</typeparam>
        /// <param name="entityId">Id of the entity to get.</param>
        /// <returns>The entity with the given Id or null if the entity does not exist.</returns>
        TEntity GetById<TEntity, TKey>(TKey entityId)
            where TEntity : Entity<TKey>
            where TKey : struct;

        /// <summary>
        /// Returns an entity by Id asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <typeparam name="TKey">Type of the entity's key.</typeparam>
        /// <param name="entityId">Id of the entity to get.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>The entity with the given Id or null if the entity does not exist.</returns>
        Task<TEntity> GetByIdAsync<TEntity, TKey>(TKey entityId, CancellationToken cancellationToken)
            where TEntity : Entity<TKey>
            where TKey : struct;

        /// <summary>
        /// Returns an entity based on a search predicate.  The predicate must only select a single entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <param name="predicate">Search predicate for the operations.</param>
        /// <returns>The entity that satisfies the search predicate's criteria or null if the entity does not exist.</returns>
        TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;

        /// <summary>
        /// Returns an entity based on a search predicate asynchronously.  The predicate must only select a single entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <param name="predicate">Search predicate for the operations.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>The entity that satisfies the search predicate's criteria or null if the entity does not exist.</returns>
        Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            where TEntity : class;

        /// <summary>
        /// Get a collection of 'length' entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <param name="length">Number of items to return.</param>
        /// <returns>Collection of entities.</returns>
        IEnumerable<TEntity> GetCount<TEntity>(int length)
            where TEntity : class;

        /// <summary>
        /// Get a collection of 'length' entities starting at the position of 'startIndex'.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <param name="startIndex">Position in the full collection to select from.</param>
        /// <param name="length">Number of items to return.</param>
        /// <returns>Collection of entities.</returns>
        IEnumerable<TEntity> GetCount<TEntity>(int startIndex, int length)
            where TEntity : class;

        /// <summary>
        /// Get a collection of 'length' entities by the search predicate.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <param name="length">Number of items to return.</param>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        IEnumerable<TEntity> GetCount<TEntity>(int length, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;

        /// <summary>
        /// Get a collection of 'length' entities by the search predicate starting at the position of 'startIndex'.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <param name="startIndex">Position in the full collection to select from.</param>
        /// <param name="length">Number of items to return.</param>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        IEnumerable<TEntity> GetCount<TEntity>(int startIndex, int length, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;

        /// <summary>
        /// Get a collection of 'length' entities asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <param name="length">Number of items to return.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities.</returns>
        Task<IEnumerable<TEntity>> GetCountAsync<TEntity>(int length, CancellationToken cancellationToken)
            where TEntity : class;

        /// <summary>
        /// Get a collection of 'length' entities starting at the position of 'startIndex' asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <param name="startIndex">Position in the full collection to select from.</param>
        /// <param name="length">Number of items to return.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities.</returns>
        Task<IEnumerable<TEntity>> GetCountAsync<TEntity>(int startIndex, int length, CancellationToken cancellationToken)
            where TEntity : class;

        /// <summary>
        /// Get a collection of 'length' entities by the search predicate asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <param name="length">Number of items to return.</param>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        Task<IEnumerable<TEntity>> GetCountAsync<TEntity>(int length, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            where TEntity : class;

        /// <summary>
        /// Get a collection of 'length' entities by the search predicate starting at the position of 'startIndex' asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <param name="startIndex">Position in the full collection to select from.</param>
        /// <param name="length">Number of items to return.</param>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        Task<IEnumerable<TEntity>> GetCountAsync<TEntity>(int startIndex, int length, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            where TEntity : class;

        /// <summary>
        /// Returns all the entities in the data store.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <returns>Collection of all entities in the data store.</returns>
        IEnumerable<TEntity> GetAll<TEntity>()
            where TEntity : class;

        /// <summary>
        /// Returns all the entities in the data store asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of all entities in the data store.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(CancellationToken cancellationToken)
            where TEntity : class;

        /// <summary>
        /// Returns a collection of entities based on a search predicate.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <param name="predicate">Search predicate for the operations.</param>
        /// <returns>Collection of entities that satisfy the search predicate's criteria.</returns>
        IEnumerable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class;

        /// <summary>
        /// Returns a collection of entities based on a search predicate asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to query.</typeparam>
        /// <param name="predicate">Search predicate for the operations.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities that satisfy the search predicate's criteria.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            where TEntity : class;
    }
}
