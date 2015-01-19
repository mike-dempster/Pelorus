using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Empty configuration element.
    /// </summary>
    public class EmptyConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Indicates if the element is present in the configuration.
        /// </summary>
        public bool IsPresent { get { return this.ElementInformation.IsPresent; } }
    }
}
