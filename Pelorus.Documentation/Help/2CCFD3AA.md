# Enums.Parse(*TEnum*) Method (String, Boolean)
 

Parses a string for the name of an enum value.

**Namespace:**&nbsp;<a href="CB7C5302">Pelorus.Core</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.1 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public static TEnum Parse<TEnum>(
	string enumName,
	bool ignoreCase
)
where TEnum : struct, new()

```

**VB**<br />
``` VB
Public Shared Function Parse(Of TEnum As {Structure, New}) ( 
	enumName As String,
	ignoreCase As Boolean
) As TEnum
```

**C++**<br />
``` C++
public:
generic<typename TEnum>
where TEnum : value class, gcnew()
static TEnum Parse(
	String^ enumName, 
	bool ignoreCase
)
```


#### Parameters
&nbsp;<dl><dt>enumName</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />String to parse for an enum value.</dd><dt>ignoreCase</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">System.Boolean</a><br />Indicates if the parse operation should be case sensitive.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEnum</dt><dd>Type of the enum that is represented by the string.</dd></dl>

#### Return Value
Type: *TEnum*<br />Enum value represented by the string or default(TEnum) if the enum value could not be parsed from the string.

## See Also


#### Reference
<a href="6ECBE43B">Enums Class</a><br /><a href="490F02E7">Parse Overload</a><br /><a href="CB7C5302">Pelorus.Core Namespace</a><br />