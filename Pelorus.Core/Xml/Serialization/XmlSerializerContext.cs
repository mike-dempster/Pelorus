using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Pelorus.Core.Xml.Serialization
{
    /// <summary>
    /// Serializer context for configuring the XML schema and serializing or deserializing objects.
    /// </summary>
    public abstract class XmlSerializerContext
    {
        // ReSharper disable StaticMemberInGenericType
        private static readonly Hashtable _serializers = new Hashtable();
        // ReSharper restore StaticMemberInGenericType

        /// <summary>
        /// Namespace table to use for serializing and deserializing XML.
        /// </summary>
        protected XmlSerializerNamespaces GlobalNamespaces;

        /// <summary>
        /// Collection of entity configurations for controlling the serialization process.
        /// </summary>
        protected IDictionary<Type, XmlSchemaConfiguration> Entities { get; }

        /// <summary>
        /// Creates a new instance of the context and initializes the internal state.
        /// </summary>
        protected XmlSerializerContext()
        {
            this.Entities = new Dictionary<Type, XmlSchemaConfiguration>();
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
        /// Serializes an object and outputs the XML to the given stream.
        /// </summary>
        /// <param name="stream">Stream to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        public void Serialize(Stream stream, object o)
        {
            var serializer = this.GetSerializer(o.GetType());
            serializer.Serialize(stream, o, this.GlobalNamespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given text writer.
        /// </summary>
        /// <param name="textWriter">Text writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        public void Serialize(TextWriter textWriter, object o)
        {
            var serializer = this.GetSerializer(o.GetType());
            serializer.Serialize(textWriter, o, this.GlobalNamespaces);
        }

        /// <summary>
        /// Serializes an object and outputs the XML to the given XML writer.
        /// </summary>
        /// <param name="xmlWriter">XML writer to output the serialized object graph to.</param>
        /// <param name="o">Object to serialize.</param>
        public void Serialize(XmlWriter xmlWriter, object o)
        {
            var serializer = this.GetSerializer(o.GetType());
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
            var serializer = this.GetSerializer(o.GetType());
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
            var serializer = this.GetSerializer(o.GetType());
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
            var serializer = this.GetSerializer(o.GetType());
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
            var serializer = this.GetSerializer(o.GetType());
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
            var serializer = this.GetSerializer(o.GetType());
            serializer.Serialize(xmlWriter, o, namespaces, encodingStyle, id);
        }

        /// <summary>
        /// Deserializes a stream of XML data to an object.
        /// </summary>
        /// <typeparam name="TEntity">Type represented by the XML document.</typeparam>
        /// <param name="xmlStream">Stream of XML data representing the object instance.</param>
        /// <returns>Instance of the object in the stream.</returns>
        public TEntity Deserialize<TEntity>(Stream xmlStream)
            where TEntity : class
        {
            var serializer = this.GetSerializer(typeof (TEntity));
            var response = serializer.Deserialize(xmlStream);

            return response as TEntity;
        }

        /// <summary>
        /// Deserializes the XML data in the given text reader to an object.
        /// </summary>
        /// <typeparam name="TEntity">Type represented by the XML document.</typeparam>
        /// <param name="textReader">Text reader of XML data representing the object instance.</param>
        /// <returns>Instance of the object in the text reader.</returns>
        public TEntity Deserialize<TEntity>(TextReader textReader)
            where TEntity : class
        {
            var serializer = this.GetSerializer(typeof (TEntity));
            var response = serializer.Deserialize(textReader);

            return response as TEntity;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <typeparam name="TEntity">Type represented by the XML document.</typeparam>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public TEntity Deserialize<TEntity>(XmlReader xmlReader)
            where TEntity : class
        {
            var serializer = this.GetSerializer(typeof (TEntity));
            var response = serializer.Deserialize(xmlReader);

            return response as TEntity;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <typeparam name="TEntity">Type represented by the XML document.</typeparam>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <param name="encodingStyle">Encoding style of the data in the XML reader.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public TEntity Deserialize<TEntity>(XmlReader xmlReader, string encodingStyle)
            where TEntity : class
        {
            var serializer = this.GetSerializer(typeof (TEntity));
            var response = serializer.Deserialize(xmlReader, encodingStyle);

            return response as TEntity;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <typeparam name="TEntity">Type represented by the XML document.</typeparam>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <param name="events">An instance of the System.Xml.Serialization.XmlDeserializationEvents class.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public TEntity Deserialize<TEntity>(XmlReader xmlReader, XmlDeserializationEvents events)
            where TEntity : class
        {
            var serializer = this.GetSerializer(typeof (TEntity));
            var response = serializer.Deserialize(xmlReader, events);

            return response as TEntity;
        }

        /// <summary>
        /// Deserializes the XML data in the given XML reader to an object.
        /// </summary>
        /// <typeparam name="TEntity">Type represented by the XML document.</typeparam>
        /// <param name="xmlReader">XML reader of the data representing the object instance.</param>
        /// <param name="encodingStyle">Encoding style of the data in the XML reader.</param>
        /// <param name="events">An instance of the System.Xml.Serialization.XmlDeserializationEvents class.</param>
        /// <returns>Instance of the object in the XML reader.</returns>
        public TEntity Deserialize<TEntity>(XmlReader xmlReader, string encodingStyle, XmlDeserializationEvents events)
            where TEntity : class
        {
            var serializer = this.GetSerializer(typeof (TEntity));
            var response = serializer.Deserialize(xmlReader, encodingStyle, events);

            return response as TEntity;
        }

        /// <summary>
        /// Calculate the hash code for the serializer context.
        /// </summary>
        /// <returns>Hash code of the serializer context.</returns>
        public override int GetHashCode()
        {
            int hash = 0;

            foreach (var config in this.Entities)
            {
                hash ^= config.Value.GetHashCode();
            }

            return hash;
        }

        /// <summary>
        /// Gets the configuration for the given entity type.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to get the configuration for.</typeparam>
        /// <returns>Configuration instance for the given entity type.</returns>
        protected XmlSchemaConfiguration<TEntity> Entity<TEntity>()
            where TEntity : class
        {
            XmlSchemaConfiguration entityConfig;

            if (true == this.Entities.TryGetValue(typeof (TEntity), out entityConfig))
            {
                return entityConfig as XmlSchemaConfiguration<TEntity>;
            }

            entityConfig = new XmlSchemaConfiguration<TEntity>();
            this.Entities.Add(typeof (TEntity), entityConfig);

            return (XmlSchemaConfiguration<TEntity>) entityConfig;
        }

        /// <summary>
        /// Gets the configuration for the given entity type.
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity to get the configuration for.</typeparam>
        /// <param name="name">Name of the element representing the given entity type.</param>
        /// <returns>Configuration instance for the given entity type.</returns>
        protected XmlSchemaConfiguration<TEntity> Entity<TEntity>(string name)
            where TEntity : class
        {
            XmlSchemaConfiguration entityConfig;

            if (true == this.Entities.TryGetValue(typeof (TEntity), out entityConfig))
            {
                entityConfig.Name(name);
                return entityConfig as XmlSchemaConfiguration<TEntity>;
            }

            entityConfig = new XmlSchemaConfiguration<TEntity>();
            entityConfig.Name(name);
            this.Entities.Add(typeof (TEntity), entityConfig);

            return (XmlSchemaConfiguration<TEntity>) entityConfig;
        }

        /// <summary>
        /// Gets a serializer instance.
        /// </summary>
        /// <param name="type">Type that is to be serialized or deserialized.</param>
        /// <returns>Instance of an Xml serializer.</returns>
        private XmlSerializer GetSerializer(Type type)
        {
            int rootHash = 0;
            int hash = rootHash ^ type.MetadataToken ^ this.GetHashCode();

            if (_serializers.ContainsKey(hash))
            {
                return (XmlSerializer) _serializers[hash];
            }

            var entityConfig = this.Entities[type];
            string rootName = entityConfig.ElementName;
            var config = this.ConfigureContext(type, rootName);
            var serializer = new XmlSerializer(type, config);
            _serializers.Add(hash, serializer);

            return serializer;
        }

        /// <summary>
        /// Configure the context for serializing or deserializing an object.
        /// </summary>
        /// <param name="rootType">Type of the root element.</param>
        /// <param name="rootName">Name of the root element.</param>
        /// <returns>Override object with the configured attributes for the context.</returns>
        private XmlAttributeOverrides ConfigureContext(Type rootType, string rootName)
        {
            var overridesInstance = new XmlAttributeOverrides();

            foreach (var config in this.Entities)
            {
                config.Value.ApplyConfiguration(overridesInstance);
            }

            var rootEntityKvp = this.Entities.Single(e => e.Key == rootType);
            var rootEntity = rootEntityKvp.Value;
            string rootElementName = rootName;

            if (string.IsNullOrWhiteSpace(rootElementName))
            {
                rootElementName = string.IsNullOrWhiteSpace(rootEntity.ElementNamespace) ? rootType.Name : rootEntity.ElementNamespace;
            }

            var rootEntityOverride = new XmlAttributes
            {
                XmlRoot = new XmlRootAttribute(rootElementName)
            };

            if (false == string.IsNullOrWhiteSpace(rootEntity.ElementNamespace))
            {
                rootEntityOverride.XmlRoot.Namespace = rootEntity.ElementNamespace;
            }

            overridesInstance.Add(rootType, string.Empty, rootEntityOverride);

            return overridesInstance;
        }
    }
}
