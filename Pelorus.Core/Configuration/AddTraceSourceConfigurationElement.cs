using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Configuration element that adds a trace source to a collection.
    /// </summary>
    internal class AddTraceSourceConfigurationElement : ConfigurationElement
    {
        private const string NameKey = "name";

        /// <summary>
        /// Name of the trace source to add to the collection.
        /// </summary>
        [ConfigurationProperty(NameKey)]
        public string Name { get { return this[NameKey] as string; } }
    }
}
