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
    /// <typeparam name="TEntity">Type of the repository's entity.</typeparam>
    /// <typeparam name="TKey">Type of the repository's entity's key.</typeparam>
    public interface IBaseReadOnlyRepository<TEntity, in TKey> : IDisposable
        where TEntity : EntityDao<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Returns an entity by Id.
        /// </summary>
        /// <param name="entityId">Id of the entity to get.</param>
        /// <returns>The entity with the given Id or null if the entity does not exist.</returns>
        TEntity GetById(TKey entityId);

        /// <summary>
        /// Returns an entity by Id asynchronously.
        /// </summary>
        /// <param name="entityId">Id of the entity to get.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>The entity with the given Id or null if the entity does not exist.</returns>
        Task<TEntity> GetByIdAsync(TKey entityId, CancellationToken cancellationToken);

        /// <summary>
        /// Returns an entity based on a search predicate.  The predicate must only select a single entity.
        /// </summary>
        /// <param name="predicate">Search predicate for the operations.</param>
        /// <returns>The entity that satisfies the search predicate's criteria or null if the entity does not exist.</returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns an entity based on a search predicate asynchronously.  The predicate must only select a single entity.
        /// </summary>
        /// <param name="predicate">Search predicate for the operations.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>The entity that satisfies the search predicate's criteria or null if the entity does not exist.</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Get a collection of 'length' entities.
        /// </summary>
        /// <param name="length">Number of items to return.</param>
        /// <returns>Collection of entities.</returns>
        IEnumerable<TEntity> GetCount(int length);

        /// <summary>
        /// Get a collection of 'length' entities starting at the position of 'startIndex'.
        /// </summary>
        /// <param name="startIndex">Position in the full collection to select from.</param>
        /// <param name="length">Number of items to return.</param>
        /// <returns>Collection of entities.</returns>
        IEnumerable<TEntity> GetCount(int startIndex, int length);

        /// <summary>
        /// Get a collection of 'length' entities by the search predicate.
        /// </summary>
        /// <param name="length">Number of items to return.</param>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        IEnumerable<TEntity> GetCount(int length, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Get a collection of 'length' entities by the search predicate starting at the position of 'startIndex'.
        /// </summary>
        /// <param name="startIndex">Position in the full collection to select from.</param>
        /// <param name="length">Number of items to return.</param>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        IEnumerable<TEntity> GetCount(int startIndex, int length, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Get a collection of 'length' entities asynchronously.
        /// </summary>
        /// <param name="length">Number of items to return.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities.</returns>
        Task<IEnumerable<TEntity>> GetCountAsync(int length, CancellationToken cancellationToken);

        /// <summary>
        /// Get a collection of 'length' entities starting at the position of 'startIndex' asynchronously.
        /// </summary>
        /// <param name="startIndex">Position in the full collection to select from.</param>
        /// <param name="length">Number of items to return.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities.</returns>
        Task<IEnumerable<TEntity>> GetCountAsync(int startIndex, int length, CancellationToken cancellationToken);

        /// <summary>
        /// Get a collection of 'length' entities by the search predicate asynchronously.
        /// </summary>
        /// <param name="length">Number of items to return.</param>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        Task<IEnumerable<TEntity>> GetCountAsync(int length, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Get a collection of 'length' entities by the search predicate starting at the position of 'startIndex' asynchronously.
        /// </summary>
        /// <param name="startIndex">Position in the full collection to select from.</param>
        /// <param name="length">Number of items to return.</param>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        Task<IEnumerable<TEntity>> GetCountAsync(int startIndex, int length, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Returns all the entities in the data store.
        /// </summary>
        /// <returns>Collection of all entities in the data store.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Returns all the entities in the data store asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of all entities in the data store.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Returns a collection of entities based on a search predicate.
        /// </summary>
        /// <param name="predicate">Search predicate for the operations.</param>
        /// <returns>Collection of entities that satisfy the search predicate's criteria.</returns>
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns a collection of entities based on a search predicate asynchronously.
        /// </summary>
        /// <param name="predicate">Search predicate for the operations.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities that satisfy the search predicate's criteria.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Defines the base functionality of data repositories that have a read only entity with a key of type int.
    /// </summary>
    /// <typeparam name="TEntity">Type of the repository's entity.</typeparam>
    public interface IBaseReadOnlyRepository<TEntity> : IBaseReadOnlyRepository<TEntity, long>
        where TEntity : EntityDao<long>
    {
    }
}
