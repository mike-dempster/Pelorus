using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Pelorus.Core.Entities;
using Pelorus.Core.Reflection;

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
        /// <param name="context">Context to use for the repository.</param>
        protected BaseRepository(DbContext context) : base(context)
        {
            if (null == context)
            {
                throw new ArgumentNullException("context");
            }

            this._context = context;
            this._context.Configuration.LazyLoadingEnabled = false;
            this._context.Configuration.ProxyCreationEnabled = false;
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
        public virtual TEntity Create(TEntity entity)
        {
            var expression = EntityNavigationExpressionBuilder.BuildExpression<TEntity, TKey>();
            var func = expression.Compile();
            func(entity);
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
        public virtual async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var expression = EntityNavigationExpressionBuilder.BuildExpression<TEntity, TKey>();
            var func = expression.Compile();
            func(entity);
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
        public virtual TEntity Delete(TEntity entity)
        {
            var expression = EntityNavigationExpressionBuilder.BuildExpression<TEntity, TKey>();
            var func = expression.Compile();
            func(entity);
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
        public virtual async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var expression = EntityNavigationExpressionBuilder.BuildExpression<TEntity, TKey>();
            var func = expression.Compile();
            func(entity);
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
        public virtual bool DeleteById(TKey entityId)
        {
            var entity = this.DataSet.Find(entityId);

            if (null == entity)
            {
                return false;
            }

            this.DataSet.Remove(entity);
            this.SaveChanges();

            return true;
        }

        /// <summary>
        /// Delete entity by Id.
        /// </summary>
        /// <param name="entityId">Id of the entity to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>True if the entity was found and deleted or false if the entity was not found.</returns>
        public virtual async Task<bool> DeleteByIdAsync(TKey entityId, CancellationToken cancellationToken)
        {
            var entity = await this.DataSet.FindAsync(cancellationToken, entityId)
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
        /// <param name="entity">Entity with update to save.</param>
        /// <returns>Updated entity.</returns>
        public virtual TEntity Update(TEntity entity)
        {
            var contextEntity = this.DataSet.Find(entity.Id);

            if (null == contextEntity)
            {
                return null;
            }

            var entry = this.Entry(contextEntity);
            entry.CurrentValues.SetValues(entity);
            entry.State = EntityState.Modified;
            this.SaveChanges();

            return entity;
        }

        /// <summary>
        /// Update the identified properties on an existing entity.
        /// </summary>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <returns>Updated entity.</returns>
        public virtual TEntity Update(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> modifiedProperties)
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
        /// Update the identified properties on an existing entity.
        /// </summary>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <returns>Updated entity.</returns>
        public virtual TEntity Update(TEntity entity, params Expression<Func<TEntity, object>>[] modifiedProperties)
        {
            var properties = modifiedProperties as IEnumerable<Expression<Func<TEntity, object>>>;
            return this.Update(entity, properties);
        }

        /// <summary>
        /// Update an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">Entity with update to save.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Updated entity.</returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
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
        /// Update the identified properties on an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Updated entity.</returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> modifiedProperties, CancellationToken cancellationToken)
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
        /// Update the identified properties on an existing entity asynchronously.
        /// </summary>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <returns>Updated entity.</returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] modifiedProperties)
        {
            var properties = modifiedProperties as IEnumerable<Expression<Func<TEntity, object>>>;
            return await this.UpdateAsync(entity, properties, cancellationToken);
        }

        /// <summary>
        /// Creates the relationship between parent/child entities.
        /// </summary>
        /// <typeparam name="TChild">Type of the child entity.</typeparam>
        /// <param name="entity">Parent object of the relationship.</param>
        /// <param name="child">Child object of the relationship.</param>
        /// <param name="expression">Expression identifying the associated navigation property on the parent object.</param>
        protected virtual void AddChild<TChild>(TEntity entity, TChild child, Expression<Func<TEntity, ICollection<TChild>>> expression)
            where TChild : class
        {
            // Convert the strongly typed expression to a Func<TEntity, object> expression that the ChangeRelationshipState method will accept.
            var convertExpression = Expression.Convert(expression.Body, typeof (object));
            var parameter = Expression.Parameter(typeof (TEntity));
            var objectExpression = Expression.Lambda<Func<TEntity, object>>(convertExpression, parameter);

            var objectContext = this._context as IObjectContextAdapter;
            objectContext.ObjectContext.ObjectStateManager.ChangeRelationshipState(entity, child, objectExpression, EntityState.Added);
        }

        /// <summary>
        /// Removes the relationship between parent/child entities.
        /// </summary>
        /// <typeparam name="TChild">Type of the child entity.</typeparam>
        /// <param name="entity">Parent object of the relationship.</param>
        /// <param name="child">Child object of the relationship.</param>
        /// <param name="expression">Expression identifying the associated navigation property on the parent object.</param>
        protected virtual void RemoveChild<TChild>(TEntity entity, TChild child, Expression<Func<TEntity, ICollection<TChild>>> expression)
            where TChild : class
        {
            // Convert the strongly typed expression to a Func<TEntity, object> expression that the ChangeRelationshipState method will accept.
            var convertExpression = Expression.Convert(expression.Body, typeof (object));
            var parameter = Expression.Parameter(typeof (TEntity));
            var objectExpression = Expression.Lambda<Func<TEntity, object>>(convertExpression, parameter);

            var objectContext = this._context as IObjectContextAdapter;
            objectContext.ObjectContext.ObjectStateManager.ChangeRelationshipState(entity, child, objectExpression, EntityState.Deleted);
        }

        /// <summary>
        /// Writes the content of a stream to a FILESTREAM file on SQL Server using a pending transaction.
        /// </summary>
        /// <param name="property">Key property used to select a single record for file streaming.</param>
        /// <param name="fileStreamColumnName">Name of the FILESTREAM column.</param>
        /// <param name="primaryKey">Primary key of the record to write the file to.</param>
        /// <param name="contents">File content to write to the database.</param>
        protected void WriteContentStream<TResult>(
            Expression<Func<TEntity, TResult>> property,
            string fileStreamColumnName,
            TResult primaryKey,
            Stream contents)
        {
            var propertyInfo = PropertyInfoExtensions.Property<TEntity, TResult>(property);
            var propertyMappings = this._context.GetPropertyMapping<TEntity>();
            string columnName = propertyMappings[propertyInfo];
            const string sqlQueryFormat = "SELECT {0}.PathName() AS [Path], GET_FILESTREAM_TRANSACTION_CONTEXT() AS [Transaction] FROM {1} WHERE [{2}] = @rowId;";
            string connectionString = this._context.Database.Connection.ConnectionString;
            string schemaAndTableName = this._context.GetSchemaAndTablename<TEntity>();

            using (var connection = new SqlConnection(connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format(CultureInfo.InvariantCulture, sqlQueryFormat, fileStreamColumnName, schemaAndTableName, columnName);
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
                            string exMsg = string.Format(CultureInfo.CurrentCulture, "No records were found for key '{0}'.", primaryKey);
                            throw new DataException(exMsg);
                        }

                        int pathOrdinal = reader.GetOrdinal("Path");
                        int transactionOrdinal = reader.GetOrdinal("Transaction");
                        logicalPath = reader.GetString(pathOrdinal);
                        var sqlBytes = reader.GetSqlBytes(transactionOrdinal);
                        transactionId = sqlBytes.Value;
                    }

                    using (var stream = new SqlFileStream(logicalPath, transactionId, FileAccess.Write))
                    {
                        if (contents.CanSeek)
                        {
                            // If the stream supports seeking then seek to the beginning of the stream.  If the stream does not support seeking then
                            // we must assume that the pointer is already at the beginning of the stream.
                            contents.Seek(0, SeekOrigin.Begin);
                        }

                        contents.CopyTo(stream);
                    }

                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Writes the content of a stream to a FILESTREAM file on SQL Server using a pending transaction asynchronously.
        /// </summary>
        /// <param name="property">Key property used to select a single record for file streaming.</param>
        /// <param name="fileStreamColumnName">Name of the FILESTREAM column.</param>
        /// <param name="primaryKey">Primary key of the record to write the file to.</param>
        /// <param name="contents">File content to write to the database.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        protected async Task WriteContentStreamAsync<TResult>(
            Expression<Func<TEntity, TResult>> property,
            string fileStreamColumnName,
            TResult primaryKey,
            Stream contents,
            CancellationToken cancellationToken)
        {
            await Task.Run(() => this.WriteContentStream(property, fileStreamColumnName, primaryKey, contents), cancellationToken)
                      .ConfigureAwait(false);
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
        /// Executes a stored procedure and maps the output to type TEntity.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="args">Arguments to pass to the stored procedure.</param>
        /// <returns>Results of the stored procedure.</returns>
        public IEnumerable<TEntity> ExecuteStoredProcedure(string storedProcedureName, IDictionary<string, object> args)
        {
            return this.ExecuteStoredProcedure(storedProcedureName, "dbo", args);
        }

        /// <summary>
        /// Executes a stored procedure and maps the output to type TEntity asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <param name="args">Arguments to pass to the stored procedure.</param>
        /// <returns>Results of the stored procedure.</returns>
        public async Task<IEnumerable<TEntity>> ExecuteStoredProcedureAsync(
            string storedProcedureName,
            CancellationToken cancellationToken,
            IDictionary<string, object> args)
        {
            return await Task.Run(() => this.ExecuteStoredProcedure(storedProcedureName, args), cancellationToken)
                             .ConfigureAwait(false);
        }

        /// <summary>
        /// Executes a stored procedure and maps the output to type TEntity.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="schemaName">Schema name of the stored procedure.</param>
        /// <param name="args">Arguments to pass to the stored procedure.</param>
        /// <returns>Results of the stored procedure.</returns>
        public IEnumerable<TEntity> ExecuteStoredProcedure(string storedProcedureName, string schemaName, IDictionary<string, object> args)
        {
            var results = new Collection<TEntity>();

            using (var cmd = this._context.Database.Connection.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", schemaName, storedProcedureName);

                foreach (var arg in args)
                {
                    var parameter = new SqlParameter(arg.Key, arg.Value);
                    cmd.Parameters.Add(parameter);
                }

                if (ConnectionState.Open != cmd.Connection.State)
                {
                    cmd.Connection.Open();
                }

                var reader = cmd.ExecuteReader();

                if (false == reader.HasRows)
                {
                    return results;
                }

                var resultMapping = this._context.GetColumnMapping<TEntity>();

                while (reader.Read())
                {
                    var instance = this.CreateEntityInstance();

                    foreach (var property in resultMapping)
                    {
                        try
                        {
                            var value = reader[property.Key];
                            property.Value.SetValue(instance, value);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            // Continue if a nullable property is missing from the results.
                            if (property.Value.PropertyType.IsClass)
                            {
                                continue;
                            }

                            // If the property is not nullable then throw the exception.
                            var exMsg = string.Format(
                                CultureInfo.InvariantCulture,
                                "Column '{0}' for non nullable field '{1}' is missing from the result set.",
                                property.Key,
                                property.Value.Name);
                            throw new InvalidDataException(exMsg);
                        }
                    }

                    results.Add(instance);
                }
            }

            return results;
        }

        /// <summary>
        /// Executes a stored procedure and maps the output to type TEntity.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="schemaName">Schema name of the stored procedure.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <param name="args">Arguments to pass to the stored procedure.</param>
        /// <returns>Results of the stored procedure.</returns>
        public async Task<IEnumerable<TEntity>> ExecuteStoredProcedureAsync(
            string storedProcedureName,
            string schemaName,
            CancellationToken cancellationToken,
            IDictionary<string, object> args)
        {
            return await Task.Run(() => this.ExecuteStoredProcedure(storedProcedureName, schemaName, args), cancellationToken)
                             .ConfigureAwait(false);
        }

        /// <summary>
        /// Save the changes in the underlying data context.
        /// </summary>
        protected virtual int SaveChanges()
        {
            return this._context.SaveChanges();
        }

        /// <summary>
        /// Save the changes in the underlying data context.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns></returns>
        protected virtual async Task SaveChangesAsync(CancellationToken cancellationToken)
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
        /// Creates a new instance of the entity type.
        /// </summary>
        /// <returns>New instance of type TEntity.</returns>
        private TEntity CreateEntityInstance()
        {
            var instance = ((IObjectContextAdapter) this._context).ObjectContext.CreateObject<TEntity>();

            return instance;
        }

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
    public abstract class BaseRepository<TEntity> : BaseRepository<TEntity, long>
        where TEntity : EntityDao<long>
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
        /// <param name="context">Context to use for the repository.</param>
        protected BaseRepository(DbContext context) : base(context)
        {
        }
    }
}
