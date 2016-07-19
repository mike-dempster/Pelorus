# XmlSchemaConfiguration.Property(*T*, *TProperty*) Method 
 

Gets an instance of an property configuration for a property on the entity type.

**Namespace:**&nbsp;<a href="9052B9D6">Pelorus.Core.Xml.Serialization</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public XmlSchemaPropertyConfiguration<T, TProperty> Property<T, TProperty>(
	Expression<Func<T, TProperty>> propertExpression
)
where T : class

```

**VB**<br />
``` VB
Public Function Property(Of T As Class, TProperty) ( 
	propertExpression As Expression(Of Func(Of T, TProperty))
) As XmlSchemaPropertyConfiguration(Of T, TProperty)
```

**C++**<br />
``` C++
public:
generic<typename T, typename TProperty>
where T : ref class
XmlSchemaPropertyConfiguration<T, TProperty>^ Property(
	Expression<Func<T, TProperty>^>^ propertExpression
)
```


#### Parameters
&nbsp;<dl><dt>propertExpression</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/bb335710" target="_blank">System.Linq.Expressions.Expression</a>(<a href="http://msdn2.microsoft.com/en-us/library/bb549151" target="_blank">Func</a>(*T*, *TProperty*))<br />Expression selecting the property to configure.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>Type of the entity that the property is a child of.</dd><dt>TProperty</dt><dd>Type of the property to get.</dd></dl>

#### Return Value
Type: <a href="22622739">XmlSchemaPropertyConfiguration</a>(*T*, *TProperty*)<br />Property configuration instance.

## See Also


#### Reference
<a href="4EE6CF69">XmlSchemaConfiguration Class</a><br /><a href="9052B9D6">Pelorus.Core.Xml.Serialization Namespace</a><br />