using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Configuration element for distributed mutual exclusions.
    /// </summary>
    public class DistributedMutualExclusionConfigurationElement : ConfigurationElement
    {
        private const string ConnectionStringNameKey = "connectionStringName";

        /// <summary>
        /// Connection string to the database to use for distributed mutual exclusions.
        /// </summary>
        [ConfigurationProperty(ConnectionStringNameKey)]
        public string ConnectionStringName => this[ConnectionStringNameKey] as string;
    }
}
