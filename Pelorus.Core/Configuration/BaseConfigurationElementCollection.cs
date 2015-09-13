using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Base class for configuration collections.
    /// </summary>
    /// <typeparam name="TElement">Type of the child elements of the collection.</typeparam>
    public abstract class BaseConfigurationElementCollection<TElement> : ConfigurationElementCollection
        where TElement : ConfigurationElement, new()
    {
        /// <summary>
        /// Name of the child elements.
        /// </summary>
        private readonly string _childElementName;

        /// <summary>
        /// Type of the collection type object.
        /// </summary>
        private readonly ConfigurationElementCollectionType _collectionType;

        /// <summary>
        /// Indicates if the collection is readonly.
        /// </summary>
        private readonly bool _isReadonly;

        /// <summary>
        /// Create a new instance of the configuration collection and initialize the internal properties.
        /// </summary>
        /// <param name="elementName">Name of the child elements.</param>
        protected BaseConfigurationElementCollection(string elementName) : this(elementName, false)
        {
        }

        /// <summary>
        /// Create a new instance of the configuration collection and initialize the internal properties.
        /// </summary>
        /// <param name="elementName">Name of the child elements.</param>
        /// <param name="isReadonly">Indicates if the collection is read only.</param>
        protected BaseConfigurationElementCollection(string elementName, bool isReadonly)
        {
            this._childElementName = elementName;
            this._isReadonly = isReadonly;
            this._collectionType = ConfigurationElementCollectionType.AddRemoveClearMap;
        }

        /// <summary>
        /// Returns the key of an element of the array.
        /// </summary>
        /// <param name="element">Element to get the key.</param>
        /// <returns>Key of the given element.</returns>
        protected override abstract object GetElementKey(ConfigurationElement element);

        /// <summary>
        /// Type of the collection type object.
        /// </summary>
        // ReSharper disable ConvertToAutoProperty
        public override ConfigurationElementCollectionType CollectionType => this._collectionType;
        // ReSharper restore ConvertToAutoProperty

        /// <summary>
        /// Name of the child elements.
        /// </summary>
        protected override string ElementName => this._childElementName;

        /// <summary>
        /// Determines if the collection is readonly.
        /// </summary>
        /// <returns>True if the collection is readonly otherwise false.</returns>
        public override bool IsReadOnly()
        {
            return this._isReadonly;
        }

        /// <summary>
        /// Determines if the given string is the name of the child elements.
        /// </summary>
        /// <param name="elementName">String to compare to the element name.</param>
        /// <returns>True if the string is the element name otherwise false.</returns>
        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(this._childElementName);
        }

        /// <summary>
        /// Creates a new child element.
        /// </summary>
        /// <returns>New instance of the child element type.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new TElement();
        }

        /// <summary>
        /// Integer indexer of the collection.
        /// </summary>
        /// <param name="index">Index of the element to return.</param>
        /// <returns>Element at the given index of the collection.</returns>
        public TElement this[int index] => (TElement) this.BaseGet(index);
    }
}
