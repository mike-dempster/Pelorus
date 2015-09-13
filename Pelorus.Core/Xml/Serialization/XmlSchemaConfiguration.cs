using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Serialization;

namespace Pelorus.Core.Xml.Serialization
{
    /// <summary>
    /// Configuration for an XML entity.
    /// </summary>
    /// <typeparam name="T">Type of the entity to configure for the serializer.</typeparam>
    public class XmlSchemaConfiguration<T> : XmlSchemaConfiguration
        where T : class
    {
        /// <summary>
        /// Gets an instance of an property configuration for a property on the entity type.
        /// </summary>
        /// <typeparam name="TProperty">Type of the property to get.</typeparam>
        /// <param name="propertExpression">Expression selecting the property to configure.</param>
        /// <returns>Property configuration instance.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> Property<TProperty>(Expression<Func<T, TProperty>> propertExpression)
        {
            var property = new XmlSchemaPropertyConfiguration<T, TProperty>(propertExpression);
            this.Properties.Add(property);

            return property;
        }
    }

    /// <summary>
    /// Configuration for an XML entity.
    /// </summary>
    public class XmlSchemaConfiguration
    {
        /// <summary>
        /// Gets the name of the element representing this entity.
        /// </summary>
        internal string ElementName { get; private set; }

        /// <summary>
        /// Gets the namespace of the element representing this entity.
        /// </summary>
        internal string ElementNamespace { get; private set; }

        /// <summary>
        /// Collection of properties that have configuration data.
        /// </summary>
        internal ICollection<XmlSchemaPropertyConfiguration> Properties { get; }

        /// <summary>
        /// Creates an instance of the schema configuration and initializes the internal state.
        /// </summary>
        public XmlSchemaConfiguration()
        {
            this.Properties = new Collection<XmlSchemaPropertyConfiguration>();
        }

        /// <summary>
        /// Gets an instance of an property configuration for a property on the entity type.
        /// </summary>
        /// <typeparam name="T">Type of the entity that the property is a child of.</typeparam>
        /// <typeparam name="TProperty">Type of the property to get.</typeparam>
        /// <param name="propertExpression">Expression selecting the property to configure.</param>
        /// <returns>Property configuration instance.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> Property<T, TProperty>(Expression<Func<T, TProperty>> propertExpression)
            where T : class
        {
            var property = new XmlSchemaPropertyConfiguration<T, TProperty>(propertExpression);
            this.Properties.Add(property);

            return property;
        }

        /// <summary>
        /// Sets the name of this entity.
        /// </summary>
        /// <param name="name">Name of the element.</param>
        /// <returns>Instance of the schema configuration with the root name configuration.</returns>
        public XmlSchemaConfiguration Name(string name)
        {
            this.ElementName = name;

            return this;
        }

        /// <summary>
        /// Sets the root namespace for this entity.
        /// </summary>
        /// <param name="ns">Namespace of the element</param>
        /// <returns>Instance of the schema configuration with the root namespace configuration.</returns>
        public XmlSchemaConfiguration Namespace(string ns)
        {
            this.ElementNamespace = ns;

            return this;
        }

        /// <summary>
        /// Applies the entity's configuration to the set of attribute overrides.
        /// </summary>
        /// <param name="overridesInstance">Instance of an attribute override object to add the entity's configuration to.</param>
        internal virtual void ApplyConfiguration(XmlAttributeOverrides overridesInstance)
        {
            foreach (var prop in this.Properties)
            {
                prop.ApplyConfigurationToContext(overridesInstance);
            }
        }

        /// <summary>
        /// Calculates the hash code for the Xml schema.
        /// </summary>
        /// <returns>Hash code of the Xml schema.</returns>
        public override int GetHashCode()
        {
            var hashCodes = new Collection<int>();

            foreach (var prop in this.Properties)
            {
                hashCodes.Add(prop.GetHashCode());
            }

            var orderedHashes = hashCodes.OrderBy(e => e)
                                         .ToList();
            int hash = 0;

            foreach (var h in orderedHashes)
            {
                hash ^= h;
            }

            return hash;
        }
    }
}
