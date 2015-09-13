using Pelorus.Core.Reflection;
using System;
using System.Linq.Expressions;
using System.Xml.Serialization;

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
        /// Attribute to apply to the property.
        /// </summary>
        private Attribute thisPropertyAttribute;

        /// <summary>
        /// Name of the property.
        /// </summary>
        private string propertyName;

        /// <summary>
        /// Indicates if the property is nullable.
        /// </summary>
        private bool propertyIsNullable;

        /// <summary>
        /// Namespace of the property.
        /// </summary>
        private string propertyNamespace;

        /// <summary>
        /// Creates a new instance of the property configuration and initializes the internal state.
        /// </summary>
        /// <param name="expression">Expression identifying the property to configure.</param>
        public XmlSchemaPropertyConfiguration(Expression<Func<T, TProperty>> expression)
        {
            this.PropertyExpression = expression;
            this.Attributes = new XmlAttributes();
            this.propertyIsNullable = true;
        }

        /// <summary>
        /// The property represents an XML attribute.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsAttribute()
        {
            this.thisPropertyAttribute = new XmlAttributeAttribute();

            return this;
        }

        /// <summary>
        /// The property represents an XML attribute.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsAttribute(string attributeName)
        {
            this.thisPropertyAttribute = new XmlAttributeAttribute();
            this.propertyName = attributeName;

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
            this.thisPropertyAttribute = new XmlAttributeAttribute();
            this.propertyName = attributeName;
            this.propertyNamespace = ns;

            return this;
        }

        /// <summary>
        /// The property represents an XML element.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsElement()
        {
            this.thisPropertyAttribute = new XmlElementAttribute();

            return this;
        }

        /// <summary>
        /// The property represents an XML element.
        /// </summary>
        /// <param name="elementName">Name of the element that the proerty represents.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsElement(string elementName)
        {
            this.propertyName = elementName;
            this.thisPropertyAttribute = new XmlElementAttribute();

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
            this.propertyNamespace = ns;
            this.propertyName = elementName;
            this.thisPropertyAttribute = new XmlElementAttribute(elementName);

            return this;
        }

        /// <summary>
        /// The property represents an XML element.
        /// </summary>
        /// <param name="elementName">Name of the element that the proerty represents.</param>
        /// <param name="isNullable">Indicates if the element is nullable.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsElement(string elementName, bool isNullable)
        {
            this.propertyName = elementName;
            this.propertyIsNullable = isNullable;
            this.thisPropertyAttribute = new XmlElementAttribute(elementName);

            return this;
        }

        /// <summary>
        /// The property represents an XML element.
        /// </summary>
        /// <param name="ns">Namespace of the XML element.</param>
        /// <param name="elementName">Name of the element that the proerty represents.</param>
        /// <param name="isNullable">Indicates if the element is nullable.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsElement(string ns, string elementName, bool isNullable)
        {
            this.propertyNamespace = ns;
            this.propertyName = elementName;
            this.propertyIsNullable = isNullable;
            this.thisPropertyAttribute = new XmlElementAttribute(elementName);

            return this;
        }

        /// <summary>
        /// The property represents an XML array.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsArray()
        {
            this.thisPropertyAttribute = new XmlArrayAttribute();

            return this;
        }

        /// <summary>
        /// The property represents an XML array.
        /// </summary>
        /// <param name="elementName">Name of the XML array element.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsArray(string elementName)
        {
            this.propertyName = elementName;
            this.thisPropertyAttribute = new XmlArrayAttribute();

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
            this.propertyNamespace = ns;
            this.propertyName = elementName;
            this.thisPropertyAttribute = new XmlArrayAttribute();

            return this;
        }

        /// <summary>
        /// The property represents an XML array item.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsArrayItem()
        {
            this.thisPropertyAttribute = new XmlArrayItemAttribute();

            return this;
        }

        /// <summary>
        /// The property represents an XML array item.
        /// </summary>
        /// <param name="elementName">Name of the elements of the XML array.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsArrayItem(string elementName)
        {
            this.propertyName = elementName;
            this.thisPropertyAttribute = new XmlArrayItemAttribute();

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
            this.propertyNamespace = ns;
            this.propertyName = elementName;
            this.thisPropertyAttribute = new XmlArrayItemAttribute();

            return this;
        }

        /// <summary>
        /// The property represents an XML array item.
        /// </summary>
        /// <param name="elementName">Name of the elements of the XML array.</param>
        /// <param name="isNullable">Indicates if the item is nullable.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsArrayItem(string elementName, bool isNullable)
        {
            this.propertyName = elementName;
            this.propertyIsNullable = isNullable;
            this.thisPropertyAttribute = new XmlArrayItemAttribute();

            return this;
        }

        /// <summary>
        /// The property represents an XML array item.
        /// </summary>
        /// <param name="ns">Namespace of the items in the XML array.</param>
        /// <param name="elementName">Name of the elements of the XML array.</param>
        /// <param name="isNullable">Indicates if the item is nullable.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsArrayItem(string ns, string elementName, bool isNullable)
        {
            this.propertyNamespace = ns;
            this.propertyName = elementName;
            this.propertyIsNullable = isNullable;
            this.thisPropertyAttribute = new XmlArrayItemAttribute();

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
            this.thisPropertyAttribute = new XmlEnumAttribute();

            return this;
        }

        /// <summary>
        /// The property is an XML enum type.
        /// </summary>
        /// <param name="name">Name of the enumeration member.</param>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsEnum(string name)
        {
            this.propertyName = name;
            this.thisPropertyAttribute = new XmlEnumAttribute();

            return this;
        }

        /// <summary>
        /// The property should be serialized as text.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsText()
        {
            var propertyInfo = PropertyInfoExtensions.Property(this.PropertyExpression);
            this.thisPropertyAttribute = new XmlTextAttribute(propertyInfo.PropertyType);

            return this;
        }

        /// <summary>
        /// Makes the property not nullable.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsRequired()
        {
            this.propertyIsNullable = false;

            return this;
        }

        /// <summary>
        /// Makes the property nullable.
        /// </summary>
        /// <returns>The property's updated configuration.</returns>
        public XmlSchemaPropertyConfiguration<T, TProperty> IsOptional()
        {
            this.propertyIsNullable = true;

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
        /// Get the hash code of the property schema.
        /// </summary>
        /// <returns>Hash code of the property schema.</returns>
        // ReSharper disable NonReadonlyMemberInGetHashCode
        public override int GetHashCode()
        {
            var propertyInfo = PropertyInfoExtensions.Property(this.PropertyExpression);
            var attribute = this.thisPropertyAttribute as XmlAttributeAttribute;

            if (null != attribute)
            {
                int hash = this.GetAttributeHash(attribute);
                hash ^= propertyInfo.MetadataToken;

                return hash;
            }

            var element = this.thisPropertyAttribute as XmlElementAttribute;

            if (null != element)
            {
                int hash = this.GetElementHash(element);
                hash ^= propertyInfo.MetadataToken;

                return hash;
            }

            var array = this.thisPropertyAttribute as XmlArrayAttribute;

            if (null != array)
            {
                int hash = this.GetArrayHash(array);
                hash ^= propertyInfo.MetadataToken;

                return hash;
            }

            var arrayItem = this.thisPropertyAttribute as XmlArrayItemAttribute;

            if (null != arrayItem)
            {
                int hash = this.GetArrayItemHash(arrayItem);
                hash ^= propertyInfo.MetadataToken;

                return hash;
            }

            var enumAttribute = this.thisPropertyAttribute as XmlEnumAttribute;

            if (null != enumAttribute)
            {
                int hash = this.GetEnumHash(enumAttribute);
                hash ^= propertyInfo.MetadataToken;

                return hash;
            }

            var text = this.thisPropertyAttribute as XmlTextAttribute;

            if (null == text)
            {
                return 0;
            }

            int textHash = this.GetTextHash(text);
            textHash ^= propertyInfo.MetadataToken;

            return textHash;
        }
        // ReSharper restore NonReadonlyMemberInGetHashCode

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

            this.ConfigureAttributes();
            overridesInstance.Add(propertyInfo.DeclaringType, propertyInfo.Name, this.Attributes);
        }

        /// <summary>
        /// Configures the properties of the attributes.
        /// </summary>
        private void ConfigureAttributes()
        {
            var attribute = this.thisPropertyAttribute as XmlAttributeAttribute;

            if (null != attribute)
            {
                this.ConfigureAttribute(attribute);
                return;
            }

            var element = this.thisPropertyAttribute as XmlElementAttribute;

            if (null != element)
            {
                this.ConfigureElement(element);
                return;
            }

            var array = this.thisPropertyAttribute as XmlArrayAttribute;

            if (null != array)
            {
                this.ConfigureArray(array);
                return;
            }

            var arrayItem = this.thisPropertyAttribute as XmlArrayItemAttribute;

            if (null != arrayItem)
            {
                this.ConfigureArrayItem(arrayItem);
                return;
            }

            var enumAttribute = this.thisPropertyAttribute as XmlEnumAttribute;

            if (null != enumAttribute)
            {
                this.ConfigureEnum(enumAttribute);
                return;
            }

            var text = this.thisPropertyAttribute as XmlTextAttribute;

            if (null == text)
            {
                return;
            }

            this.ConfigureText(text);
        }

        /// <summary>
        /// Configures the property as an attribute.
        /// </summary>
        /// <param name="attribute">Attribute attribute to apply to the property.</param>
        private void ConfigureAttribute(XmlAttributeAttribute attribute)
        {
            if (false == string.IsNullOrWhiteSpace(this.propertyName))
            {
                attribute.AttributeName = this.propertyName;
            }

            if (false == string.IsNullOrWhiteSpace(this.propertyNamespace))
            {
                attribute.Namespace = this.propertyNamespace;
            }

            this.Attributes.XmlAttribute = attribute;
        }

        /// <summary>
        /// Configures the property as an element.
        /// </summary>
        /// <param name="element">Element attribute to apply to the property.</param>
        private void ConfigureElement(XmlElementAttribute element)
        {
            if (false == string.IsNullOrWhiteSpace(this.propertyName))
            {
                element.ElementName = this.propertyName;
            }

            if (false == string.IsNullOrWhiteSpace(this.propertyNamespace))
            {
                element.Namespace = this.propertyNamespace;
            }

            element.IsNullable = this.propertyIsNullable;
            this.Attributes.XmlElements.Add(element);
        }

        /// <summary>
        /// Configures the property as an array.
        /// </summary>
        /// <param name="array">Array attribute to apply to the property.</param>
        private void ConfigureArray(XmlArrayAttribute array)
        {
            if (false == string.IsNullOrWhiteSpace(this.propertyName))
            {
                array.ElementName = this.propertyName;
            }

            if (false == string.IsNullOrWhiteSpace(this.propertyNamespace))
            {
                array.Namespace = this.propertyNamespace;
            }

            array.IsNullable = this.propertyIsNullable;
            this.Attributes.XmlArray = array;
        }

        /// <summary>
        /// Configures the property as an array item.
        /// </summary>
        /// <param name="arrayItem">Array item attribute to apply to the property.</param>
        private void ConfigureArrayItem(XmlArrayItemAttribute arrayItem)
        {
            if (false == string.IsNullOrWhiteSpace(this.propertyName))
            {
                arrayItem.ElementName = this.propertyName;
            }

            if (false == string.IsNullOrWhiteSpace(this.propertyNamespace))
            {
                arrayItem.Namespace = this.propertyNamespace;
            }

            arrayItem.IsNullable = this.propertyIsNullable;
            this.Attributes.XmlArrayItems.Add(arrayItem);
        }

        /// <summary>
        /// Configures the property as an enum.
        /// </summary>
        /// <param name="enumAttribute">Enum attribute to apply to the property.</param>
        private void ConfigureEnum(XmlEnumAttribute enumAttribute)
        {
            if (false == string.IsNullOrWhiteSpace(this.propertyName))
            {
                enumAttribute.Name = this.propertyName;
            }

            this.Attributes.XmlEnum = enumAttribute;
        }

        /// <summary>
        /// Configures the property as text.
        /// </summary>
        /// <param name="text">Text attribute to apply to the property.</param>
        private void ConfigureText(XmlTextAttribute text)
        {
            this.Attributes.XmlText = text;
        }

        /// <summary>
        /// Calculates the hash code for an Xml attribute attribute.
        /// </summary>
        /// <param name="attributeAttribute">Attribute to calculate the hash code for.</param>
        /// <returns>Hash code of the given Xml attribute attribute.</returns>
        private int GetAttributeHash(XmlAttributeAttribute attributeAttribute)
        {
            int hash = (attributeAttribute.AttributeName ?? string.Empty).GetHashCode();
            hash ^= (attributeAttribute.DataType ?? string.Empty).GetHashCode();
            hash ^= attributeAttribute.Form.GetHashCode();
            hash ^= (attributeAttribute.Namespace ?? string.Empty).GetHashCode();

            if (null != attributeAttribute.Type)
            {
                hash ^= attributeAttribute.Type.MetadataToken;
            }

            return hash;
        }

        /// <summary>
        /// Calculates the hash code for an Xml element attribute.
        /// </summary>
        /// <param name="elementAttribute">Attribute to calculate the hash code for.</param>
        /// <returns>Hash code of the given Xml element attribute.</returns>
        private int GetElementHash(XmlElementAttribute elementAttribute)
        {
            int hash = elementAttribute.Order.GetHashCode();
            hash ^= (elementAttribute.DataType ?? string.Empty).GetHashCode();
            hash ^= (elementAttribute.ElementName ?? string.Empty).GetHashCode();
            hash ^= elementAttribute.Form.GetHashCode();
            hash ^= elementAttribute.IsNullable.GetHashCode();
            hash ^= (elementAttribute.Namespace ?? string.Empty).GetHashCode();

            if (null != elementAttribute.Type)
            {
                hash ^= elementAttribute.Type.MetadataToken;
            }

            return hash;
        }

        /// <summary>
        /// Calculates the hash code for an Xml array attribute.
        /// </summary>
        /// <param name="arrayAttribute">Attribute to calculate the hash code for.</param>
        /// <returns>Hash code of the given Xml array attribute.</returns>
        private int GetArrayHash(XmlArrayAttribute arrayAttribute)
        {
            int hash = arrayAttribute.Order.GetHashCode();
            hash ^= (arrayAttribute.ElementName ?? string.Empty).GetHashCode();
            hash ^= arrayAttribute.Form.GetHashCode();
            hash ^= arrayAttribute.IsNullable.GetHashCode();
            hash ^= (arrayAttribute.Namespace ?? string.Empty).GetHashCode();

            return hash;
        }

        /// <summary>
        /// Calculates the hash code for an Xml array item attribute.
        /// </summary>
        /// <param name="arrayItemAttribute">Attribute to calculate the hash code for.</param>
        /// <returns>Hash code of the given Xml array item attribute.</returns>
        private int GetArrayItemHash(XmlArrayItemAttribute arrayItemAttribute)
        {
            int hash = arrayItemAttribute.NestingLevel.GetHashCode();
            hash ^= (arrayItemAttribute.DataType ?? string.Empty).GetHashCode();
            hash ^= (arrayItemAttribute.ElementName ?? string.Empty).GetHashCode();
            hash ^= arrayItemAttribute.Form.GetHashCode();
            hash ^= arrayItemAttribute.IsNullable.GetHashCode();
            hash ^= (arrayItemAttribute.Namespace ?? string.Empty).GetHashCode();

            if (null != arrayItemAttribute.Type)
            {
                hash ^= arrayItemAttribute.Type.MetadataToken;
            }

            return hash;
        }

        /// <summary>
        /// Calculates the hash code for an Xml enum attribute.
        /// </summary>
        /// <param name="enumAttribute">Attribute to calculate the hash code for.</param>
        /// <returns>Hash code of the given Xml enum attribute.</returns>
        private int GetEnumHash(XmlEnumAttribute enumAttribute)
        {
            int hash = enumAttribute.Name.GetHashCode();

            return hash;
        }

        /// <summary>
        /// Calculates the hash code for an Xml text attribute.
        /// </summary>
        /// <param name="textAttribute">Attribute to calculate the hash code for.</param>
        /// <returns>Hash code of the given Xml text attribute.</returns>
        private int GetTextHash(XmlTextAttribute textAttribute)
        {
            int hash = (textAttribute.DataType ?? string.Empty).GetHashCode();

            if (null != textAttribute.Type)
            {
                hash ^= textAttribute.Type.MetadataToken;
            }

            return hash;
        }
    }

    /// <summary>
    /// Base class for configuring an entity for XML serialization.
    /// </summary>
    public abstract class XmlSchemaPropertyConfiguration
    {
        /// <summary>
        /// Attributes that apply to the property.
        /// </summary>
        internal XmlAttributes Attributes { get; set; }

        /// <summary>
        /// Expression identifying the property that is configured.
        /// </summary>
        internal LambdaExpression PropertyExpression { get; set; }

        /// <summary>
        /// Adds the configured attributes for the property to the given attribute override.
        /// </summary>
        /// <param name="overridesInstance">Instance of an attribute override object.</param>
        internal abstract void ApplyConfigurationToContext(XmlAttributeOverrides overridesInstance);
    }
}
