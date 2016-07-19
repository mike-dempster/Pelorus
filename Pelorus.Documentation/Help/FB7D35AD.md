# Types.Cast Method (Object, Type)
 

Convert the given object to type targetType. If the object cannot be cast to type targetType then return null.

**Namespace:**&nbsp;<a href="CB7C5302">Pelorus.Core</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public static Object Cast(
	Object subject,
	Type targetType
)
```

**VB**<br />
``` VB
Public Shared Function Cast ( 
	subject As Object,
	targetType As Type
) As Object
```

**C++**<br />
``` C++
public:
static Object^ Cast(
	Object^ subject, 
	Type^ targetType
)
```


#### Parameters
&nbsp;<dl><dt>subject</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">System.Object</a><br />Object to cast.</dd><dt>targetType</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/42892f65" target="_blank">System.Type</a><br />Type to convert the given object to.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a><br />Subject cast to type targetType or null if a converter does not exist.

## See Also


#### Reference
<a href="4DD83F54">Types Class</a><br /><a href="3A5C8A5C">Cast Overload</a><br /><a href="CB7C5302">Pelorus.Core Namespace</a><br />