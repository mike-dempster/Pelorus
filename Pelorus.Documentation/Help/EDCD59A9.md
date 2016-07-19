# BaseConfigurationElementCollection(*TElement*).Item Property (Int32)
 

Integer indexer of the collection.

**Namespace:**&nbsp;<a href="74405DDA">Pelorus.Core.Configuration</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.1 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public TElement this[
	int index
] { get; }
```

**VB**<br />
``` VB
Public ReadOnly Default Property Item ( 
	index As Integer
) As TElement
	Get
```

**C++**<br />
``` C++
public:
property TElement default[int index] {
	TElement get (int index);
}
```


#### Parameters
&nbsp;<dl><dt>index</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/td2s409d" target="_blank">System.Int32</a><br />Index of the element to return.</dd></dl>

#### Return Value
Type: <a href="CAF267CA">*TElement*</a><br />Element at the given index of the collection.

## See Also


#### Reference
<a href="CAF267CA">BaseConfigurationElementCollection(TElement) Class</a><br /><a href="C71FDD8">Item Overload</a><br /><a href="74405DDA">Pelorus.Core.Configuration Namespace</a><br />