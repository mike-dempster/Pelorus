using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Wrapper around the retrieval of the configuration data.
    /// </summary>
    internal static class CoreConfiguration
    {
        private const string ConfigurationSectionPath = "pelorus.core";
        /// <summary>
        /// Cache of the configuration data.
        /// </summary>
        private static CoreConfigurationSection coreSection;

        public static CoreConfigurationSection Core => coreSection ?? (coreSection = GetConfiguration());

        /// <summary>
        /// Configuration section for the diagnostics module.
        /// </summary>
        public static DiagnosticsConfigurationElement Diagnostics => Core.Diagnostics;

        /// <summary>
        /// Configuration data for the IoC module.
        /// </summary>
        public static IoCConfigurationElement IoC => Core.IoC;

        /// <summary>
        /// Gets the configuration section from the application config.
        /// </summary>
        /// <returns>Configuration data from the application's config data.</returns>
        private static CoreConfigurationSection GetConfiguration()
        {
            return ConfigurationManager.GetSection(ConfigurationSectionPath) as CoreConfigurationSection;
        }
    }
}
