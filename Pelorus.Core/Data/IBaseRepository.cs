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
    public interface IBaseRepository : IBaseReadOnlyRepository
    {
        /// <summary>
        /// Create a new entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being created.</typeparam>
        /// <param name="entity">New entity to create in the data store.</param>
        /// <returns>The new entity with any generated properties.</returns>
        TEntity Create<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// Create a new entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being created.</typeparam>
        /// <param name="entity">New entity to create in the data store.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>The new entity with any generated properties.</returns>
        Task<TEntity> CreateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TEntity : class;

        /// <summary>
        /// Delete an entity from the data store.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being deleted.</typeparam>
        /// <param name="entity">Entity to delete from the data store.</param>
        /// <returns>Deleted entity.</returns>
        TEntity Delete<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// Delete an entity from the data store asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being deleted.</typeparam>
        /// <param name="entity">Entity to delete from the data store.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Deleted entity.</returns>
        Task<TEntity> DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TEntity : class;

        /// <summary>
        /// Delete an entity from the data store by Id.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being deleted.</typeparam>
        /// <typeparam name="TKey">Type of the key of the entity being deleted.</typeparam>
        /// <param name="entityId">Id of the entity to delete from the data store.</param>
        /// <returns>True if the entity was found and deleted.</returns>
        bool DeleteById<TEntity, TKey>(TKey entityId)
            where TEntity : Entity<TKey>
            where TKey : struct;

        /// <summary>
        /// Delete an entity from the data store by Id asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being deleted.</typeparam>
        /// <typeparam name="TKey">Type of the key of the entity being deleted.</typeparam>
        /// <param name="entityId">Id of the entity to delete from the data store.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>True if the entity was found and deleted.</returns>
        Task<bool> DeleteByIdAsync<TEntity, TKey>(TKey entityId, CancellationToken cancellationToken)
            where TEntity : Entity<TKey>
            where TKey : struct;

        /// <summary>
        /// Update an existing entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being updated.</typeparam>
        /// <param name="entity">Updaterd entity to update in the data store.</param>
        /// <returns>The updated entity.</returns>
        TEntity Update<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// Update the identified properties on an existing entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being updated.</typeparam>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <returns>Updated entity.</returns>
        TEntity Update<TEntity>(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> modifiedProperties)
            where TEntity : class;

        /// <summary>
        /// Update the identified properties on an existing entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being updated.</typeparam>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <returns>Updated entity.</returns>
        TEntity Update<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] modifiedProperties)
            where TEntity : class;

        /// <summary>
        /// Update an existing entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being updated.</typeparam>
        /// <param name="entity">Updated entity to update in the data store.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>The updated entity.</returns>
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TEntity : class;

        /// <summary>
        /// Update the identified properties on an existing entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being updated.</typeparam>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Updated entity.</returns>
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> modifiedProperties, CancellationToken cancellationToken)
            where TEntity : class;

        /// <summary>
        /// Update the identified properties on an existing entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being updated.</typeparam>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <returns>Updated entity.</returns>
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] modifiedProperties)
            where TEntity : class;
    }
}
