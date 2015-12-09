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
        private const string DistributedMutualExclusionKey = "distributedMutualExclusion";

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

        /// <summary>
        /// Distributed mutual exclsusion configuration data.
        /// </summary>
        [ConfigurationProperty(DistributedMutualExclusionKey)]
        public DistributedMutualExclusionConfigurationElement DistributedMutualExclusion => this[DistributedMutualExclusionKey] as DistributedMutualExclusionConfigurationElement;
    }
}
