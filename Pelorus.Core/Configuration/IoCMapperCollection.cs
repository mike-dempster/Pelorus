using System;
using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Collection of IoC mappers.
    /// </summary>
    internal class IoCMapperCollection : BaseConfigurationElementCollection<TypeConfigurationElement>
    {
        /// <summary>
        /// Create a new instance of the collection.
        /// </summary>
        public IoCMapperCollection() : base("add")
        {
        }

        /// <summary>
        /// Returns the key of an element of the array.
        /// </summary>
        /// <param name="element">Element to get the key.</param>
        /// <returns>Key of the given element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            var addElement = element as TypeConfigurationElement;

            if (null == addElement)
            {
                throw new InvalidCastException($"Cannot cast element to type '{typeof(AddTraceSourceConfigurationElement).FullName}'.");
            }

            return addElement.Type;
        }
    }
}
