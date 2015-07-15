using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private XmlRootAttribute rootAttribute;

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

        /// <summary>
        /// Sets the entity type as the root entity in the XML graph.
        /// </summary>
        public void IsRoot()
        {
            this.rootAttribute = new XmlRootAttribute();
        }

        /// <summary>
        /// Sets the entity type as the root entity in the XML graph with the given element name.
        /// </summary>
        /// <param name="name">Name of the root element.</param>
        public void IsRoot(string name)
        {
            this.rootAttribute = new XmlRootAttribute(name);
        }

        /// <summary>
        /// Applies the entity's configuration to the set of attribute overrides.
        /// </summary>
        /// <param name="overridesInstance">Instance of an attribute override object to add the entity's configuration to.</param>
        internal override void ApplyConfiguration(XmlAttributeOverrides overridesInstance)
        {
            if (null != this.rootAttribute)
            {
                var attrs = new XmlAttributes
                {
                    XmlRoot = this.rootAttribute
                };

                overridesInstance.Add(typeof (T), attrs);
            }

            base.ApplyConfiguration(overridesInstance);
        }
    }

    /// <summary>
    /// Configuration for an XML entity.
    /// </summary>
    public class XmlSchemaConfiguration
    {
        /// <summary>
        /// Collection of properties that have configuration data.
        /// </summary>
        internal ICollection<XmlSchemaPropertyConfiguration> Properties { get; private set; }

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
    }
}
