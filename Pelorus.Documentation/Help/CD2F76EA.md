# XmlSerializerContext.Serialize Method (Stream, Object, XmlSerializerNamespaces)
 

Serializes an object and outputs the XML to the given stream.

**Namespace:**&nbsp;<a href="9052B9D6">Pelorus.Core.Xml.Serialization</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void Serialize(
	Stream stream,
	Object o,
	XmlSerializerNamespaces namespaces
)
```

**VB**<br />
``` VB
Public Sub Serialize ( 
	stream As Stream,
	o As Object,
	namespaces As XmlSerializerNamespaces
)
```

**C++**<br />
``` C++
public:
void Serialize(
	Stream^ stream, 
	Object^ o, 
	XmlSerializerNamespaces^ namespaces
)
```


#### Parameters
&nbsp;<dl><dt>stream</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/8f86tw9e" target="_blank">System.IO.Stream</a><br />Stream to output the serialized object graph to.</dd><dt>o</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">System.Object</a><br />Object to serialize.</dd><dt>namespaces</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/8e2fsfb7" target="_blank">System.Xml.Serialization.XmlSerializerNamespaces</a><br />Namespaces to use for the serialize process.</dd></dl>

## See Also


#### Reference
<a href="859B939D">XmlSerializerContext Class</a><br /><a href="A1B2A50E">Serialize Overload</a><br /><a href="9052B9D6">Pelorus.Core.Xml.Serialization Namespace</a><br />