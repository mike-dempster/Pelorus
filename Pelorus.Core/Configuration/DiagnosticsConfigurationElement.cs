using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Diagnostics configuration data.
    /// </summary>
    internal class DiagnosticsConfigurationElement : ConfigurationElement
    {
        private const string TraceSourcesKey = "traceSources";

        /// <summary>
        /// Diagnostic trace sources.
        /// </summary>
        [ConfigurationProperty(TraceSourcesKey)]
        public TraceSourceConfigurationCollection TraceSources { get { return this[TraceSourcesKey] as TraceSourceConfigurationCollection; } }
    }
}
