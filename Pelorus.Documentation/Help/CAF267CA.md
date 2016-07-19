# BaseConfigurationElementCollection(*TElement*) Class
 

Base class for configuration collections.


## Inheritance Hierarchy
<a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">System.Object</a><br />&nbsp;&nbsp;<a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">System.Configuration.ConfigurationElement</a><br />&nbsp;&nbsp;&nbsp;&nbsp;<a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">System.Configuration.ConfigurationElementCollection</a><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pelorus.Core.Configuration.BaseConfigurationElementCollection(TElement)<br />
**Namespace:**&nbsp;<a href="74405DDA">Pelorus.Core.Configuration</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.1 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public abstract class BaseConfigurationElementCollection<TElement> : ConfigurationElementCollection
where TElement : new(), ConfigurationElement

```

**VB**<br />
``` VB
Public MustInherit Class BaseConfigurationElementCollection(Of TElement As {New, ConfigurationElement})
	Inherits ConfigurationElementCollection
```

**C++**<br />
``` C++
generic<typename TElement>
where TElement : gcnew(), ConfigurationElement
public ref class BaseConfigurationElementCollection abstract : public ConfigurationElementCollection
```


#### Type Parameters
&nbsp;<dl><dt>TElement</dt><dd>Type of the child elements of the collection.</dd></dl>&nbsp;
The BaseConfigurationElementCollection(TElement) type exposes the following members.


## Constructors
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="BDB143D0">BaseConfigurationElementCollection(TElement)(String)</a></td><td>
Create a new instance of the configuration collection and initialize the internal properties.</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="A2ED9621">BaseConfigurationElementCollection(TElement)(String, Boolean)</a></td><td>
Create a new instance of the configuration collection and initialize the internal properties.</td></tr></table>&nbsp;
<a href="#baseconfigurationelementcollection(*telement*)-class">Back to Top</a>

## Properties
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Protected property](media/protproperty.gif "Protected property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134167" target="_blank">AddElementName</a></td><td>
Gets or sets the name of the <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> to associate with the add operation in the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a> when overridden in a derived class.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected property](media/protproperty.gif "Protected property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134168" target="_blank">ClearElementName</a></td><td>
Gets or sets the name for the <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> to associate with the clear operation in the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a> when overridden in a derived class.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="EC92B6AA">CollectionType</a></td><td>
Type of the collection type object.
 (Overrides <a href="http://msdn2.microsoft.com/en-us/library/x4skd9kd" target="_blank">ConfigurationElementCollection.CollectionType</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/yf0s34t1" target="_blank">Count</a></td><td>
Gets the number of elements in the collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/dd412601" target="_blank">CurrentConfiguration</a></td><td>
Gets a reference to the top-level <a href="http://msdn2.microsoft.com/en-us/library/s7kc101z" target="_blank">Configuration</a> instance that represents the configuration hierarchy that the current <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> instance belongs to.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134142" target="_blank">ElementInformation</a></td><td>
Gets an <a href="http://msdn2.microsoft.com/en-us/library/ms134413" target="_blank">ElementInformation</a> object that contains the non-customizable information and functionality of the <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected property](media/protproperty.gif "Protected property")</td><td><a href="7C12F16A">ElementName</a></td><td>
Name of the child elements.
 (Overrides <a href="http://msdn2.microsoft.com/en-us/library/8f06bh6s" target="_blank">ConfigurationElementCollection.ElementName</a>.)</td></tr><tr><td>![Protected property](media/protproperty.gif "Protected property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134143" target="_blank">ElementProperty</a></td><td>
Gets the <a href="http://msdn2.microsoft.com/en-us/library/ms134174" target="_blank">ConfigurationElementProperty</a> object that represents the <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> object itself.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/adedfexe" target="_blank">EmitClear</a></td><td>
Gets or sets a value that specifies whether the collection has been cleared.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected property](media/protproperty.gif "Protected property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134144" target="_blank">EvaluationContext</a></td><td>
Gets the <a href="http://msdn2.microsoft.com/en-us/library/ms134368" target="_blank">ContextInformation</a> object for the <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected property](media/protproperty.gif "Protected property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/hh136640" target="_blank">HasContext</a></td><td>
Gets a value that indicates whether the <a href="http://msdn2.microsoft.com/en-us/library/dd412601" target="_blank">CurrentConfiguration</a> property is null.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134169" target="_blank">IsSynchronized</a></td><td>
Gets a value indicating whether access to the collection is synchronized.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected property](media/protproperty.gif "Protected property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/es150ftc" target="_blank">Item(ConfigurationProperty)</a></td><td>
Gets or sets a property or attribute of this configuration element.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected property](media/protproperty.gif "Protected property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/c8693ks1" target="_blank">Item(String)</a></td><td>
Gets or sets a property, attribute, or child element of this configuration element.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="EDCD59A9">Item(Int32)</a></td><td>
Integer indexer of the collection.</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134146" target="_blank">LockAllAttributesExcept</a></td><td>
Gets the collection of locked attributes.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134147" target="_blank">LockAllElementsExcept</a></td><td>
Gets the collection of locked elements.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134148" target="_blank">LockAttributes</a></td><td>
Gets the collection of locked attributes
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134149" target="_blank">LockElements</a></td><td>
Gets the collection of locked elements.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134150" target="_blank">LockItem</a></td><td>
Gets or sets a value indicating whether the element is locked.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected property](media/protproperty.gif "Protected property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/3kx8tt8d" target="_blank">Properties</a></td><td>
Gets the collection of properties.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected property](media/protproperty.gif "Protected property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134170" target="_blank">RemoveElementName</a></td><td>
Gets or sets the name of the <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> to associate with the remove operation in the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a> when overridden in a derived class.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Public property](media/pubproperty.gif "Public property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134171" target="_blank">SyncRoot</a></td><td>
Gets an object used to synchronize access to the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected property](media/protproperty.gif "Protected property")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ea6s6hb8" target="_blank">ThrowOnDuplicate</a></td><td>
Gets a value indicating whether an attempt to add a duplicate <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> to the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a> will cause an exception to be thrown.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr></table>&nbsp;
<a href="#baseconfigurationelementcollection(*telement*)-class">Back to Top</a>

## Methods
&nbsp;<table><tr><th></th><th>Name</th><th>Description</th></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/19tyhxbx" target="_blank">BaseAdd(ConfigurationElement)</a></td><td>
Adds a configuration element to the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/94tzb2x4" target="_blank">BaseAdd(ConfigurationElement, Boolean)</a></td><td>
Adds a configuration element to the configuration element collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/09d36k3s" target="_blank">BaseAdd(Int32, ConfigurationElement)</a></td><td>
Adds a configuration element to the configuration element collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/b4yf3zw6" target="_blank">BaseClear</a></td><td>
Removes all configuration element objects from the collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/e90fbaaw" target="_blank">BaseGet(Object)</a></td><td>
Returns the configuration element with the specified key.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/w493w5yy" target="_blank">BaseGet(Int32)</a></td><td>
Gets the configuration element at the specified index location.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/9ts4f970" target="_blank">BaseGetAllKeys</a></td><td>
Returns an array of the keys for all of the configuration elements contained in the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/zf0857te" target="_blank">BaseGetKey</a></td><td>
Gets the key for the <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> at the specified index location.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/8byca88s" target="_blank">BaseIndexOf</a></td><td>
Indicates the index of the specified <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/wk5tz03f" target="_blank">BaseIsRemoved</a></td><td>
Indicates whether the <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> with the specified key has been removed from the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/s4cs5s6w" target="_blank">BaseRemove</a></td><td>
Removes a <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> from the collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/wt92bf00" target="_blank">BaseRemoveAt</a></td><td>
Removes the <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> at the specified index location.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/d22w8c80" target="_blank">CopyTo</a></td><td>
Copies the contents of the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a> to an array.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="28268CB8">CreateNewElement()</a></td><td>
Creates a new child element.
 (Overrides <a href="http://msdn2.microsoft.com/en-us/library/ak7z48w8" target="_blank">ConfigurationElementCollection.CreateNewElement()</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ky49faah" target="_blank">CreateNewElement(String)</a></td><td>
Creates a new <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> when overridden in a derived class.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134126" target="_blank">DeserializeElement</a></td><td>
Reads XML from the configuration file.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/0eye6ky8" target="_blank">Equals</a></td><td>
Compares the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a> to the specified object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/4k87zsw7" target="_blank">Finalize</a></td><td>
Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="C6B1824B">GetElementKey</a></td><td>
Returns the key of an element of the array.
 (Overrides <a href="http://msdn2.microsoft.com/en-us/library/bxcte21d" target="_blank">ConfigurationElementCollection.GetElementKey(ConfigurationElement)</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134159" target="_blank">GetEnumerator</a></td><td>
Gets an <a href="http://msdn2.microsoft.com/en-us/library/1t2267t6" target="_blank">IEnumerator</a> which is used to iterate through the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/7w9k269c" target="_blank">GetHashCode</a></td><td>
Gets a unique value representing the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a> instance.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/dd642109" target="_blank">GetTransformedAssemblyString</a></td><td>
Returns the transformed version of the specified assembly name.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/dd642039" target="_blank">GetTransformedTypeString</a></td><td>
Returns the transformed version of the specified type name.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/dfwy45w9" target="_blank">GetType</a></td><td>
Gets the <a href="http://msdn2.microsoft.com/en-us/library/42892f65" target="_blank">Type</a> of the current instance.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134128" target="_blank">Init</a></td><td>
Sets the <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> object to its initial state.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/9kaww10k" target="_blank">InitializeDefault</a></td><td>
Used to initialize a default set of values for the <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="88D967D3">IsElementName</a></td><td>
Determines if the given string is the name of the child elements.
 (Overrides <a href="http://msdn2.microsoft.com/en-us/library/11833ks2" target="_blank">ConfigurationElementCollection.IsElementName(String)</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/00et13y9" target="_blank">IsElementRemovable</a></td><td>
Indicates whether the specified <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> can be removed from the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/sdfx3fsd" target="_blank">IsModified</a></td><td>
Indicates whether this <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a> has been modified since it was last saved or loaded when overridden in a derived class.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="97F0AD1F">IsReadOnly</a></td><td>
Determines if the collection is readonly.
 (Overrides <a href="http://msdn2.microsoft.com/en-us/library/ms134160" target="_blank">ConfigurationElementCollection.IsReadOnly()</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134130" target="_blank">ListErrors</a></td><td>
Adds the invalid-property errors in this <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a> object, and in all subelements, to the passed list.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/57ctke0a" target="_blank">MemberwiseClone</a></td><td>
Creates a shallow copy of the current <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134131" target="_blank">OnDeserializeUnrecognizedAttribute</a></td><td>
Gets a value indicating whether an unknown attribute is encountered during deserialization.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134161" target="_blank">OnDeserializeUnrecognizedElement</a></td><td>
Causes the configuration system to throw an exception.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134133" target="_blank">OnRequiredPropertyNotFound</a></td><td>
Throws an exception when a required property is not found.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134134" target="_blank">PostDeserialize</a></td><td>
Called after deserialization.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134135" target="_blank">PreSerialize</a></td><td>
Called before serialization.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134162" target="_blank">Reset</a></td><td>
Resets the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a> to its unmodified state when overridden in a derived class.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/5t09d48z" target="_blank">ResetModified</a></td><td>
Resets the value of the <a href="http://msdn2.microsoft.com/en-us/library/sdfx3fsd" target="_blank">IsModified()</a> property to false when overridden in a derived class.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134163" target="_blank">SerializeElement</a></td><td>
Writes the configuration data to an XML element in the configuration file when overridden in a derived class.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/yxcx3y27" target="_blank">SerializeToXmlElement</a></td><td>
Writes the outer tags of this configuration element to the configuration file when implemented in a derived class.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms224426" target="_blank">SetPropertyValue</a></td><td>
Sets a property to the specified value.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/kyx77cz3" target="_blank">ConfigurationElement</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms134164" target="_blank">SetReadOnly</a></td><td>
Sets the <a href="http://msdn2.microsoft.com/en-us/library/ms134160" target="_blank">IsReadOnly()</a> property for the <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a> object and for all sub-elements.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr><tr><td>![Public method](media/pubmethod.gif "Public method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/7bxwbwt2" target="_blank">ToString</a></td><td>
Returns a string that represents the current object.
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>.)</td></tr><tr><td>![Protected method](media/protmethod.gif "Protected method")</td><td><a href="http://msdn2.microsoft.com/en-us/library/ms224411" target="_blank">Unmerge</a></td><td>
Reverses the effect of merging configuration information from different levels of the configuration hierarchy
 (Inherited from <a href="http://msdn2.microsoft.com/en-us/library/a35we8et" target="_blank">ConfigurationElementCollection</a>.)</td></tr></table>&nbsp;
<a href="#baseconfigurationelementcollection(*telement*)-class">Back to Top</a>

## See Also


#### Reference
<a href="74405DDA">Pelorus.Core.Configuration Namespace</a><br />