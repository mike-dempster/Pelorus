using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Wrapper around the retrieval of the configuration data.
    /// </summary>
    internal static class CoreConfiguration
    {
        /// <summary>
        /// Path to the configuration data for the diagnostics configuration data.
        /// </summary>
        private const string DiagnosticsConfigurationSectionPath = "pelorus.core/diagnostics";

        /// <summary>
        /// Cache of the diagnostics configuration data.
        /// </summary>
        private static DiagnosticsConfigurationSection diagnosticsConfigurationSection;

        /// <summary>
        /// Configuration section for the diagnostics modules.
        /// </summary>
        public static DiagnosticsConfigurationSection Diagnostics
        {
            get
            {
                if (null == diagnosticsConfigurationSection)
                {
                    diagnosticsConfigurationSection = ConfigurationManager.GetSection(DiagnosticsConfigurationSectionPath) as DiagnosticsConfigurationSection;
                }

                return diagnosticsConfigurationSection;
            }
        }
    }
}
