using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Generic configuration element with a single attribute called "value".
    /// </summary>
    /// <typeparam name="TValue">Type of the value attribute.</typeparam>
    public class SimpleValueConfigurationElement<TValue> : ConfigurationElement
    {
        private const string ValueKey = "value";

        /// <summary>
        /// Value of the element.
        /// </summary>
        [ConfigurationProperty(ValueKey)]
        public TValue Value
        {
            get { return (TValue)this[ValueKey]; }
            set { this[ValueKey] = value; }
        }
    }

    /// <summary>
    /// Generic configuration element with a single attribute called "value" of type string.
    /// </summary>
    public class SimpleValueConfigurationElement : SimpleValueConfigurationElement<string>
    {
    }
}
