using System.Configuration;
using Pelorus.Core.Configuration;

namespace Pelorus.Core.Web.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    internal class RssSkipDaysConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("sunday", IsRequired = false)]
        public EmptyConfigurationElement Sunday { get { return this["sunday"] as EmptyConfigurationElement; } }

        [ConfigurationProperty("monday", IsRequired = false)]
        public EmptyConfigurationElement Monday { get { return this["monday"] as EmptyConfigurationElement; } }

        [ConfigurationProperty("tuesday", IsRequired = false)]
        public EmptyConfigurationElement Tuesday { get { return this["tuesday"] as EmptyConfigurationElement; } }

        [ConfigurationProperty("wednesday", IsRequired = false)]
        public EmptyConfigurationElement Wednesday { get { return this["wednesday"] as EmptyConfigurationElement; } }

        [ConfigurationProperty("thursday", IsRequired = false)]
        public EmptyConfigurationElement Thursday { get { return this["thursday"] as EmptyConfigurationElement; } }

        [ConfigurationProperty("friday", IsRequired = false)]
        public EmptyConfigurationElement Friday { get { return this["friday"] as EmptyConfigurationElement; } }

        [ConfigurationProperty("saturday", IsRequired = false)]
        public EmptyConfigurationElement Saturday { get { return this["saturday"] as EmptyConfigurationElement; } }
    }
}
