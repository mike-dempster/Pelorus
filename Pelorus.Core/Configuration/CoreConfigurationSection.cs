using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Main configuration section for the project.
    /// </summary>
    internal class CoreConfigurationSection : ConfigurationSection
    {
        private const string DiagnosticsElementKey = "diagnostics";

        /// <summary>
        /// Diagnostics configuration data.
        /// </summary>
        [ConfigurationProperty(DiagnosticsElementKey)]
        public DiagnosticsConfigurationElement Diagnostics { get { return this[DiagnosticsElementKey] as DiagnosticsConfigurationElement; } }
    }
}
