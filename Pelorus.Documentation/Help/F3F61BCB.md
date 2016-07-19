# Types.Cast(*T*) Method (Object)
 

Convert the given object to type 'T'. If the object cannot be cast to type T then return the default value of T.

**Namespace:**&nbsp;<a href="CB7C5302">Pelorus.Core</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public static T Cast<T>(
	Object subject
)

```

**VB**<br />
``` VB
Public Shared Function Cast(Of T) ( 
	subject As Object
) As T
```

**C++**<br />
``` C++
public:
generic<typename T>
static T Cast(
	Object^ subject
)
```


#### Parameters
&nbsp;<dl><dt>subject</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">System.Object</a><br />Object to cast.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>Type to cast object to.</dd></dl>

#### Return Value
Type: *T*<br />Subject cast to type T or the default value ot type T if a converter does not exist.

## See Also


#### Reference
<a href="4DD83F54">Types Class</a><br /><a href="3A5C8A5C">Cast Overload</a><br /><a href="CB7C5302">Pelorus.Core Namespace</a><br />