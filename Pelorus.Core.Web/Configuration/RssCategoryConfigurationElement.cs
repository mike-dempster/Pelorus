using System.Configuration;

namespace Pelorus.Core.Web.Configuration
{
    /// <summary>
    /// Configuration element that specifies a category for an RSS feed or item.
    /// </summary>
    internal class RssCategoryConfigurationElement : ConfigurationElement
    {
        private const string ValueKey = "value";
        private const string DomainKey = "domain";

        /// <summary>
        /// Value or name of the category.
        /// </summary>
        [ConfigurationProperty(ValueKey, IsRequired = true)]
        public string Value => this[ValueKey] as string;

        /// <summary>
        /// Domain of the category.
        /// </summary>
        [ConfigurationProperty(DomainKey, IsRequired = true)]
        public string Domain => this[DomainKey] as string;
    }
}
