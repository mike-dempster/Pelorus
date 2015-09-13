using Pelorus.Core.Configuration;
using System.Configuration;

namespace Pelorus.Core.Web.Configuration
{
    /// <summary>
    /// RSS text input configuration element.
    /// </summary>
    internal class RssTextInputConfigurationElement : ConfigurationElement
    {
        private const string TitleKey = "title";
        private const string DescriptionKey = "description";
        private const string NameKey = "name";
        private const string LinkKey = "link";

        /// <summary>
        /// Label of the submit button to show in the input area.
        /// </summary>
        [ConfigurationProperty(TitleKey, IsRequired = true)]
        public SimpleValueConfigurationElement Title => this[TitleKey] as SimpleValueConfigurationElement;

        /// <summary>
        /// Explains the text area input.
        /// </summary>
        [ConfigurationProperty(DescriptionKey, IsRequired = true)]
        public SimpleValueConfigurationElement Description => this[DescriptionKey] as SimpleValueConfigurationElement;

        /// <summary>
        /// Name of the text object in the text input area.
        /// </summary>
        [ConfigurationProperty(NameKey, IsRequired = true)]
        public SimpleValueConfigurationElement Name => this[NameKey] as SimpleValueConfigurationElement;

        /// <summary>
        /// Url of the script to handle the request.
        /// </summary>
        [ConfigurationProperty(LinkKey, IsRequired = true)]
        public SimpleValueConfigurationElement Link => this[LinkKey] as SimpleValueConfigurationElement;
    }
}
