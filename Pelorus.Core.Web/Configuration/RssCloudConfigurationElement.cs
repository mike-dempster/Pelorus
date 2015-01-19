using Pelorus.Core.Configuration;
using System.Configuration;

namespace Pelorus.Core.Web.Configuration
{
    /// <summary>
    /// Configuration element that identifies a service that supports rssCloud interface.
    /// </summary>
    internal class RssCloudConfigurationElement : ConfigurationElement
    {
        private const string DomainKey = "domain";
        private const string PathKey = "path";
        private const string PortKey = "port";
        private const string ProtocolKey = "protocol";
        private const string RegisterProcedureKey = "registerProcedure";

        /// <summary>
        /// Domain name of the web service.
        /// </summary>
        [ConfigurationProperty(DomainKey, IsRequired = true)]
        public SimpleValueConfigurationElement Domain
        {
            get { return this[DomainKey] as SimpleValueConfigurationElement; }
        }

        /// <summary>
        /// Path to the service endpoint.
        /// </summary>
        [ConfigurationProperty(PathKey, IsRequired = true)]
        public SimpleValueConfigurationElement Path
        {
            get { return this[PathKey] as SimpleValueConfigurationElement; }
        }

        /// <summary>
        /// Port that the web service is listening on.
        /// </summary>
        [ConfigurationProperty(PortKey, IsRequired = true)]
        public SimpleValueConfigurationElement<int> Port
        {
            get { return this[PortKey] as SimpleValueConfigurationElement<int>; }
        }

        /// <summary>
        /// The protocol that the web service uses for communication.
        /// </summary>
        [ConfigurationProperty(ProtocolKey, IsRequired = true)]
        public SimpleValueConfigurationElement Protocol
        {
            get { return this[ProtocolKey] as SimpleValueConfigurationElement; }
        }

        /// <summary>
        /// The procedure name to call to register with the service.
        /// </summary>
        [ConfigurationProperty(RegisterProcedureKey, IsRequired = true)]
        public SimpleValueConfigurationElement RegisterProcedure
        {
            get { return this[RegisterProcedureKey] as SimpleValueConfigurationElement; }
        }
    }
}
