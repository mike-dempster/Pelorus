using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Diagnostics configuration data.
    /// </summary>
    internal class DiagnosticsConfigurationSection : ConfigurationSection
    {
        private const string TraceSourcesKey = "traceSources";

        /// <summary>
        /// Diagnostic trace sources.
        /// </summary>
        [ConfigurationProperty(TraceSourcesKey)]
        public TraceSourceConfigurationCollection TraceSources { get { return this[TraceSourcesKey] as TraceSourceConfigurationCollection; } }
    }
}
