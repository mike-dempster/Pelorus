# XmlSchemaPropertyConfiguration(*T*, *TProperty*).IsArrayItem Method (String, Boolean)
 

The property represents an XML array item.

**Namespace:**&nbsp;<a href="9052B9D6">Pelorus.Core.Xml.Serialization</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public XmlSchemaPropertyConfiguration<T, TProperty> IsArrayItem(
	string elementName,
	bool isNullable
)
```

**VB**<br />
``` VB
Public Function IsArrayItem ( 
	elementName As String,
	isNullable As Boolean
) As XmlSchemaPropertyConfiguration(Of T, TProperty)
```

**C++**<br />
``` C++
public:
XmlSchemaPropertyConfiguration<T, TProperty>^ IsArrayItem(
	String^ elementName, 
	bool isNullable
)
```


#### Parameters
&nbsp;<dl><dt>elementName</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Name of the elements of the XML array.</dd><dt>isNullable</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">System.Boolean</a><br />Indicates if the item is nullable.</dd></dl>

#### Return Value
Type: <a href="22622739">XmlSchemaPropertyConfiguration</a>(<a href="22622739">*T*</a>, <a href="22622739">*TProperty*</a>)<br />The property's updated configuration.

## See Also


#### Reference
<a href="22622739">XmlSchemaPropertyConfiguration(T, TProperty) Class</a><br /><a href="A9FC2FB0">IsArrayItem Overload</a><br /><a href="9052B9D6">Pelorus.Core.Xml.Serialization Namespace</a><br />