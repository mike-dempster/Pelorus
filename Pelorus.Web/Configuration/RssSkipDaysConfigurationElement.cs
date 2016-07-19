using Pelorus.Core.Configuration;
using System.Configuration;

namespace Pelorus.Web.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    internal class RssSkipDaysConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("sunday", IsRequired = false)]
        public EmptyConfigurationElement Sunday => this["sunday"] as EmptyConfigurationElement;

        [ConfigurationProperty("monday", IsRequired = false)]
        public EmptyConfigurationElement Monday => this["monday"] as EmptyConfigurationElement;

        [ConfigurationProperty("tuesday", IsRequired = false)]
        public EmptyConfigurationElement Tuesday => this["tuesday"] as EmptyConfigurationElement;

        [ConfigurationProperty("wednesday", IsRequired = false)]
        public EmptyConfigurationElement Wednesday => this["wednesday"] as EmptyConfigurationElement;

        [ConfigurationProperty("thursday", IsRequired = false)]
        public EmptyConfigurationElement Thursday => this["thursday"] as EmptyConfigurationElement;

        [ConfigurationProperty("friday", IsRequired = false)]
        public EmptyConfigurationElement Friday => this["friday"] as EmptyConfigurationElement;

        [ConfigurationProperty("saturday", IsRequired = false)]
        public EmptyConfigurationElement Saturday => this["saturday"] as EmptyConfigurationElement;
    }
}
