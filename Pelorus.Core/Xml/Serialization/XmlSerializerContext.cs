using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Pelorus.Core.Xml.Serialization
{
    /// <summary>
    /// Serializer context for configuring the XML schema and serializing or deserializing objects.
    /// </summary>
    public abstract class XmlSerializerContext
    {
        private Type rootType;
        private string rootElementName;

        /// <summary>
        /// Namespace table to use for serializing and deserializing XML.
        /// </summary>
        protected XmlSerializerNamespaces GlobalNamespaces;

        /// <summary>
        /// Collection of entity configurations for controlling the serialization process.
        /// </summary>
        protected ICollection<XmlSchemaConfiguration> Configurations { get; private set; }

        /// <summary>
        /// Creates a new instance of the context and initializes the internal state.
        /// </summary>
        protected XmlSerializerContext()
        {
            this.Configurations = new Collection<XmlSchemaConfiguration>();
            this.GlobalNamespaces = new XmlSerializerNamespaces();
            this.GlobalNamespaces.Add(string.Empty, string.Empty);
        }

        /// <summary>
        /// Defines a namespace in the context's namespace table.
        /// </summary>
        /// <param name="prefix">Prefix for the namespace.</param>
        /// <param name="ns">Namespace to add to the table.</param>
        public void HasNamespace(string prefix, string ns)
        {
            this.GlobalNamespaces.Add(prefix, ns);
        }

        /// <summary>
        /// Sets the default namespace for the context.
        /// </summary>
        /// <param name="ns">Default namespace for the context.</param>
        public void HasDefaultNamespace(string ns)
        {
            this.GlobalNamespaces.Add(string.Empty, ns);
        }

        /// <summary>
        /// Sets the root entity type for the context.
        /// </summary>
        /// <typeparam name="T">Type of the root entity</typeparam>
        public void SetRoot<T>()
        {
            this.rootType = typeof (T);
            this.rootElementName = this.rootType.Name;
        }

        /// <summary>
        /// Sets the root entity type for the context.
        /// </summary>
        /// <typeparam name="T">Type of the root entity</typeparam>
        /// <param name="elementName">Name of the root element.</param>
        public void SetRoot<T>(string elementName)
        {
            this.rootType = typeof (T);
            this.rootElementName = elementName;
        }

        public XmlSchemaConfiguration<T> ConfigureEntity<T>()
            where T : class
        {
            var configuration = new XmlSchemaConfiguration<T>();
            this.Configurations.Add(configuration);

            return configuration;
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given stream.
        /// </summary>
        /// <param name="stream">Stream to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        public void Serialize(Stream stream, object o)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(o.GetType(), config);
            serializer.Serialize(stream, o, this.GlobalNamespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given text writer.
        /// </summary>
        /// <param name="textWriter">Text writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        public void Serialize(TextWriter textWriter, object o)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(o.GetType(), config);
            serializer.Serialize(textWriter, o, this.GlobalNamespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given XML writer.
        /// </summary>
        /// <param name="xmlWriter">XML writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        public void Serialize(XmlWriter xmlWriter, object o)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(o.GetType(), config);
            serializer.Serialize(xmlWriter, o, this.GlobalNamespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given stream.
        /// </summary>
        /// <param name="stream">Stream to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        /// <param name="namespaces">Namespaces to use for the serialize process.</param>
        public void Serialize(Stream stream, object o, XmlSerializerNamespaces namespaces)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(o.GetType(), config);
            serializer.Serialize(stream, o, namespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given stream.
        /// </summary>
        /// <param name="textWriter">Text writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        /// <param name="namespaces">Namespaces to use for the serialize process.</param>
        public void Serialize(TextWriter textWriter, object o, XmlSerializerNamespaces namespaces)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(o.GetType(), config);
            serializer.Serialize(textWriter, o, namespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given stream.
        /// </summary>
        /// <param name="xmlWriter">XML writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        /// <param name="namespaces">Namespaces to use for the serialize process.</param>
        public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(o.GetType(), config);
            serializer.Serialize(xmlWriter, o, namespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given stream.
        /// </summary>
        /// <param name="xmlWriter">XML writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        /// <param name="namespaces">Namespaces to use for the serialize process.</param>
        /// <param name="encodingStyle">Encoding style to use for the serialize process.</param>
        public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(o.GetType(), config);
            serializer.Serialize(xmlWriter, o, namespaces, encodingStyle);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given stream.
        /// </summary>
        /// <param name="xmlWriter">XML writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        /// <param name="namespaces">Namespaces to use for the serialize process.</param>
        /// <param name="encodingStyle">Encoding style to use for the serialize process.</param>
        /// <param name="id">For SOAP encoded messages, the base used to generate id attributes.</param>
        public void Serialize(XmlWriter xmlWriter, object o, XmlSerializerNamespaces namespaces, string encodingStyle, string id)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(o.GetType(), config);
            serializer.Serialize(xmlWriter, o, namespaces, encodingStyle, id);
        }

        /// <summary>
        /// Deserializes a stream of XML data to an object.
        /// </summary>
        /// <typeparam name="T">Type of the object represented by the XML data.</typeparam>
        /// <param name="xmlStream">Stream of XML data representing the object instance.</param>
        /// <returns>Instance of the object in the stream.</returns>
        public T Deserialize<T>(Stream xmlStream)
            where T : class
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(typeof (T), config);
            var response = serializer.Deserialize(xmlStream);

            return response as T;
        }

        /// <summary>
        /// Deserializes a stream of XML data to an object.
        /// </summary>
        /// <param name="xmlStream">Stream of XML data representing the object instance.</param>
        /// <param name="type">Type of the object represented by the XML data.</param>
        /// <returns>Instance of the object in the stream.</returns>
        public object Deserialize(Stream xmlStream, Type type)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(type, config);
            var response = serializer.Deserialize(xmlStream);

            return response;
        }

        /// <summary>
        /// Deserializes the XML data in the given text reader to an object.
        /// </summary>
        /// <typeparam name="T">Type of the object represented by the XML data.</typeparam>
        /// <param name="textReader">Text reader of XML data representing the object instance.</param>
        /// <returns>Instance of the object in the text reader.</returns>
        public T Deserialize<T>(TextReader textReader)
            where T : class
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(typeof (T), config);
            var response = serializer.Deserialize(textReader);

            return response as T;
        }

        /// <summary>
        /// Deserializes the XML data in the given text reader to an object.
        /// </summary>
        /// <param name="textReader">Text reader of XML data representing the object instance.</param>
        /// <param name="type">Type of the object represented by the XML data.</param>
        /// <returns>Instance of the object in the text reader.</returns>
        public object Deserialize(TextReader textReader, Type type)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(type, config);
            var response = serializer.Deserialize(textReader);

            return response;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <typeparam name="T">Type of the object represented by the XML data.</typeparam>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public T Deserialize<T>(XmlReader xmlReader)
            where T : class
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(typeof (T), config);
            var response = serializer.Deserialize(xmlReader);

            return response as T;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <param name="type">Type of the object represented by the XML data.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public object Deserialize(XmlReader xmlReader, Type type)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(type, config);
            var response = serializer.Deserialize(xmlReader);

            return response;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <typeparam name="T">Type of the object represented by the XML data.</typeparam>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <param name="encodingStyle">Encoding style of the data in the XML reader.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public T Deserialize<T>(XmlReader xmlReader, string encodingStyle)
            where T : class
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(typeof (T), config);
            var response = serializer.Deserialize(xmlReader, encodingStyle);

            return response as T;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <param name="encodingStyle">Encoding style of the data in the XML reader.</param>
        /// <param name="type">Type of the object represented by the XML data.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public object Deserialize(XmlReader xmlReader, string encodingStyle, Type type)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(type, config);
            var response = serializer.Deserialize(xmlReader, encodingStyle);

            return response;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <typeparam name="T">Type of the object represented by the XML data.</typeparam>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <param name="events">An instance of the System.Xml.Serialization.XmlDeserializationEvents class.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public T Deserialize<T>(XmlReader xmlReader, XmlDeserializationEvents events)
            where T : class
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(typeof (T), config);
            var response = serializer.Deserialize(xmlReader, events);

            return response as T;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <param name="events">An instance of the System.Xml.Serialization.XmlDeserializationEvents class.</param>
        /// <param name="type">Type of the object represented by the XML data.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public object Deserialize(XmlReader xmlReader, XmlDeserializationEvents events, Type type)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(type, config);
            var response = serializer.Deserialize(xmlReader, events);

            return response;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <typeparam name="T">Type of the object represented by the XML data.</typeparam>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <param name="encodingStyle">Encoding style of the data in the XML reader.</param>
        /// <param name="events">An instance of the System.Xml.Serialization.XmlDeserializationEvents class.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public T Deserialize<T>(XmlReader xmlReader, string encodingStyle, XmlDeserializationEvents events)
            where T : class
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(typeof (T), config);
            var response = serializer.Deserialize(xmlReader, encodingStyle, events);

            return response as T;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <param name="encodingStyle">Encoding style of the data in the XML reader.</param>
        /// <param name="events">An instance of the System.Xml.Serialization.XmlDeserializationEvents class.</param>
        /// <param name="type">Type of the object represented by the XML data.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public object Deserialize(XmlReader xmlReader, string encodingStyle, XmlDeserializationEvents events, Type type)
        {
            var config = this.ConfigureContext();
            var serializer = new XmlSerializer(type, config);
            var response = serializer.Deserialize(xmlReader, encodingStyle, events);

            return response;
        }

        /// <summary>
        /// Configure the context for serializing or deserializing an object.
        /// </summary>
        /// <returns>Override object with the configured attributes for the context.</returns>
        private XmlAttributeOverrides ConfigureContext()
        {
            var overridesInstance = new XmlAttributeOverrides();

            foreach (var config in this.Configurations)
            {
                config.ApplyConfiguration(overridesInstance);
            }

            if (null == this.rootType)
            {
                return overridesInstance;
            }

            var rootEntityOverride = overridesInstance[this.rootType];

            if (null == rootEntityOverride)
            {
                var attrs = new XmlAttributes
                {
                    XmlRoot = new XmlRootAttribute(this.rootElementName)
                };
                overridesInstance.Add(this.rootType, attrs);
            }
            else
            {
                rootEntityOverride.XmlRoot = new XmlRootAttribute(this.rootElementName);
            }

            return overridesInstance;
        }
    }
}
