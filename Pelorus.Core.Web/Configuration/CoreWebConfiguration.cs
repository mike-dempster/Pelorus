using System.Configuration;

namespace Pelorus.Core.Web.Configuration
{
    /// <summary>
    /// Wrapper for the configuration of the web portion of the core library.
    /// </summary>
    internal static class CoreWebConfiguration
    {
        private const string ConfigurationSectionPath = "pelorus.core/web";

        private static CoreWebConfigurationSection configurationSection = null;

        /// <summary>
        /// Get the configuration section. This property is lazy loaded to improve performance if the web module is not
        /// used by the application that it is configured for.
        /// </summary>
        public static CoreWebConfigurationSection Configuration
        {
            get
            {
                if (null == configurationSection)
                {
                    configurationSection = ConfigurationManager.GetSection(ConfigurationSectionPath) as CoreWebConfigurationSection;
                }

                return configurationSection;
            }
        }
    }
}
