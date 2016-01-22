using Pelorus.Core.Entities;
using Pelorus.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Pelorus.Core.Data.EntityFramework
{
    /// <summary>
    /// Base functionality for repository classes with read only entities.
    /// </summary>
    /// <typeparam name="TEntity">The repository's entity type.</typeparam>
    /// <typeparam name="TKey">Type of the Id property on TEntity.</typeparam>
    public abstract class BaseReadOnlyRepository<TEntity, TKey> : IBaseReadOnlyRepository<TEntity, TKey>
        where TEntity : EntityDao<TKey>
        where TKey : struct
    {
        private readonly IContextFactory _contextFactory;

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
                throw new ArgumentNullException(nameof(contextFactory));
            }

            this._contextFactory = contextFactory;
        }

        /// <summary>
        /// Disposes of resources that are only referenced internally.
        /// </summary>
        ~BaseReadOnlyRepository()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Get an entity by Id.
        /// </summary>
        /// <param name="entityId">Id of the entity to get.</param>
        /// <returns>Entity with the given Id or null if the entity does not exist.</returns>
        public virtual TEntity GetById(TKey entityId)
        {
            using (var context = this.GetContextInstance())
            {
                var predicate = this.GetKeyEqualityExpression(entityId);
                var dbSet = context.Set<TEntity>();
                var query = dbSet.Where(predicate);
                var includeQuery = this.IncludeChildren(query);

                return includeQuery.SingleOrDefault();
            }
        }

        /// <summary>
        /// Get an entity by Id asynchronously.
        /// </summary>
        /// <param name="entityId">Id of the entity to get.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Entity with the given Id or null if the entity does not exist.</returns>
        public virtual async Task<TEntity> GetByIdAsync(TKey entityId, CancellationToken cancellationToken)
        {
            using (var context = this.GetContextInstance())
            {
                var predicate = this.GetKeyEqualityExpression(entityId);
                var dbSet = context.Set<TEntity>();
                var query = dbSet.Where(predicate);
                var includeQuery = this.IncludeChildren(query);

                return await includeQuery.SingleOrDefaultAsync(cancellationToken)
                                         .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get a single entity by the search predicate.
        /// </summary>
        /// <param name="predicate">Search predicate for querying the entity.</param>
        /// <returns>Entity that match the search predicate.</returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.Where(predicate);
                var includeQuery = this.IncludeChildren(query);

                return includeQuery.SingleOrDefault();
            }
        }

        /// <summary>
        /// Get a single entity by the search predicate.
        /// </summary>
        /// <param name="predicate">Search predicate for querying the entity.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Entity that match the search predicate.</returns>
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.Where(predicate);
                var includeQuery = this.IncludeChildren(query);

                return await includeQuery.SingleOrDefaultAsync(cancellationToken)
                                         .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get a collection of 'length' entities.
        /// </summary>
        /// <param name="length">Number of items to return.</param>
        /// <returns>Collection of entities.</returns>
        public virtual IEnumerable<TEntity> GetCount(int length)
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.AsQueryable()
                                 .OrderBy(e => e.Id)
                                 .Take(length);
                var includeQuery = this.IncludeChildren(query);

                return includeQuery.ToList();
            }
        }

        /// <summary>
        /// Get a collection of 'length' entities starting at the position of 'startIndex'.
        /// </summary>
        /// <param name="startIndex">Position in the full collection to select from.</param>
        /// <param name="length">Number of items to return.</param>
        /// <returns>Collection of entities.</returns>
        public virtual IEnumerable<TEntity> GetCount(int startIndex, int length)
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.AsQueryable()
                                 .OrderBy(e => e.Id)
                                 .Skip(startIndex)
                                 .Take(length);
                var includeQuery = this.IncludeChildren(query);

                return includeQuery.ToList();
            }
        }

        /// <summary>
        /// Get a collection of 'length' entities by the search predicate.
        /// </summary>
        /// <param name="length">Number of items to return.</param>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        public virtual IEnumerable<TEntity> GetCount(int length, Expression<Func<TEntity, bool>> predicate)
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.Where(predicate)
                                 .OrderBy(e => e.Id)
                                 .Take(length);
                var includeQuery = this.IncludeChildren(query);

                return includeQuery.ToList();
            }
        }

        /// <summary>
        /// Get a collection of 'length' entities by the search predicate starting at the position of 'startIndex'.
        /// </summary>
        /// <param name="startIndex">Position in the full collection to select from.</param>
        /// <param name="length">Number of items to return.</param>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        public virtual IEnumerable<TEntity> GetCount(int startIndex, int length, Expression<Func<TEntity, bool>> predicate)
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.Where(predicate)
                                 .OrderBy(e => e.Id)
                                 .Skip(startIndex)
                                 .Take(length);
                var includeQuery = this.IncludeChildren(query);

                return includeQuery.ToList();
            }
        }

        /// <summary>
        /// Get a collection of 'length' entities asynchronously.
        /// </summary>
        /// <param name="length">Number of items to return.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities.</returns>
        public virtual async Task<IEnumerable<TEntity>> GetCountAsync(int length, CancellationToken cancellationToken)
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.AsQueryable()
                                 .OrderBy(e => e.Id)
                                 .Take(length);
                var includeQuery = this.IncludeChildren(query);

                return await includeQuery.ToListAsync(cancellationToken)
                                         .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get a collection of 'length' entities starting at the position of 'startIndex' asynchronously.
        /// </summary>
        /// <param name="startIndex">Position in the full collection to select from.</param>
        /// <param name="length">Number of items to return.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities.</returns>
        public virtual async Task<IEnumerable<TEntity>> GetCountAsync(int startIndex, int length, CancellationToken cancellationToken)
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.AsQueryable()
                                 .OrderBy(e => e.Id)
                                 .Skip(startIndex)
                                 .Take(length);
                var includeQuery = this.IncludeChildren(query);

                return await includeQuery.ToListAsync(cancellationToken)
                                         .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get a collection of 'length' entities by the search predicate asynchronously.
        /// </summary>
        /// <param name="length">Number of items to return.</param>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        public virtual async Task<IEnumerable<TEntity>> GetCountAsync(int length, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.Where(predicate)
                                 .OrderBy(e => e.Id)
                                 .Take(length);
                var includeQuery = this.IncludeChildren(query);

                return await includeQuery.ToListAsync(cancellationToken)
                                         .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get a collection of 'length' entities by the search predicate starting at the position of 'startIndex' asynchronously.
        /// </summary>
        /// <param name="startIndex">Position in the full collection to select from.</param>
        /// <param name="length">Number of items to return.</param>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        public virtual async Task<IEnumerable<TEntity>> GetCountAsync(int startIndex, int length, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.Where(predicate)
                                 .OrderBy(e => e.Id)
                                 .Skip(startIndex)
                                 .Take(length);
                var includeQuery = this.IncludeChildren(query);

                return await includeQuery.ToListAsync(cancellationToken)
                                         .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get a collection of entities
        /// </summary>
        /// <returns>Collection of entities.</returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.AsQueryable();
                var includeQuery = this.IncludeChildren(query);

                return includeQuery.ToList();
            }
        }

        /// <summary>
        /// Get a collection of entities by the search predicate.
        /// </summary>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.Where(predicate);
                var includeQuery = this.IncludeChildren(query);

                return includeQuery.ToList();
            }
        }

        /// <summary>
        /// Get a collection of entities asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities.</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.AsQueryable();
                var includeQuery = this.IncludeChildren(query);

                return await includeQuery.ToListAsync(cancellationToken)
                                         .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Get a collection of entities by the search predicate asynchronously.
        /// </summary>
        /// <param name="predicate">Search predicate for querying the entities.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Collection of entities that match the search predicate.</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var query = dbSet.Where(predicate);
                var includeQuery = this.IncludeChildren(query);

                return await includeQuery.ToListAsync(cancellationToken)
                                         .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets the contents of a FILESTREAM file from SQL Server using a pending transaction.
        /// </summary>
        /// <typeparam name="TResult">Type of the primary key of the table to open the content stream from.</typeparam>
        /// <param name="property">Key property used to select a single record for file streaming.</param>
        /// <param name="fileStreamColumnName">Name of the FILESTREAM column.</param>
        /// <param name="primaryKey">Primary key of the record to read the file from.</param>
        /// <returns>Binary stream of the file content.</returns>
        protected Stream GetContentStream<TResult>(Expression<Func<TEntity, TResult>> property, string fileStreamColumnName, TResult primaryKey)
        {
            using (var context = this.GetContextInstance())
            {
                var propertyInfo = PropertyInfoExtensions.Property<TEntity, TResult>(property);
                var propertyMappings = this.GetPropertyMapping(context);
                string columnName = propertyMappings[propertyInfo.Name];
                const string sqlQueryFormat = "SELECT {0}.PathName() AS [Path], GET_FILESTREAM_TRANSACTION_CONTEXT() AS [Transaction] FROM {1} WHERE [{2}] = @rowId;";
                string tableName = this.GetSchemaAndTablename(context);
                string connectionString = context.Database.Connection.ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format(CultureInfo.InvariantCulture, sqlQueryFormat, fileStreamColumnName, tableName, columnName);
                    cmd.Parameters.Add(new SqlParameter("rowId", primaryKey));

                    if (ConnectionState.Open != connection.State)
                    {
                        connection.Open();
                    }

                    using (var transaction = connection.BeginTransaction())
                    {
                        cmd.Transaction = transaction;
                        string logicalPath = null;
                        byte[] transactionId = null;

                        using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            bool hasRows = reader.Read();

                            if (false == hasRows)
                            {
                                return Stream.Null;
                            }

                            int pathOrdinal = reader.GetOrdinal("Path");
                            int transactionOrdinal = reader.GetOrdinal("Transaction");
                            logicalPath = reader.GetString(pathOrdinal);
                            var sqlBytes = reader.GetSqlBytes(transactionOrdinal);
                            transactionId = sqlBytes.Value;
                        }

                        var contents = new MemoryStream();

                        using (var stream = new SqlFileStream(logicalPath, transactionId, FileAccess.Read))
                        {
                            stream.CopyTo(contents);
                        }

                        contents.Seek(0, SeekOrigin.Begin);
                        transaction.Commit();

                        return contents;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the contents of a FILESTREAM file from SQL Server using a pending transaction asynchronously.
        /// </summary>
        /// <typeparam name="TResult">Type of the primary key of the table to open the content stream from.</typeparam>
        /// <param name="property">Key property used to select a single record for file streaming.</param>
        /// <param name="fileStreamColumnName">Name of the FILESTREAM column.</param>
        /// <param name="primaryKey">Primary key of the record to read the file from.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Binary stream of the file content.</returns>
        protected async Task<Stream> GetContentStreamAsync<TResult>(
            Expression<Func<TEntity, TResult>> property,
            string fileStreamColumnName,
            TResult primaryKey,
            CancellationToken cancellationToken)
        {
            return await Task.Run(() => this.GetContentStream<TResult>(property, fileStreamColumnName, primaryKey), cancellationToken)
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
                this._contextFactory.Dispose();
            }
        }

        /// <summary>
        /// Build an expression that compares the Id of the entity to the given key value.
        /// </summary>
        /// <param name="entityId">Key value for the expression to compare the Id property to.</param>
        /// <returns>Equality expression equivalent to e => e.Id == entityId.</returns>
        protected Expression<Func<TEntity, bool>> GetKeyEqualityExpression(TKey entityId)
        {
            var parameterExpr = Expression.Parameter(typeof (TEntity));
            var idPropertyInfo = PropertyInfoExtensions.Property<TEntity, TKey>(e => e.Id);
            var entityIdExpr = Expression.Property(parameterExpr, idPropertyInfo);
            var constIdExpr = Expression.Constant(entityId, typeof (TKey));
            var whereClause = Expression.Equal(entityIdExpr, constIdExpr);
            var predicate = Expression.Lambda<Func<TEntity, bool>>(whereClause, parameterExpr);

            return predicate;
        }

        /// <summary>
        /// Gets a new instance of the DbContext.
        /// </summary>
        /// <returns>new instance of the context from the context factory.</returns>
        private DbContext GetContextInstance()
        {
            var context = this._contextFactory.Create();
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;

            return context;
        }

        /// <summary>
        /// Gets the schema and name the table that that TEntity is mapped to.
        /// </summary>
        /// <param name="context">Data instance to get the schema and table name from.</param>
        /// <returns>Schema and table name that TEntity is mapped to.</returns>
        private string GetSchemaAndTablename(DbContext context)
        {
            var metadataWorkspace = ((IObjectContextAdapter) context).ObjectContext.MetadataWorkspace;
            var objectSpaceMetadata = metadataWorkspace.GetItems<EntityType>(DataSpace.OSpace);
            var entityMetadata = objectSpaceMetadata.SingleOrDefault(e => e.Name == typeof (TEntity).Name);

            if (null == entityMetadata)
            {
                return null;
            }

            var database = metadataWorkspace.GetItems<EntityContainer>(DataSpace.SSpace)
                                            .FirstOrDefault();

            if (null == database)
            {
                return null;
            }

            var dbEntitySets = database.BaseEntitySets.OfType<EntitySet>();
            var tableMetadata = dbEntitySets.SingleOrDefault(e => e.Name == typeof (TEntity).Name);

            if (null == tableMetadata)
            {
                return null;
            }

            string schemaAndTableName = string.Format(CultureInfo.InvariantCulture, "[{0}].[{1}]", tableMetadata.Schema, tableMetadata.Table);

            return schemaAndTableName;
        }

        /// <summary>
        /// Gets a dictionary that maps entity properties to database column names for the current DbContext.
        /// </summary>
        /// <param name="context">Data context to get the property mapping from.</param>
        /// <returns>Dictionary of entity properties to database column name mapping.</returns>
        private IDictionary<string, string> GetPropertyMapping(DbContext context)
        {
            var metadataWorkspace = ((IObjectContextAdapter) context).ObjectContext.MetadataWorkspace;
            var objectSpaceMetadata = metadataWorkspace.GetItems<EntityType>(DataSpace.OSpace);
            var entityMetadata = objectSpaceMetadata.SingleOrDefault(e => e.Name == typeof (TEntity).Name);

            if (null == entityMetadata)
            {
                return null;
            }

            var propertyNames = entityMetadata.DeclaredProperties;
            var database = metadataWorkspace.GetItems<EntityContainer>(DataSpace.SSpace)
                                            .FirstOrDefault();

            if (null == database)
            {
                return null;
            }

            var dbEntitySets = database.BaseEntitySets.OfType<EntitySet>();
            var tableMetadata = dbEntitySets.SingleOrDefault(e => e.Name == typeof (TEntity).Name);

            if (null == tableMetadata)
            {
                return null;
            }

            var columnNames = tableMetadata.ElementType.DeclaredProperties;

            Func<EdmProperty, int, KeyValuePair<string, string>> createKvp = (e, i) =>
            {
                string columnName = columnNames[i].Name;
                string propertyName = e.Name;

                if (string.IsNullOrWhiteSpace(columnName))
                {
                    columnName = propertyName;
                }

                return new KeyValuePair<string, string>(propertyName, columnName);
            };

            var mappingDictionary = propertyNames.Select(createKvp)
                                                 .ToDictionary(e => e.Key, e => e.Value);

            return mappingDictionary;
        }
    }

    /// <summary>
    /// Short hand base repository for TEntity types that have an int Id property.
    /// </summary>
    /// <typeparam name="TEntity">The repository's entity type.</typeparam>
    public abstract class BaseReadOnlyRepository<TEntity> : BaseReadOnlyRepository<TEntity, long>
        where TEntity : EntityDao<long>
    {
        /// <summary>
        /// Create a new instance of the base repository class with a context factory.
        /// </summary>
        /// <param name="contextFactory">Context factory to create the data context.</param>
        protected BaseReadOnlyRepository(IContextFactory contextFactory) : base(contextFactory)
        {
        }
    }
}
