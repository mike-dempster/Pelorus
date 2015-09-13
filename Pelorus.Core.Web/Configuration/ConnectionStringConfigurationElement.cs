using System.Configuration;

namespace Pelorus.Core.Web.Configuration
{
    /// <summary>
    /// Configuration element for connection string name.
    /// </summary>
    internal class ConnectionStringConfigurationElement : ConfigurationElement
    {
        private const string NameKey = "name";

        /// <summary>
        /// Name of the connection string to use from the connectionString configuration section.
        /// </summary>
        [ConfigurationProperty(NameKey)]
        public string Name => this[NameKey] as string;
    }
}
