using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// IoC configuration data.
    /// </summary>
    internal class IoCConfigurationElement : ConfigurationElement
    {
        private const string MappersKey = "mappers";

        /// <summary>
        /// Gets or sets IoC mapper configuration data.
        /// </summary>
        [ConfigurationProperty(MappersKey)]
        public IoCMapperCollection Mappers => this[MappersKey] as IoCMapperCollection;
    }
}
