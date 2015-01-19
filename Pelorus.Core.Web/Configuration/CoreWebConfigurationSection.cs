using System.Configuration;

namespace Pelorus.Core.Web.Configuration
{
    /// <summary>
    /// Configuration section for the web portion of the core library.
    /// </summary>
    internal class CoreWebConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Element name of the application log RSS feed configuration.
        /// </summary>
        private const string ApplicationLogRssFeedKey = "applicationLogFeed";

        /// <summary>
        /// Configuration element for the application log RSS feed.
        /// </summary>
        [ConfigurationProperty(ApplicationLogRssFeedKey, IsRequired = false)]
        public ApplicationLogRssFeedConfigurationElement ApplicationLogRssFeed
        {
            get { return this[ApplicationLogRssFeedKey] as ApplicationLogRssFeedConfigurationElement; }
        }
    }
}
