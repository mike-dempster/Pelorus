using Pelorus.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pelorus.Core.IoC
{
    /// <summary>
    /// Base class for initializing an IoC container from code and configuration.
    /// </summary>
    public abstract class BaseInitializer : IDisposable
    {
        private static IEnumerable<Type> configuredMapperTypes;

        private readonly IContainer _container;
        private readonly ICollection<BaseMapper> _codedMappers;

        /// <summary>
        /// When overridden in a derived class, configures the container.
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="container"></param>
        protected abstract void ConfigureContainer(string containerName, IContainer container);

        /// <summary>
        /// When overridden in a derived class, adds the mappers to the initializer that are used to initialize the container.
        /// </summary>
        protected abstract void ConfigureMappers();

        /// <summary>
        /// Creates a new instance of the initializer.
        /// </summary>
        /// <param name="container">Instance of the IoC container to initialize.</param>
        protected BaseInitializer(IContainer container)
        {
            this._container = container;
            this._codedMappers = new Collection<BaseMapper>();
        }

        /// <summary>
        /// Initializes the default container.
        /// </summary>
        public void InitializeContainer()
        {
            this.InitializeContainer(null);
        }

        /// <summary>
        /// Initializes a named container.
        /// </summary>
        /// <param name="containerName">Name of the container to initialize.</param>
        public void InitializeContainer(string containerName)
        {
            this.ConfigureContainer(containerName, this._container);
            var configuredMappers = this.GetConfiguredMappers();

            if (null != configuredMappers)
            {
                foreach (var mapper in configuredMappers)
                {
                    mapper.InternalMapContracts(this._container);
                }
            }

            this.ConfigureMappers();

            if (false == this._codedMappers.Any())
            {
                return;
            }

            foreach (var mapper in this._codedMappers)
            {
                mapper.InternalMapContracts(this._container);
            }
        }

        /// <summary>
        /// Releases the mappers that were added to the mapper collection using one of the AddMapper methods.
        /// </summary>
        public void Dispose()
        {
            this._codedMappers.Clear();
        }

        /// <summary>
        /// Adds a mapper to the internal mapper collection.
        /// </summary>
        /// <typeparam name="TMapper">Type of the mapper to add to the internal collection.</typeparam>
        protected void AddMapper<TMapper>()
            where TMapper : class
        {
            var instance = (BaseMapper) Activator.CreateInstance(typeof (TMapper));
            this._codedMappers.Add(instance);
        }

        /// <summary>
        /// Adds a mapper instance to the internal mapper collection.
        /// </summary>
        /// <param name="mapperInstance">Instance of BaseMapper to add to the internal mapper collection.</param>
        protected void AddMapper(BaseMapper mapperInstance)
        {
            this._codedMappers.Add(mapperInstance);
        }

        /// <summary>
        /// Gets a collection of mapper instancews from the configuration data.
        /// </summary>
        /// <returns>Collection of mappers based on the configuration data.</returns>
        private IEnumerable<BaseMapper> GetConfiguredMappers()
        {
            if (null != configuredMapperTypes)
            {
                var mappers = configuredMapperTypes.Select(Activator.CreateInstance)
                                                   .OfType<BaseMapper>()
                                                   .ToArray();
                return mappers;
            }

            var mapperTypes = new List<Type>();

            foreach (var mapper in CoreConfiguration.IoC.Mappers)
            {
                var tpyeConfigElement = (TypeConfigurationElement) mapper;
                var type = Type.GetType(tpyeConfigElement.Type);

                if (null == type)
                {
                    throw new TypeLoadException($"Unable to load type '{tpyeConfigElement.Type}'.");
                }

                mapperTypes.Add(type);
            }

            configuredMapperTypes = mapperTypes.ToArray();

            var configMappers = configuredMapperTypes.Select(Activator.CreateInstance)
                                                     .OfType<BaseMapper>()
                                                     .ToArray();
            return configMappers;
        }
    }
}
