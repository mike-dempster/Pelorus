using Pelorus.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Pelorus.Core.Data
{
    /// <summary>
    /// Defines the base functionality of all data repositories.
    /// </summary>
    /// <typeparam name="TEntity">Type of the repository's entity.</typeparam>
    /// <typeparam name="TKey">Type of the repository's entity's key.</typeparam>
    public interface IBaseRepository<TEntity, in TKey> : IBaseReadOnlyRepository<TEntity, TKey>
        where TEntity : EntityDao<TKey>
        where TKey : struct
    {
        /// <summary>
        /// Create a new entity.
        /// </summary>
        /// <param name="entity">New entity to create in the data store.</param>
        /// <returns>The new entity with any generated properties.</returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// Create a new entity asynchronously.
        /// </summary>
        /// <param name="entity">New entity to create in the data store.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>The new entity with any generated properties.</returns>
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Delete an entity from the data store.
        /// </summary>
        /// <param name="entity">Entity to delete from the data store.</param>
        /// <returns>Deleted entity.</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// Delete an entity from the data store asynchronously.
        /// </summary>
        /// <param name="entity">Entity to delete from the data store.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Deleted entity.</returns>
        Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Delete an entity from the data store by Id.
        /// </summary>
        /// <param name="entityId">Id of the entity to delete from the data store.</param>
        /// <returns>True if the entity was found and deleted.</returns>
        bool DeleteById(TKey entityId);

        /// <summary>
        /// Delete an entity from the data store by Id asynchronously.
        /// </summary>
        /// <param name="entityId">Id of the entity to delete from the data store.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>True if the entity was found and deleted.</returns>
        Task<bool> DeleteByIdAsync(TKey entityId, CancellationToken cancellationToken);

        /// <summary>
        /// Update an existing entity.
        /// </summary>
        /// <param name="entity">Updated entity to save in the data store.</param>
        /// <returns>The updated entity.</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Update the identifiied properties on an existing entity.
        /// </summary>
        /// <param name="entity">Updated entity to save in the data store.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <returns>Updated entity.</returns>
        TEntity Update(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> modifiedProperties);

        /// <summary>
        /// Update an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">Updated entity to save in the data store.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>The updated entity.</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Update the identifiied properties on an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">Updated entity to save in the data store.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Updated entity.</returns>
        Task<TEntity> UpdateAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> modifiedProperties, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Defines the base functionality of all data repositories that have an entity with a key of type int.
    /// </summary>
    /// <typeparam name="TEntity">Type of the repository's entity.</typeparam>
    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, int>
        where TEntity : EntityDao
    {
    }
}
