using System;
using System.Reflection;

namespace Pelorus.Core.Reflection
{
    /// <summary>
    /// Extension methods for working with indexers.
    /// </summary>
    public static class Indexers
    {
        /// <summary>
        /// Search for an indexer with the given index type.
        /// </summary>
        /// <typeparam name="TEntity">Type to search for the indexer on.</typeparam>
        /// <typeparam name="TIndexer">Index type to search for.</typeparam>
        /// <returns>Indexer with the given index type or null if an indexer with the given signature does not exist.</returns>
        public static PropertyInfo GetIndexer<TEntity, TIndexer>()
        {
            return GetIndexer(typeof(TEntity), typeof(TIndexer));
        }

        /// <summary>
        /// Search for an indexer with the given index type.
        /// </summary>
        /// <typeparam name="TIndexer">Index type to search for.</typeparam>
        /// <param name="subject">Type to search for the indexer on.</param>
        /// <returns>Indexer with the given index type or null if an indexer with the given signature does not exist.</returns>
        public static PropertyInfo GetIndexer<TIndexer>(this Type subject)
        {
            return GetIndexer(subject, typeof(TIndexer));
        }

        /// <summary>
        /// Search for an indexer with the given index type.
        /// </summary>
        /// <param name="entityType">Type to search for the indexer on.</param>
        /// <param name="indexerType">Index type to search for.</param>
        /// <returns>Indexer with the given index type or null if an indexer with the given signature does not exist.</returns>
        public static PropertyInfo GetIndexer(Type entityType, Type indexerType)
        {
            var properties = entityType.GetProperties();

            foreach (var p in properties)
            {
                var indexers = p.GetIndexParameters();

                if (1 != indexers.Length)
                {
                    continue;
                }

                if (indexers[0].ParameterType == indexerType)
                {
                    return p;
                }
            }

            return null;
        }

        /// <summary>
        /// Determines if an indexer with the given index type exists on the type.
        /// </summary>
        /// <param name="subject">Type to search for the indexer on.</param>
        /// <param name="indexerType">Index type to search for.</param>
        /// <returns>True if an indexer with the given signature exists for the entity type.</returns>
        public static bool HasIndexer(this Type subject, Type indexerType)
        {
            return GetIndexer(subject, indexerType) != null;
        }

        /// <summary>
        /// Determines if an indexer with the given index type exists on the type.
        /// </summary>
        /// <typeparam name="TIndexer">Index type to search for.</typeparam>
        /// <param name="subject">Type to search for the indexer on.</param>
        /// <returns>True if an indexer with the given signature exists for the entity type.</returns>
        public static bool HasIndexer<TIndexer>(this Type subject)
        {
            return GetIndexer(subject, typeof(TIndexer)) != null;
        }
    }
}
