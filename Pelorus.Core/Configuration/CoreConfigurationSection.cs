using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Core configuration section.
    /// </summary>
    internal class CoreConfigurationSection : ConfigurationSection
    {
        private const string DiagnosticsElementKey = "diagnostics";
        private const string IoCElementKey = "ioc";

        /// <summary>
        /// Diagnostics configuration data.
        /// </summary>
        [ConfigurationProperty(DiagnosticsElementKey)]
        public DiagnosticsConfigurationElement Diagnostics => this[DiagnosticsElementKey] as DiagnosticsConfigurationElement;

        /// <summary>
        /// IoC configuration data.
        /// </summary>
        [ConfigurationProperty(IoCElementKey)]
        public IoCConfigurationElement IoC => this[IoCElementKey] as IoCConfigurationElement;
    }
}
