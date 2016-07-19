using Pelorus.Core.Data;
using Pelorus.Core.Entities;
using Pelorus.Core.Reflection;
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

namespace Pelorus.Data.EntityFramework
{
    /// <summary>
    /// Base functionality for repository classes.
    /// </summary>
    public abstract class BaseRepository : BaseReadOnlyRepository, IBaseRepository
    {
        private readonly IContextFactory _contextFactory;

        /// <summary>
        /// Initialize the base properties of the repository class.
        /// </summary>
        /// <param name="contextFactory">Context factory to create the data context.</param>
        protected BaseRepository(IContextFactory contextFactory) : base(contextFactory)
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
        ~BaseRepository()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Create a new entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being created.</typeparam>
        /// <param name="entity">New entity to create.</param>
        /// <returns>The new entity with the generated Id.</returns>
        public virtual TEntity Create<TEntity>(TEntity entity)
            where TEntity : class
        {
            using (var context = this.GetContextInstance())
            {
                var expression = EntityNavigationExpressionBuilder.BuildExpression<TEntity>();
                var func = expression.Compile();
                func(entity);
                var dbSet = context.Set<TEntity>();
                var newEntity = dbSet.Add(entity);
                context.SaveChanges();

                return newEntity;
            }
        }

        /// <summary>
        /// Create a new entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being created.</typeparam>
        /// <param name="entity">New entity to create.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>The new entity with the generated Id.</returns>
        public virtual async Task<TEntity> CreateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TEntity : class
        {
            using (var context = this.GetContextInstance())
            {
                var expression = EntityNavigationExpressionBuilder.BuildExpression<TEntity>();
                var func = expression.Compile();
                func(entity);
                var dbSet = context.Set<TEntity>();
                var newEntity = dbSet.Add(entity);
                await context.SaveChangesAsync(cancellationToken)
                             .ConfigureAwait(false);

                return newEntity;
            }
        }

        /// <summary>
        /// Delete an existing entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being deleted.</typeparam>
        /// <param name="entity">Entity to delete.</param>
        /// <returns>Deleted entity.</returns>
        public virtual TEntity Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            using (var context = this.GetContextInstance())
            {
                var expression = EntityNavigationExpressionBuilder.BuildExpression<TEntity>();
                var func = expression.Compile();
                func(entity);
                var deletedEntity = context.Set<TEntity>()
                                           .Remove(entity);
                context.SaveChanges();

                return deletedEntity;
            }
        }

        /// <summary>
        /// Delete an existing entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being deleted.</typeparam>
        /// <param name="entity">Entity to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Deleted entity.</returns>
        public virtual async Task<TEntity> DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TEntity : class
        {
            using (var context = this.GetContextInstance())
            {
                var expression = EntityNavigationExpressionBuilder.BuildExpression<TEntity>();
                var func = expression.Compile();
                func(entity);
                var deletedEntity = context.Set<TEntity>()
                                           .Remove(entity);
                await context.SaveChangesAsync(cancellationToken)
                             .ConfigureAwait(false);

                return deletedEntity;
            }
        }

        /// <summary>
        /// Delete entity by Id.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being deleted.</typeparam>
        /// <typeparam name="TKey">Type of the key of the entity being deleted.</typeparam>
        /// <param name="entityId">Id of the entity to delete.</param>
        /// <returns>True if the entity was found and deleted or false if the entity was not found.</returns>
        public virtual bool DeleteById<TEntity, TKey>(TKey entityId)
            where TEntity : Entity<TKey>
            where TKey : struct
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var entity = dbSet.Find(entityId);

                if (null == entity)
                {
                    return false;
                }

                var entry = context.Entry(entity);
                entry.State = EntityState.Deleted;
                context.SaveChanges();

