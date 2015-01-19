using Pelorus.Core.Entities;
using Pelorus.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Pelorus.Core.Data.EntityFramework
{
    /// <summary>
    /// Base functionality for readonly repository classes.
    /// </summary>
    /// <typeparam name="TEntity">The repository's entity type.</typeparam>
    /// <typeparam name="TKey">Type of the Id property on TEntity.</typeparam>
    public abstract class BaseReadOnlyRepository<TEntity, TKey> : IBaseReadOnlyRepository<TEntity, TKey>
        where TEntity : EntityDao<TKey>
        where TKey : struct
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dataSet;

        /// <summary>
        /// Expose the data set for the repository to the inheriting class.
        /// </summary>
        protected virtual DbSet<TEntity> DataSet
        {
            get { return this._dataSet; }
        }

        /// <summary>
        /// Expression for including child entities in the base queries.
        /// </summary>
        protected virtual Func<IQueryable<TEntity>, IQueryable<TEntity>> IncludeExpression { get; set; }

        /// <summary>
        /// Initialize the base properties of the repository class.
        /// </summary>
        /// <param name="contextFactory">Context factory to create the data context.</param>
        protected BaseReadOnlyRepository(IContextFactory contextFactory)
        {
            if (null == contextFactory)
            {
                throw new ArgumentNullException("contextFactory");
            }

            this._context = contextFactory.Create();
            this._dataSet = this._context.Set<TEntity>();
        }

        /// <summary>
        /// Initialize the base properties of the repository class.
        /// </summary>
        /// <param name="context">Context to use for the respository.</param>
        protected BaseReadOnlyRepository(DbContext context)
        {
            if (null == context)
            {
                throw new ArgumentNullException("context");
            }

            this._context = context;
            this._dataSet = this._context.Set<TEntity>();
        }

        /// <summary>
        /// Get an entity by Id.
        /// </summary>
        /// <param name="entityId">Id of the entity to get.</param>
        /// <returns>Entity with the given Id or null if the entity does not exist.</returns>
        public TEntity GetById(TKey entityId)
        {
            var parameterExpr = Expression.Parameter(typeof (TEntity));
            var entityIdExpr = Expression.Property(parameterExpr, PropertyInfoExtensions.Property<TEntity, TKey>(e => e.Id));
            var constIdExpr = Expression.Constant(entityId, typeof (TKey));
            var whereClause = Expression.Equal(entityIdExpr, constIdExpr);
            var predicate = Expression.Lambda<Func<TEntity, bool>>(whereClause, parameterExpr);

            var query = this.DataSet.Where(predicate);
            var includeQuery = this.IncludeChildren(query);

            return includeQuery.SingleOrDefault();
        }

        /// <summary>
        /// Get an entity by Id asynchronously.
        /// </summary>
        /// <param name="entityId">Id of the entity to get.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Entity with the given Id or null if the entity does not exist.</returns>
        public async Task<TEntity> GetByIdAsync(TKey entityId, CancellationToken cancellationToken)
        {
            var parameterExpr = Expression.Parameter(typeof (TEntity));
            var entityIdExpr = Expression.Property(parameterExpr, PropertyInfoExtensions.Property<TEntity, TKey>(e => e.Id));
            var constIdExpr = Expression.Constant(entityId, typeof (TKey));
            var whereClause = Expression.Equal(entityIdExpr, constIdExpr);
            var predicate = Expression.Lambda<Func<TEntity, bool>>(whereClause, parameterExpr);

            var query = this.DataSet.Where(predicate);
            var includeQuery = this.IncludeChildren(query);

            return await includeQuery.SingleOrDefaultAsync(cancellationToken)
                                     .ConfigureAwait(false);
        }

        /// <summary>
        /// Get a single entity by the search predicate.
        /// </summary>
        /// <param name="predicate">Search predicate for querying the entity.</param>
        /// <returns>Entity that match the search predicate.</returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            var query = this.DataSet.Where(predicate);
            var includeQuery = this.IncludeChildren(query);

            return includeQuery.SingleOrDefault();
        }

        /// <summary>
        /// Get a single entity by the search predicate.
        /// </summary>
        /// <param name="predicate">Search predicate for querying the entity.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Entity that match the search predicate.</returns>
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var query = this.DataSet.Where(predicate);
            var includeQuery = this.IncludeChildren(query);

            return await includeQuery.SingleOrDefaultAsync(cancellationToken)
                                     .ConfigureAwait(false);
        }

        /// <summary>
        /// Get a collection of entities
        /// </summary>
        /// <returns>Collection of entities.</returns>
        public IEnumerable<TEntity> GetAll()
        {
            var query = this.DataSet.AsQueryable();
            var includeQuery = this.IncludeChildren(query);

            return includeQuery.ToList();
        }

        /// <summary>
        /// Get a collection of entities by the search predicate.
        /// </summary>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            var query = this.DataSet.Where(predicate);
            var includeQuery = this.IncludeChildren(query);

            return includeQuery.ToList();
        }

        /// <summary>
        /// Get a collection of entities asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities.</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            var query = this.DataSet.AsQueryable();
            var includeQuery = this.IncludeChildren(query);

            return await includeQuery.ToListAsync(cancellationToken)
                                     .ConfigureAwait(false);
        }

        /// <summary>
        /// Get a collection of entities by the search predicate asynchronously.
        /// </summary>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var query = this.DataSet.Where(predicate);
            var includeQuery = this.IncludeChildren(query);

            return await includeQuery.ToListAsync(cancellationToken)
                                     .ConfigureAwait(false);
        }

        /// <summary>
        /// Include children entities in the query using the IncludeExpression.
        /// </summary>
        /// <param name="subject">Query to include the child items with.</param>
        /// <returns>Query including the child entities.</returns>
        protected IQueryable<TEntity> IncludeChildren(IQueryable<TEntity> subject)
        {
            if (null == this.IncludeExpression)
            {
                return subject;
            }

            return this.IncludeExpression.Invoke(subject);
        }

        /// <summary>
        /// Dispose the resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose the resources.
        /// </summary>
        /// <param name="disposing">Indicates if the managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._context.Dispose();
            }
        }
    }

    /// <summary>
    /// Short hand base repository for TEntity types that have an int Id property.
    /// </summary>
    /// <typeparam name="TEntity">The repository's entity type.</typeparam>
    public abstract class BaseReadOnlyRepository<TEntity> : BaseReadOnlyRepository<TEntity, int>
        where TEntity : EntityDao<int>
    {
        /// <summary>
        /// Create a new instance of the base repository class with a context factory.
        /// </summary>
        /// <param name="contextFactory">Context factory to create the data context.</param>
        protected BaseReadOnlyRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }

        /// <summary>
        /// Initialize the base properties of the repository class.
        /// </summary>
        /// <param name="context">Context to use for the respository.</param>
        protected BaseReadOnlyRepository(DbContext context) : base(context)
        {
        }
    }
}
