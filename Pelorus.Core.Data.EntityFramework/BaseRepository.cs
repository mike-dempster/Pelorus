using Pelorus.Core.Entities;
using Pelorus.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Pelorus.Core.Data.EntityFramework
{
    /// <summary>
    /// Base functionality for repository classes.
    /// </summary>
    /// <typeparam name="TEntity">The repository's entity type.</typeparam>
    /// <typeparam name="TKey">Type of the Id property on TEntity.</typeparam>
    public abstract class BaseRepository<TEntity, TKey> : BaseReadOnlyRepository<TEntity, TKey>, IBaseRepository<TEntity, TKey>
        where TEntity : EntityDao<TKey>
        where TKey : struct
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dataSet;

        /// <summary>
        /// Expose the data set for the repository to the inheriting class.
        /// </summary>
        protected override DbSet<TEntity> DataSet
        {
            get { return this._dataSet; }
        }

        /// <summary>
        /// Expression for including child entities in the base queries.
        /// </summary>
        protected override Func<IQueryable<TEntity>, IQueryable<TEntity>> IncludeExpression { get; set; }

        /// <summary>
        /// Initialize the base properties of the repository class.
        /// </summary>
        /// <param name="contextFactory">Context factory to create the data context.</param>
        protected BaseRepository(IContextFactory contextFactory) : this(CreateContext(contextFactory))
        {
            if (null == contextFactory)
            {
                throw new ArgumentNullException("contextFactory");
            }
        }

        /// <summary>
        /// Initialize the base properties of the repository class.
        /// </summary>
        /// <param name="context">Context to use for the respository.</param>
        protected BaseRepository(DbContext context) : base(context)
        {
            if (null == context)
            {
                throw new ArgumentNullException("context");
            }

            this._context = context;
            this._dataSet = this._context.Set<TEntity>();
        }

        /// <summary>
        /// Validate the context factory and create a new context.
        /// </summary>
        /// <param name="contextFactory">Context factory to use to create the new context.</param>
        /// <returns>new instance of the context from the context factory.</returns>
        private static DbContext CreateContext(IContextFactory contextFactory)
        {
            if (null == contextFactory)
            {
                throw new ArgumentNullException("contextFactory");
            }

            return contextFactory.Create();
        }

        /// <summary>
        /// Create a new entity.
        /// </summary>
        /// <param name="entity">New entity to create.</param>
        /// <returns>The new entity with the generated Id.</returns>
        public TEntity Create(TEntity entity)
        {
            var newEntity = this.DataSet.Add(entity);
            this.SaveChanges();

            return newEntity;
        }

        /// <summary>
        /// Create a new entity asynchronously.
        /// </summary>
        /// <param name="entity">New entity to create.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>The new entity with the generated Id.</returns>
        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var newEntity = this.DataSet.Add(entity);
            await this.SaveChangesAsync(cancellationToken)
                      .ConfigureAwait(false);

            return newEntity;
        }

        /// <summary>
        /// Delete an existing entity.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        /// <returns>Deleted entity.</returns>
        public TEntity Delete(TEntity entity)
        {
            var deletedEntity = this.DataSet.Remove(entity);
            this.SaveChanges();

            return deletedEntity;
        }

        /// <summary>
        /// Delete an existing entity.
        /// </summary>
        /// <param name="entity">Entity to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Deleted entity.</returns>
        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var deletedEntity = this.DataSet.Remove(entity);
            await this.SaveChangesAsync(cancellationToken)
                      .ConfigureAwait(false);

            return deletedEntity;
        }

        /// <summary>
        /// Delete entity by Id.
        /// </summary>
        /// <param name="entityId">Id of the entity to delete.</param>
        /// <returns>True if the entity was found and deleted or false if the entity was not found.</returns>
        public bool DeleteById(TKey entityId)
        {
            var parameterExpr = Expression.Parameter(typeof(TEntity));
            var entityIdExpr = Expression.Property(parameterExpr, PropertyInfoExtensions.Property<TEntity, TKey>(e => e.Id));
            var constIdExpr = Expression.Constant(entityId, typeof(TKey));
            var whereClause = Expression.Equal(entityIdExpr, constIdExpr);
            var predicate = Expression.Lambda<Func<TEntity, bool>>(whereClause, parameterExpr);

            var entity = this.DataSet.SingleOrDefault(predicate);

            if (null == entity)
            {
                return false;
            }

            this.Entry(entity).State = EntityState.Deleted;
            this.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete entity by Id.
        /// </summary>
        /// <param name="entityId">Id of the entity to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>True if the entity was found and deleted or false if the entity was not found.</returns>
        public async Task<bool> DeleteByIdAsync(TKey entityId, CancellationToken cancellationToken)
        {
            var entity = await this.GetByIdAsync(entityId, cancellationToken)
                                   .ConfigureAwait(false);

            if (null == entity)
            {
                return false;
            }

            this.DataSet.Remove(entity);
            await this.SaveChangesAsync(cancellationToken)
                      .ConfigureAwait(false);

            return true;
        }

        /// <summary>
        /// Update an existing entity.
        /// </summary>
        /// <param name="entity">Updated entity to save.</param>
        /// <returns>Updated entity.</returns>
        public TEntity Update(TEntity entity)
        {
            var contextEntity = this.DataSet.Find(entity.Id);
            var entry = this.Entry(contextEntity);
            entry.CurrentValues.SetValues(entity);
            entry.State = EntityState.Modified;
            this.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Update the identifiied properties on an existing entity.
        /// </summary>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <returns>Updated entity.</returns>
        public TEntity Update(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> modifiedProperties)
        {
            var contextEntity = this.DataSet.Find(entity.Id);

            if (null == contextEntity)
            {
                string exMsg = string.Format(CultureInfo.CurrentCulture, "Entity with Id '{0}' does not exist.", entity.Id);
                throw new DataException(exMsg);
            }

            var entry = this.Entry(contextEntity);
            entry.CurrentValues.SetValues(entity);
            entry.State = EntityState.Modified;
            string idPropertyName = this.GetPropertyName(e => e.Id);
            var modifiedPropertyNames = modifiedProperties.Select(this.GetPropertyName)
                                                          .Where(e => null != e)
                                                          .ToList();

            foreach (var p in entry.CurrentValues.PropertyNames)
            {
                if (p == idPropertyName)
                {
                    continue;
                }

                entry.Property(p).IsModified = modifiedPropertyNames.Contains(p);
            }

            this.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Update an existing entity.
        /// </summary>
        /// <param name="entity">Entity with update to save.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Updated entity.</returns>
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var contextEntity = await this.DataSet.FindAsync(cancellationToken, entity.Id)
                                          .ConfigureAwait(false);
            var entry = this.Entry(contextEntity);
            entry.CurrentValues.SetValues(entity);
            entry.State = EntityState.Modified;
            await this.SaveChangesAsync(cancellationToken)
                      .ConfigureAwait(false);

            return entity;
        }

        /// <summary>
        /// Update the identifiied properties on an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Updated entity.</returns>
        public async Task<TEntity> UpdateAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> modifiedProperties, CancellationToken cancellationToken)
        {
            var contextEntity = await this.DataSet.FindAsync(cancellationToken, entity.Id)
                                          .ConfigureAwait(false);
            var entry = this.Entry(contextEntity);
            entry.CurrentValues.SetValues(entity);
            entry.State = EntityState.Modified;
            string idPropertyName = this.GetPropertyName(e => e.Id);
            var modifiedPropertyNames = modifiedProperties.Select(this.GetPropertyName)
                                                          .Where(e => null != e)
                                                          .ToList();

            foreach (var p in entry.CurrentValues.PropertyNames)
            {
                if (p == idPropertyName)
                {
                    continue;
                }

                entry.Property(p).IsModified = modifiedPropertyNames.Contains(p);
            }

            await this.SaveChangesAsync(cancellationToken)
                      .ConfigureAwait(false);

            return entity;
        }

        /// <summary>
        /// Execute a SQL query on the database.
        /// </summary>
        /// <typeparam name="T">Type of the records that will be returned from the query.</typeparam>
        /// <param name="sqlQuery">SQL query to execute on the database.</param>
        /// <param name="args">Arguments for the SQL query.</param>
        /// <returns>Results of the SQL query.</returns>
        protected DbRawSqlQuery<T> ExecuteSqlQuery<T>(string sqlQuery, params object[] args)
        {
            return this._context.Database.SqlQuery<T>(sqlQuery, args);
        }

        /// <summary>
        /// Execute a SQL command in the database.
        /// </summary>
        /// <param name="sqlQuery">SQL command to execute.</param>
        /// <param name="args">Arguments for the SQL command.</param>
        /// <returns>Number of rows that were affected by the SQL command.</returns>
        protected int ExecuteSqlCommand(string sqlQuery, params object[] args)
        {
            return this._context.Database.ExecuteSqlCommand(sqlQuery, args);
        }

        /// <summary>
        /// Execute a SQL command in the database.
        /// </summary>
        /// <param name="sqlQuery">SQL command to execute.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <param name="args">Arguments for the SQL command.</param>
        /// <returns>Number of rows that were affected by the SQL command.</returns>
        protected async Task<int> ExecuteSqlCommandAsync(string sqlQuery, CancellationToken cancellationToken, params object[] args)
        {
            return await this._context.Database.ExecuteSqlCommandAsync(sqlQuery, cancellationToken, args)
                             .ConfigureAwait(false);
        }

        /// <summary>
        /// Save the changes in the underlying data context.
        /// </summary>
        protected virtual void SaveChanges()
        {
            this._context.SaveChanges();
        }

        /// <summary>
        /// Save the changes in the underlying data context.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns></returns>
        protected async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await this._context.SaveChangesAsync(cancellationToken)
                      .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets an entity entry information from the underlying data context.
        /// </summary>
        /// <param name="entity">Entity to get the entry information for.</param>
        /// <returns>Entry information for the entity.</returns>
        protected DbEntityEntry<TEntity> Entry(TEntity entity)
        {
            return this._context.Entry(entity);
        }

        /// <summary>
        /// Gets an entity entry information from the underlying data context.
        /// </summary>
        /// <param name="entity">Entity to get the entry information for.</param>
        /// <returns>Entry information for the entity.</returns>
        protected DbEntityEntry Entry(object entity)
        {
            return this._context.Entry(entity);
        }

        // ReSharper disable RedundantOverridenMember
        /// <summary>
        /// Dispose the resources.
        /// </summary>
        /// <param name="disposing">Indicates if the managed resources should be disposed.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._context.Dispose();
            }

            base.Dispose(disposing);
        }

        // ReSharper restore RedundantOverridenMember

        /// <summary>
        /// Get the name of a property defined in type TEntity.
        /// </summary>
        /// <param name="propertyExpression">Expression identifying the property to get the name of.</param>
        /// <returns>Name of the property or null if the expression is null or the property expression type is not supported.</returns>
        private string GetPropertyName(Expression<Func<TEntity, object>> propertyExpression)
        {
            if (null == propertyExpression)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var propertyInfo = PropertyInfoExtensions.Property(propertyExpression);

            if (null == propertyInfo)
            {
                return null;
            }

            return propertyInfo.Name;
        }
    }

    /// <summary>
    /// Short hand base repository for TEntity types that have an int Id property.
    /// </summary>
    /// <typeparam name="TEntity">The repository's entity type.</typeparam>
    public abstract class BaseRepository<TEntity> : BaseRepository<TEntity, int>
        where TEntity : EntityDao<int>
    {
        /// <summary>
        /// Create a new instance of the base repository class with a context factory.
        /// </summary>
        /// <param name="contextFactory">Context factory to create the data context.</param>
        protected BaseRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        /// <summary>
        /// Initialize the base properties of the repository class.
        /// </summary>
        /// <param name="context">Context to use for the respository.</param>
        protected BaseRepository(DbContext context) : base(context)
        {
        }
    }
}
