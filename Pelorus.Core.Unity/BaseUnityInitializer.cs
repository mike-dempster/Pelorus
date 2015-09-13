using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Pelorus.Core.IoC;
using System;
using System.Configuration;

namespace Pelorus.Core.Unity
{
    /// <summary>
    /// Base initializer for Unity containers.
    /// </summary>
    public abstract class BaseUnityInitializer : BaseInitializer
    {
        /// <summary>
        /// Creates a new instance of the initializer and creates a wrapper around the container.
        /// </summary>
        /// <param name="container">Container to initialize.</param>
        protected BaseUnityInitializer(IUnityContainer container) : base(new UnityContainerWrapper(container))
        {
        }

        /// <summary>
        /// When overridden in a derived class, configures the container.
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="container"></param>
        protected override void ConfigureContainer(string containerName, IContainer container)
        {
            var containerWrapper = container as UnityContainerWrapper;

            if (null == containerWrapper)
            {
                throw new ArgumentException($"Unable to configure container of type '{container.GetType()}'.");
            }

            var unitySection = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;

            if (null == unitySection)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(containerName))
            {
                containerWrapper.Container.LoadConfiguration(unitySection);
                return;
            }

            containerWrapper.Container.LoadConfiguration(unitySection, containerName);
        }
    }
}
