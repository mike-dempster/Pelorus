using Pelorus.Core.Configuration;
using System.Configuration;

namespace Pelorus.Core.Web.Configuration
{
    /// <summary>
    /// RSS image configuration element.
    /// </summary>
    internal class RssImageConfigurationElement : ConfigurationElement
    {
        private const string UrlKey = "url";
        private const string TitleKey = "title";
        private const string LinkKey = "link";
        private const string WidthKey = "width";
        private const string HeightKey = "height";
        private const string DescriptionKey = "description";

        /// <summary>
        /// Url to the image for the RSS channel.
        /// </summary>
        [ConfigurationProperty(UrlKey, IsRequired = true)]
        public SimpleValueConfigurationElement Url => this[UrlKey] as SimpleValueConfigurationElement;

        /// <summary>
        /// Alternate title of the image if the image cannot be displayed.
        /// </summary>
        [ConfigurationProperty(TitleKey, IsRequired = true)]
        public SimpleValueConfigurationElement Title => this[TitleKey] as SimpleValueConfigurationElement;

        /// <summary>
        /// Url to the site that the user should be taken to when the image is clicked.
        /// </summary>
        [ConfigurationProperty(LinkKey, IsRequired = true)]
        public SimpleValueConfigurationElement Link => this[LinkKey] as SimpleValueConfigurationElement;

        /// <summary>
        /// Optional. Width of the image.
        /// </summary>
        [ConfigurationProperty(WidthKey, IsRequired = false)]
        public SimpleValueConfigurationElement<int> Width => this[WidthKey] as SimpleValueConfigurationElement<int>;

        /// <summary>
        /// Optional. Height of the image.
        /// </summary>
        [ConfigurationProperty(HeightKey, IsRequired = false)]
        public SimpleValueConfigurationElement<int> Height => this[HeightKey] as SimpleValueConfigurationElement<int>;

        /// <summary>
        /// Contains text that is included in the TITLE attribute of the link formed around the image in the HTML rendering.
        /// </summary>
        [ConfigurationProperty(DescriptionKey, IsRequired = false)]
        public SimpleValueConfigurationElement Description => this[DescriptionKey] as SimpleValueConfigurationElement;
    }
}
