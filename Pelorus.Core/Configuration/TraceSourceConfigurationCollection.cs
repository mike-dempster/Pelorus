using System;
using System.Configuration;
using System.Globalization;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Configuration collection of trace sources.
    /// </summary>
    internal class TraceSourceConfigurationCollection : BaseConfigurationElementCollection<AddTraceSourceConfigurationElement>
    {
        /// <summary>
        /// Create a new instance of the collection.
        /// </summary>
        public TraceSourceConfigurationCollection() : base("add")
        {
        }

        /// <summary>
        /// Get the key of the given element.
        /// </summary>
        /// <param name="element">Element to get the key for.</param>
        /// <returns>Value of the key property of the element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            var addElement = element as AddTraceSourceConfigurationElement;

            if (null != addElement)
            {
                return addElement.Name;
            }

            string exMsg = string.Format(
                    CultureInfo.InvariantCulture,
                    "Cannot cast element to type '{0}'.",
                    typeof(AddTraceSourceConfigurationElement).FullName);
            throw new InvalidCastException(exMsg);
        }
    }
}