                return true;
            }
        }

        /// <summary>
        /// Delete entity by Id.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being deleted.</typeparam>
        /// <typeparam name="TKey">Type of the key of the entity being deleted.</typeparam>
        /// <param name="entityId">Id of the entity to delete.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>True if the entity was found and deleted or false if the entity was not found.</returns>
        public virtual async Task<bool> DeleteByIdAsync<TEntity, TKey>(TKey entityId, CancellationToken cancellationToken)
            where TEntity : Entity<TKey>
            where TKey : struct
        {
            using (var context = this.GetContextInstance())
            {
                var dbSet = context.Set<TEntity>();
                var entity = await dbSet.FindAsync(cancellationToken, entityId)
                                        .ConfigureAwait(false);

                if (null == entity)
                {
                    return false;
                }

                var entry = context.Entry(entity);
                entry.State = EntityState.Deleted;
                await context.SaveChangesAsync(cancellationToken)
                             .ConfigureAwait(false);

                return true;
            }
        }

        /// <summary>
        /// Update an existing entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being updated.</typeparam>
        /// <param name="entity">Entity with update to save.</param>
        /// <returns>Updated entity.</returns>
        public virtual TEntity Update<TEntity>(TEntity entity)
            where TEntity : class
        {
            using (var context = this.GetContextInstance())
            {
                var keyValues = this.GetKeyValues(context, entity)
                                    .ToArray();
                var dbSet = context.Set<TEntity>();
                var contextEntity = dbSet.Find(keyValues.ToArray());

                if (null == contextEntity)
                {
                    string delimitedKeyValues = string.Join(", ", keyValues);
                    string exMsg = $"Entity with Id ({delimitedKeyValues}) does not exist.";
                    throw new DataException(exMsg);
                }

                var entry = context.Entry(contextEntity);
                entry.CurrentValues.SetValues(entity);
                entry.State = EntityState.Modified;
                context.SaveChanges();

                return entity;
            }
        }

        /// <summary>
        /// Update the identified properties on an existing entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being updated.</typeparam>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <returns>Updated entity.</returns>
        public virtual TEntity Update<TEntity>(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> modifiedProperties)
            where TEntity : class
        {
            using (var context = this.GetContextInstance())
            {
                var keyValues = this.GetKeyValues(context, entity)
                                    .ToArray();
                var dbSet = context.Set<TEntity>();
                var contextEntity = dbSet.Find(keyValues.ToArray());

                if (null == contextEntity)
                {
                    string delimitedKeyValues = string.Join(", ", keyValues);
                    string exMsg = $"Entity with Id ({delimitedKeyValues}) does not exist.";
                    throw new DataException(exMsg);
                }

                var entry = context.Entry(contextEntity);
                entry.CurrentValues.SetValues(entity);
                entry.State = EntityState.Modified;
                var idPropertyNames = context.GetEntityIds<TEntity>()
                                             .ToArray();
                var modifiedPropertyNames = modifiedProperties.Select(this.GetPropertyName)
                                                              .Where(e => null != e)
                                                              .ToList();

                foreach (var p in entry.CurrentValues.PropertyNames)
                {
                    if (idPropertyNames.Any(e => e.Name == p))
                    {
                        continue;
                    }

                    entry.Property(p).IsModified = modifiedPropertyNames.Contains(p);
                }

                context.SaveChanges();

                return entity;
            }
        }

        /// <summary>
        /// Update the identified properties on an existing entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being updated.</typeparam>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <returns>Updated entity.</returns>
        public virtual TEntity Update<TEntity>(TEntity entity, params Expression<Func<TEntity, object>>[] modifiedProperties)
            where TEntity : class
        {
            var properties = modifiedProperties as IEnumerable<Expression<Func<TEntity, object>>>;
            return this.Update(entity, properties);
        }

        /// <summary>
        /// Update an existing entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being updated.</typeparam>
        /// <param name="entity">Entity with update to save.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Updated entity.</returns>
        public virtual async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TEntity : class
        {
            using (var context = this.GetContextInstance())
            {
                var keyValues = this.GetKeyValues(context, entity)
                                    .ToArray();
                var dbSet = context.Set<TEntity>();
                var contextEntity = await dbSet.FindAsync(cancellationToken, keyValues.ToArray())
                                               .ConfigureAwait(false);
                var entry = context.Entry(contextEntity);
                entry.CurrentValues.SetValues(entity);
                entry.State = EntityState.Modified;
                await context.SaveChangesAsync(cancellationToken)
                             .ConfigureAwait(false);

                return entity;
            }
        }

        /// <summary>
        /// Update the identified properties on an existing entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being updated.</typeparam>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Updated entity.</returns>
        public virtual async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, IEnumerable<Expression<Func<TEntity, object>>> modifiedProperties, CancellationToken cancellationToken)
            where TEntity : class
        {
            using (var context = this.GetContextInstance())
            {
                var keyValues = this.GetKeyValues(context, entity)
                                    .ToArray();
                var dbSet = context.Set<TEntity>();
                var contextEntity = await dbSet.FindAsync(cancellationToken, keyValues)
                                               .ConfigureAwait(false);
                var entry = context.Entry(contextEntity);
                entry.CurrentValues.SetValues(entity);
                entry.State = EntityState.Modified;
                var idPropertyNames = context.GetEntityIds<TEntity>()
                                             .ToArray();
                var modifiedPropertyNames = modifiedProperties.Select(this.GetPropertyName)
                                                              .Where(e => null != e)
                                                              .ToList();

                foreach (var p in entry.CurrentValues.PropertyNames)
                {
                    if (idPropertyNames.Any(e => e.Name == p))
                    {
                        continue;
                    }

                    entry.Property(p).IsModified = modifiedPropertyNames.Contains(p);
                }

                await context.SaveChangesAsync(cancellationToken)
                             .ConfigureAwait(false);

                return entity;
            }
        }

        /// <summary>
        /// Update the identified properties on an existing entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity being updated.</typeparam>
        /// <param name="entity">Updated entity to save.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <param name="modifiedProperties">Properties to update on the entity.</param>
        /// <returns>Updated entity.</returns>
        public virtual async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] modifiedProperties)
            where TEntity : class
        {
            var properties = modifiedProperties as IEnumerable<Expression<Func<TEntity, object>>>;
            return await this.UpdateAsync(entity, properties, cancellationToken);
        }

        /// <summary>
        /// Creates the relationship between parent/child entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of the parent entity to add the child to.</typeparam>
        /// <typeparam name="TChild">Type of the child entity.</typeparam>
        /// <param name="entity">Parent object of the relationship.</param>
        /// <param name="child">Child object of the relationship.</param>
        /// <param name="expression">Expression identifying the associated navigation property on the parent object.</param>
        protected virtual void AddChild<TEntity, TChild>(TEntity entity, TChild child, Expression<Func<TEntity, ICollection<TChild>>> expression)
            where TEntity : class
            where TChild : class
        {
            using (var context = this.GetContextInstance())
            {
                // Convert the strongly typed expression to a Func<TEntity, object> expression that the ChangeRelationshipState method will accept.
                var convertExpression = Expression.Convert(expression.Body, typeof(object));
                var parameter = Expression.Parameter(typeof(TEntity));
                var objectExpression = Expression.Lambda<Func<TEntity, object>>(convertExpression, parameter);

                ((IObjectContextAdapter) context).ObjectContext.ObjectStateManager.ChangeRelationshipState(entity, child, objectExpression, EntityState.Added);
            }
        }

        /// <summary>
        /// Removes the relationship between parent/child entities.
        /// </summary>
        /// <typeparam name="TEntity">Type of the parent entity to remove the child from.</typeparam>
        /// <typeparam name="TChild">Type of the child entity.</typeparam>
        /// <param name="entity">Parent object of the relationship.</param>
        /// <param name="child">Child object of the relationship.</param>
        /// <param name="expression">Expression identifying the associated navigation property on the parent object.</param>
        protected virtual void RemoveChild<TEntity, TChild>(TEntity entity, TChild child, Expression<Func<TEntity, ICollection<TChild>>> expression)
            where TEntity : class
            where TChild : class
        {
            using (var context = this.GetContextInstance())
            {
                // Convert the strongly typed expression to a Func<TEntity, object> expression that the ChangeRelationshipState method will accept.
                var convertExpression = Expression.Convert(expression.Body, typeof(object));
                var parameter = Expression.Parameter(typeof(TEntity));
                var objectExpression = Expression.Lambda<Func<TEntity, object>>(convertExpression, parameter);

                ((IObjectContextAdapter) context).ObjectContext.ObjectStateManager.ChangeRelationshipState(entity, child, objectExpression, EntityState.Deleted);
            }
        }

        /// <summary>
        /// Writes the content of a stream to a FILESTREAM file on SQL Server using a pending transaction.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity that the file stream is associated with.</typeparam>
        /// <typeparam name="TResult">Type of the primary key of the table to write the content stream to.</typeparam>
        /// <param name="property">Key property used to select a single record for file streaming.</param>
        /// <param name="fileStreamColumnName">Name of the FILESTREAM column.</param>
        /// <param name="primaryKey">Primary key of the record to write the file to.</param>
        /// <param name="contents">File content to write to the database.</param>
        protected void WriteContentStream<TEntity, TResult>(
            Expression<Func<TEntity, TResult>> property,
            string fileStreamColumnName,
            TResult primaryKey,
            Stream contents)
            where TEntity : class
        {
            using (var context = this.GetContextInstance())
            {
                var propertyInfo = PropertyInfoExtensions.Property(property);
                var propertyMappings = context.GetPropertyMapping<TEntity>();
                string columnName = propertyMappings[propertyInfo];
                const string sqlQueryFormat = "SELECT {0}.PathName() AS [Path], GET_FILESTREAM_TRANSACTION_CONTEXT() AS [Transaction] FROM {1} WHERE [{2}] = @rowId;";
                string connectionString = context.Database.Connection.ConnectionString;
                string schemaAndTableName = context.GetSchemaAndTableName<TEntity>();

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
        }

        /// <summary>
        /// Writes the content of a stream to a FILESTREAM file on SQL Server using a pending transaction asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity that the file stream is associated with.</typeparam>
        /// <typeparam name="TResult">Type of the primary key of the table to write the content stream to.</typeparam>
        /// <param name="property">Key property used to select a single record for file streaming.</param>
        /// <param name="fileStreamColumnName">Name of the FILESTREAM column.</param>
        /// <param name="primaryKey">Primary key of the record to write the file to.</param>
        /// <param name="contents">File content to write to the database.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <returns>Handle to the async task.</returns>
        protected async Task WriteContentStreamAsync<TEntity, TResult>(
            Expression<Func<TEntity, TResult>> property,
            string fileStreamColumnName,
            TResult primaryKey,
            Stream contents,
            CancellationToken cancellationToken)
            where TEntity : class
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
            using (var context = this.GetContextInstance())
            {
                return context.Database.SqlQuery<T>(sqlQuery, args);
            }
        }

        /// <summary>
        /// Execute a SQL command in the database.
        /// </summary>
        /// <param name="sqlQuery">SQL command to execute.</param>
        /// <param name="args">Arguments for the SQL command.</param>
        /// <returns>Number of rows that were affected by the SQL command.</returns>
        protected int ExecuteSqlCommand(string sqlQuery, params object[] args)
        {
            using (var context = this.GetContextInstance())
            {
                return context.Database.ExecuteSqlCommand(sqlQuery, args);
            }
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
            using (var context = this.GetContextInstance())
            {
                return await context.Database.ExecuteSqlCommandAsync(sqlQuery, cancellationToken, args)
                                    .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Executes a stored procedure and maps the output to type TEntity.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="args">Arguments to pass to the stored procedure.</param>
        /// <returns>Results of the stored procedure.</returns>
        public IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string storedProcedureName, IDictionary<string, object> args)
            where TEntity : class
        {
            return this.ExecuteStoredProcedure<TEntity>(storedProcedureName, "dbo", args);
        }

        /// <summary>
        /// Executes a stored procedure and maps the output to type TEntity asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <param name="args">Arguments to pass to the stored procedure.</param>
        /// <returns>Results of the stored procedure.</returns>
        public async Task<IEnumerable<TEntity>> ExecuteStoredProcedureAsync<TEntity>(
            string storedProcedureName,
            CancellationToken cancellationToken,
            IDictionary<string, object> args)
            where TEntity : class
        {
            return await Task.Run(() => this.ExecuteStoredProcedure<TEntity>(storedProcedureName, args), cancellationToken)
                             .ConfigureAwait(false);
        }

        /// <summary>
        /// Executes a stored procedure and maps the output to type TEntity.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="schemaName">Schema name of the stored procedure.</param>
        /// <param name="args">Arguments to pass to the stored procedure.</param>
        /// <returns>Results of the stored procedure.</returns>
        public IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string storedProcedureName, string schemaName, IDictionary<string, object> args)
            where TEntity : class
        {
            using (var context = this.GetContextInstance())
            {
                var results = new Collection<TEntity>();

                using (var cmd = context.Database.Connection.CreateCommand())
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

                    var resultMapping = context.GetColumnMapping<TEntity>();

                    while (reader.Read())
                    {
                        var instance = this.CreateEntityInstance<TEntity>();

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
        }

        /// <summary>
        /// Executes a stored procedure and maps the output to type TEntity.
        /// </summary>
        /// <param name="storedProcedureName">Name of the stored procedure to execute.</param>
        /// <param name="schemaName">Schema name of the stored procedure.</param>
        /// <param name="cancellationToken">Cancellation token for the asynchronous operation.</param>
        /// <param name="args">Arguments to pass to the stored procedure.</param>
        /// <returns>Results of the stored procedure.</returns>
        public async Task<IEnumerable<TEntity>> ExecuteStoredProcedureAsync<TEntity>(
            string storedProcedureName,
            string schemaName,
            CancellationToken cancellationToken,
            IDictionary<string, object> args)
            where TEntity : class
        {
            return await Task.Run(() => this.ExecuteStoredProcedure<TEntity>(storedProcedureName, schemaName, args), cancellationToken)
                             .ConfigureAwait(false);
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
                this._contextFactory.Dispose();
            }

            base.Dispose(disposing);
        }

        // ReSharper restore RedundantOverridenMember

        /// <summary>
        /// Creates a new instance of the entity type.
        /// </summary>
        /// <returns>New instance of type TEntity.</returns>
        private TEntity CreateEntityInstance<TEntity>()
            where TEntity : class
        {
            using (var context = this.GetContextInstance())
            {
                var instance = ((IObjectContextAdapter) context).ObjectContext.CreateObject<TEntity>();

                return instance;
            }
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
            context.Configuration.AutoDetectChangesEnabled = false;

            return context;
        }

        /// <summary>
        /// Get the name of a property defined in type TEntity.
        /// </summary>
        /// <param name="propertyExpression">Expression identifying the property to get the name of.</param>
        /// <returns>Name of the property or null if the expression is null or the property expression type is not supported.</returns>
        private string GetPropertyName<TEntity>(Expression<Func<TEntity, object>> propertyExpression)
            where TEntity : class
        {
            if (null == propertyExpression)
            {
                throw new ArgumentNullException(nameof(propertyExpression));
            }

            var propertyInfo = PropertyInfoExtensions.Property(propertyExpression);

            if (null == propertyInfo)
            {
                return null;
            }

            return propertyInfo.Name;
        }

        /// <summary>
        /// Gets the key values from an instance of an entity.
        /// </summary>
        /// <param name="context">Instance of a data context that has the given entity type configured.</param>
        /// <param name="instance">Instance of the entity to get the key values from.</param>
        /// <returns>Collection of the key values from the given entity instance.</returns>
        private IEnumerable<object> GetKeyValues<TEntity>(DbContext context, TEntity instance)
            where TEntity : class
        {
            if (null == context)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (null == instance)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var properties = context.GetEntityIds<TEntity>();
            var keyValues = new Collection<object>();

            foreach (var p in properties)
            {
                var val = p.GetValue(instance);
                keyValues.Add(val);
            }

            return keyValues;
        }
    }
}
