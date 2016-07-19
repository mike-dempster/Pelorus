using Pelorus.Core.Configuration;
using System.Configuration;

namespace Pelorus.Web.Configuration
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
        public SimpleValueConfigurationElement Domain => this[DomainKey] as SimpleValueConfigurationElement;

        /// <summary>
        /// Path to the service endpoint.
        /// </summary>
        [ConfigurationProperty(PathKey, IsRequired = true)]
        public SimpleValueConfigurationElement Path => this[PathKey] as SimpleValueConfigurationElement;

        /// <summary>
        /// Port that the web service is listening on.
        /// </summary>
        [ConfigurationProperty(PortKey, IsRequired = true)]
        public SimpleValueConfigurationElement<int> Port => this[PortKey] as SimpleValueConfigurationElement<int>;

        /// <summary>
        /// The protocol that the web service uses for communication.
        /// </summary>
        [ConfigurationProperty(ProtocolKey, IsRequired = true)]
        public SimpleValueConfigurationElement Protocol => this[ProtocolKey] as SimpleValueConfigurationElement;

        /// <summary>
        /// The procedure name to call to register with the service.
        /// </summary>
        [ConfigurationProperty(RegisterProcedureKey, IsRequired = true)]
        public SimpleValueConfigurationElement RegisterProcedure => this[RegisterProcedureKey] as SimpleValueConfigurationElement;
    }
}
