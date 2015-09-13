using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Respresents a configuration element that specifies a type.
    /// </summary>
    internal class TypeConfigurationElement : ConfigurationElement
    {
        private const string TypeKey = "type";

        /// <summary>
        /// Gets the type specified by the configuration element.
        /// </summary>
        [ConfigurationProperty(TypeKey)]
        public string Type => this[TypeKey] as string;
    }
}
