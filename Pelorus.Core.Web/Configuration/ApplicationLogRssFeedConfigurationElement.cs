using Pelorus.Core.Configuration;
using System.Configuration;

namespace Pelorus.Core.Web.Configuration
{
    /// <summary>
    /// Configuration element for the application log RSS feed.
    /// </summary>
    internal class ApplicationLogRssFeedConfigurationElement : ConfigurationElement
    {
        private const string ChannelKey = "channel";
        private const string ConnectionStringNameKey = "connectionString";
        private const string XsltUriKey = "xsltUri";

        /// <summary>
        /// Connection string to the application log data.
        /// </summary>
        [ConfigurationProperty(ConnectionStringNameKey, IsRequired = true)]
        public ConnectionStringConfigurationElement ConnectionString
        {
            get { return this[ConnectionStringNameKey] as ConnectionStringConfigurationElement; }
        }

        /// <summary>
        /// Uri to an XSLT transform that is used to transform application log details to a viewable XHTML page.
        /// </summary>
        [ConfigurationProperty(XsltUriKey, IsRequired = false)]
        public SimpleValueConfigurationElement XsltUri { get { return this[XsltUriKey] as SimpleValueConfigurationElement; } }

        /// <summary>
        /// Configuration data for the RSS channel.
        /// </summary>
        [ConfigurationProperty(ChannelKey, IsRequired = true)]
        public RssChannelConfigurationElement Channel { get { return this[ChannelKey] as RssChannelConfigurationElement; } }
    }
}
