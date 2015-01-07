using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Wrapper around the retrieval of the configuration data.
    /// </summary>
    internal static class CoreConfiguration
    {
        private const string ConfigurationSectionPath = "pelorus.core";

        private static CoreConfigurationSection configurationSection = null;

        /// <summary>
        /// Configuration data.
        /// </summary>
        public static CoreConfigurationSection Data
        {
            get
            {
                if (null == configurationSection)
                {
                    configurationSection = ConfigurationManager.GetSection(ConfigurationSectionPath) as CoreConfigurationSection;
                }

                return configurationSection;
            }
        }
    }
}
