using Pelorus.Core.Configuration;
using System.Configuration;

namespace Pelorus.Web.Configuration
{
    /// <summary>
    /// Collection of categories for the RSS channel.
    /// </summary>
    internal class RssCategoriesConfigurationCollection : BaseConfigurationElementCollection<RssCategoryConfigurationElement>
    {
        /// <summary>
        /// Creates a new instance of the collection.
        /// </summary>
        public RssCategoriesConfigurationCollection() : base("add")
        {
        }

        /// <summary>
        /// Get the key value for the given configuration element.
        /// </summary>
        /// <param name="element">Configuration element to get the key for.</param>
        /// <returns>Category or name of the configuration element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RssCategoryConfigurationElement) element).Value;
        }
    }
}
