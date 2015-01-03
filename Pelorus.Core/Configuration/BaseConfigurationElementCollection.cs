using System.Configuration;

namespace Pelorus.Core.Configuration
{
    /// <summary>
    /// Base class for configuration collections.
    /// </summary>
    /// <typeparam name="T">Type of the child elements of the collection.</typeparam>
    public abstract class BaseConfigurationElementCollection<T> : ConfigurationElementCollection
        where T : ConfigurationElement, new()
    {
        /// <summary>
        /// Name of the child elements.
        /// </summary>
        protected abstract string _childElementName { get; }

        /// <summary>
        /// Type of the collection type object.
        /// </summary>
        protected abstract ConfigurationElementCollectionType _collectionType { get; }

        /// <summary>
        /// Indicates if the collection is readonly.
        /// </summary>
        protected abstract bool _isReadonly { get; }

        /// <summary>
        /// Returns the key of an element of the array.
        /// </summary>
        /// <param name="element">Element to get the key.</param>
        /// <returns>Key of the given element.</returns>
        protected override abstract object GetElementKey(ConfigurationElement element);

        /// <summary>
        /// Type of the collection type object.
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType { get { return this._collectionType; } }

        /// <summary>
        /// Name of the child elements.
        /// </summary>
        protected override string ElementName { get { return this._childElementName; } }

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
            return new T();
        }

        /// <summary>
        /// Integer indexer of the collection.
        /// </summary>
        /// <param name="index">Index of the element to return.</param>
        /// <returns>Element at the given index of the collection.</returns>
        public T this[int index] { get { return (T) BaseGet(index); } }
    }
}
