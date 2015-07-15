using System;
using System.Linq.Expressions;
using System.Xml.Serialization;
using Pelorus.Core.Reflection;

namespace Pelorus.Core.Xml.Serialization
{
    /// <summary>
    /// Configures an entity for XML serialization.
    /// </summary>
    /// <typeparam name="T">Type of the entity to configure.</typeparam>
    /// <typeparam name="TProperty">Type of the property to configure.</typeparam>
    public class XmlSchemaPropertyConfiguration<T, TProperty> : XmlSchemaPropertyConfiguration
        where T : class
    {
        /// <summary>
        /// Namespace of the property.
        /// </summary>
        private string propertyNamespace;

        /// <summary>
        /// Attributes that apply to the property.
        /// </summary>
        internal XmlAttributes Attributes { get; set; }

        /// <summary>
        /// Expression identifying the property that is configured.
        /// </summary>
        internal Expression<Func<T, TProperty>> PropertyExpression { get; set; }

        /// <summary>
        /// Creates a new instance of the property configuration and initializes the internal state.
        /// </summary>
        /// <param name="expression">Expression identifying the property to configure.</param>
        public XmlSchemaPropertyConfiguration(Expression<Func<T, TProperty>> expression)
        {
            this.PropertyExpression = expression;
            this.Attributes = new XmlAttributes();
        }

        /// <summary>
        /// The property represents an XML attribute.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsAttribute()
        {
            this.Attributes.XmlAttribute = new XmlAttributeAttribute();

            return this;
        }

        /// <summary>
        /// The property represents an XML attribute.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsAttribute(string attributeName)
        {
            this.Attributes.XmlAttribute = new XmlAttributeAttribute(attributeName);

            return this;
        }

        /// <summary>
        /// The property represents an XML attribute.
        /// </summary>
        /// <param name="ns">Namespace of the attribute.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsAttribute(string ns, string attributeName)
        {
            this.Attributes.XmlAttribute = new XmlAttributeAttribute(attributeName)
            {
                Namespace = ns
            };

            return this;
        }

        /// <summary>
        /// The property represents an XML element.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsElement()
        {
            this.Attributes.XmlElements.Add(new XmlElementAttribute());

            return this;
        }

        /// <summary>
        /// The property represents an XML element.
        /// </summary>
        /// <param name="elementName">Name of the element that the proerty represents.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsElement(string elementName)
        {
            this.Attributes.XmlElements.Add(new XmlElementAttribute(elementName));

            return this;
        }

        /// <summary>
        /// The property represents an XML element.
        /// </summary>
        /// <param name="ns">Namespace of the XML element.</param>
        /// <param name="elementName">Name of the element that the proerty represents.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsElement(string ns, string elementName)
        {
            var elementAttribute = new XmlElementAttribute(elementName)
            {
                Namespace = ns
            };
            this.Attributes.XmlElements.Add(elementAttribute);

            return this;
        }

        /// <summary>
        /// The property represents an XML array.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsArray()
        {
            this.Attributes.XmlArray = new XmlArrayAttribute();

            return this;
        }

        /// <summary>
        /// The property represents an XML array.
        /// </summary>
        /// <param name="elementName">Name of the XML array element.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsArray(string elementName)
        {
            this.Attributes.XmlArray = new XmlArrayAttribute(elementName);

            return this;
        }

        /// <summary>
        /// The property represents an XML array.
        /// </summary>
        /// <param name="ns">Namespace of the XML array.</param>
        /// <param name="elementName">Name of the XML array element.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsArray(string ns, string elementName)
        {
            this.Attributes.XmlArray = new XmlArrayAttribute(elementName)
            {
                Namespace = ns
            };

            return this;
        }

        /// <summary>
        /// The property represents an XML array item.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsArrayItem()
        {
            this.Attributes.XmlArrayItems.Add(new XmlArrayItemAttribute());

            return this;
        }

        /// <summary>
        /// The property represents an XML array item.
        /// </summary>
        /// <param name="elementName">Name of the elements of the XML array.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsArrayItem(string elementName)
        {
            this.Attributes.XmlArrayItems.Add(new XmlArrayItemAttribute(elementName));

            return this;
        }

        /// <summary>
        /// The property represents an XML array item.
        /// </summary>
        /// <param name="ns">Namespace of the items in the XML array.</param>
        /// <param name="elementName">Name of the elements of the XML array.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsArrayItem(string ns, string elementName)
        {
            var arrayAttribute = new XmlArrayItemAttribute(elementName)
            {
                Namespace = ns
            };
            this.Attributes.XmlArrayItems.Add(arrayAttribute);

            return this;
        }

        /// <summary>
        /// The property should be ignored in the XML serialization and deserialization processes.
        /// </summary>
        public void Ignore()
        {
            this.Attributes.XmlIgnore = true;
        }

        /// <summary>
        /// The property is an XML enum type.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsEnum()
        {
            this.Attributes.XmlEnum = new XmlEnumAttribute();

            return this;
        }

        /// <summary>
        /// The property is an XML enum type.
        /// </summary>
        /// <param name="name">Name of the enumeration member.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsEnum(string name)
        {
            this.Attributes.XmlEnum = new XmlEnumAttribute(name);

            return this;
        }

        /// <summary>
        /// The property is the root of the XML graph.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsRoot()
        {
            this.Attributes.XmlRoot = new XmlRootAttribute();

            return this;
        }

        /// <summary>
        /// The property is the root of the XML graph.
        /// </summary>
        /// <param name="elementName">Name of the root element of the XML graph.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsRoot(string elementName)
        {
            this.Attributes.XmlRoot = new XmlRootAttribute(elementName);

            return this;
        }

        /// <summary>
        /// The property is the root of the XML graph.
        /// </summary>
        /// <param name="ns">Namespace of the root XML element.</param>
        /// <param name="elementName">Name of the root element of the XML graph.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsRoot(string ns, string elementName)
        {
            this.Attributes.XmlRoot = new XmlRootAttribute(elementName)
            {
                Namespace = ns
            };

            return this;
        }

        /// <summary>
        /// The property should be serialized as text.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsText()
        {
            var propertyInfo = PropertyInfoExtensions.Property(this.PropertyExpression);
            this.Attributes.XmlText = new XmlTextAttribute(propertyInfo.PropertyType);

            return this;
        }

        /// <summary>
        /// Specifies how the property is to be serialized or deserialized.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsType()
        {
            this.Attributes.XmlType = new XmlTypeAttribute();

            return this;
        }

        /// <summary>
        /// Specifies how the property is to be serialized or deserialized.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsType(string typeName)
        {
            this.Attributes.XmlType = new XmlTypeAttribute(typeName);

            return this;
        }

        /// <summary>
        /// Specifies how the property is to be serialized or deserialized.
        /// </summary>
        /// <param name="ns">Namespace of the type.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsType(string ns, string typeName)
        {
            this.Attributes.XmlType = new XmlTypeAttribute(typeName)
            {
                Namespace = ns
            };

            return this;
        }

        /// <summary>
        /// Sets the namespace to use for the property.
        /// </summary>
        /// <param name="ns">Namespace of the property.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> HasNamespace(string ns)
        {
            this.propertyNamespace = ns;

            return this;
        }

        /// <summary>
        /// Adds the configured attributes for the property to the given attribute override.
        /// </summary>
        /// <param name="overridesInstance">Instance of an attribute override object.</param>
        internal override void ApplyConfigurationToContext(XmlAttributeOverrides overridesInstance)
        {
            var propertyInfo = PropertyInfoExtensions.Property(this.PropertyExpression);

            if (null == propertyInfo.DeclaringType)
            {
                return;
            }

            this.SetNamespace();
            overridesInstance.Add(propertyInfo.DeclaringType, propertyInfo.Name, this.Attributes);
        }

        /// <summary>
        /// Sets the namespace on the attributes for the property.
        /// </summary>
        private void SetNamespace()
        {
            if (null == this.propertyNamespace)
            {
                return;
            }

            if (null != this.Attributes.XmlRoot)
            {
                this.Attributes.XmlRoot.Namespace = this.propertyNamespace;
            }

            if (null != this.Attributes.XmlArray)
            {
                this.Attributes.XmlArray.Namespace = this.propertyNamespace;
            }

            if (null != this.Attributes.XmlAttribute)
            {
                this.Attributes.XmlAttribute.Namespace = this.propertyNamespace;
            }

            if (null != this.Attributes.XmlArrayItems)
            {
                foreach (XmlArrayItemAttribute item in this.Attributes.XmlArrayItems)
                {
                    item.Namespace = this.propertyNamespace;
                }
            }

            if (null != this.Attributes.XmlElements)
            {
                foreach (XmlElementAttribute item in this.Attributes.XmlElements)
                {
                    item.Namespace = this.propertyNamespace;
                }
            }
        }
    }

    /// <summary>
    /// Base class for configuring an entity for XML serialization.
    /// </summary>
    public abstract class XmlSchemaPropertyConfiguration
    {
        /// <summary>
        /// Adds the configured attributes for the property to the given attribute override.
        /// </summary>
        /// <param name="overridesInstance">Instance of an attribute override object.</param>
        internal abstract void ApplyConfigurationToContext(XmlAttributeOverrides overridesInstance);
    }
}
