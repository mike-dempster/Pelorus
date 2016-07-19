using System.Configuration;

namespace Pelorus.Web.Configuration
{
    /// <summary>
    /// Wrapper for the configuration of the web portion of the core library.
    /// </summary>
    internal static class CoreWebConfiguration
    {
        private const string ConfigurationSectionPath = "pelorus.core/web";

        private static CoreWebConfigurationSection configurationSection;

        /// <summary>
        /// Get the configuration section. This property is lazy loaded to improve performance if the web module is not
        /// used by the application that it is configured for.
        /// </summary>
        public static CoreWebConfigurationSection Configuration => configurationSection ?? (configurationSection = GetConfiguration());

        /// <summary>
        /// Gets the core web configuration data from the application config.
        /// </summary>
        /// <returns>Web configuration from the application's config data.</returns>
        private static CoreWebConfigurationSection GetConfiguration()
        {
            return ConfigurationManager.GetSection(ConfigurationSectionPath) as CoreWebConfigurationSection;
        }
    }
}
