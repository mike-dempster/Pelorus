﻿using Pelorus.Core.Reflection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Pelorus.Data.EntityFramework
{
    /// <summary>
    /// Extension methods for simplifying common tasks done on DbContext instances.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Gets the schema and name the table that that TEntity is mapped to in the data context.
        /// </summary>
        /// <typeparam name="TEntity">Entity type to get the schema and table name for.</typeparam>
        /// <param name="context">Data context with the entity mapping.</param>
        /// <returns>Schema and table name that TEntity is mapped to.</returns>
        public static string GetSchemaAndTableName<TEntity>(this DbContext context)
            where TEntity : class
        {
            var metadataWorkspace = ((IObjectContextAdapter) context).ObjectContext.MetadataWorkspace;
            var objectSpaceMetadata = metadataWorkspace.GetItems<EntityType>(DataSpace.OSpace);
            var entityMetadata = objectSpaceMetadata.SingleOrDefault(e => e.Name == typeof(TEntity).Name);

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
            var tableMetadata = dbEntitySets.SingleOrDefault(e => e.Name == typeof(TEntity).Name);

            if (null == tableMetadata)
            {
                return null;
            }

            string schemaAndTableName = string.Format(CultureInfo.InvariantCulture, "[{0}].[{1}]", tableMetadata.Schema, tableMetadata.Table);

            return schemaAndTableName;
        }

        /// <summary>
        /// Gets a dictionary of properties and the column names that they are mapped to for the DbContext.
        /// </summary>
        /// <typeparam name="TEntity">Entity type to get the property mapping for.</typeparam>
        /// <param name="context">Data context with the property mapping.</param>
        /// <returns>Dictionary of entity properties to database column name mapping.</returns>
        public static IDictionary<PropertyInfo, string> GetPropertyMapping<TEntity>(this DbContext context)
            where TEntity : class
        {
            if (null == context)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var metadataWorkspace = ((IObjectContextAdapter) context).ObjectContext.MetadataWorkspace;
            var objectSpaceMetadata = metadataWorkspace.GetItems<EntityType>(DataSpace.OSpace);
            var entityMetadata = objectSpaceMetadata.SingleOrDefault(e => e.Name == typeof(TEntity).Name);

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
            var tableMetadata = dbEntitySets.SingleOrDefault(e => e.Name == typeof(TEntity).Name);

            if (null == tableMetadata)
            {
                return null;
            }

            var columnNames = tableMetadata.ElementType.DeclaredProperties;

            Func<EdmProperty, int, KeyValuePair<PropertyInfo, string>> createKvp = (e, i) =>
            {
                string columnName = columnNames[i].Name;
                string propertyName = e.Name;

                if (string.IsNullOrWhiteSpace(columnName))
                {
                    columnName = propertyName;
                }

                var propertyInfo = typeof(TEntity).GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);

                return new KeyValuePair<PropertyInfo, string>(propertyInfo, columnName);
            };

            var mappingDictionary = propertyNames.Select(createKvp)
                                                 .ToDictionary(e => e.Key, e => e.Value);

            return mappingDictionary;
        }

        /// <summary>
        /// Gets a dictionary that maps database column names to entity properties for the data context.
        /// </summary>
        /// <typeparam name="TEntity">Entity type to get the column mapping for.</typeparam>
        /// <param name="context">Data context with the property mapping.</param>
        /// <returns>Dictionary of database column names to entity property mapping.</returns>
        public static IDictionary<string, PropertyInfo> GetColumnMapping<TEntity>(this DbContext context)
            where TEntity : class
        {
            var metadataWorkspace = ((IObjectContextAdapter) context).ObjectContext.MetadataWorkspace;
            var objectSpaceMetadata = metadataWorkspace.GetItems<EntityType>(DataSpace.OSpace);
            var entityMetadata = objectSpaceMetadata.SingleOrDefault(e => e.Name == typeof(TEntity).Name);

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
            var tableMetadata = dbEntitySets.SingleOrDefault(e => e.Name == typeof(TEntity).Name);

            if (null == tableMetadata)
            {
                return null;
            }

            var columnNames = tableMetadata.ElementType.DeclaredProperties;

            Func<EdmProperty, int, KeyValuePair<string, PropertyInfo>> createKvp = (e, i) =>
            {
                string columnName = e.Name;
                string propertyName = propertyNames[i].Name;

                if (string.IsNullOrWhiteSpace(columnName))
                {
                    columnName = propertyName;
                }

                var propertyInfo = typeof(TEntity).GetProperty(propertyName);

                return new KeyValuePair<string, PropertyInfo>(columnName, propertyInfo);
            };

            var mappingDictionary = columnNames.Select(createKvp)
                                               .ToDictionary(e => e.Key, e => e.Value);

            return mappingDictionary;
        }

        /// <summary>
        /// Gets the properties that are defined as the keys for the given entity.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to get the key properties for.</typeparam>
        /// <param name="context">Instance of a context that has <typeparamref name="TEntity"/> configured</param>
        /// <returns>Collection of key properties on the given entity type.</returns>
        public static IEnumerable<PropertyInfo> GetEntityIds<TEntity>(this DbContext context)
            where TEntity : class
        {
            var metadataWorkspace = ((IObjectContextAdapter) context).ObjectContext.MetadataWorkspace;
            var objectSpaceMetadata = metadataWorkspace.GetItems<EntityType>(DataSpace.OSpace);
            var entityMetadata = objectSpaceMetadata.SingleOrDefault(e => e.Name == typeof(TEntity).Name);

            if (null == entityMetadata)
            {
                return null;
            }

            var keyProperties = new Collection<PropertyInfo>();

            foreach (var p in entityMetadata.KeyProperties)
            {
                var pInfo = PropertyInfoExtensions.GetProperty<TEntity>(p.Name);
                keyProperties.Add(pInfo);
            }

            return keyProperties;
        }
    }
}
