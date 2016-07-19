# DataRecordExtensions.GetNullableGuid Method 
 

Gets the value of the specified column as a nullable Guid.

**Namespace:**&nbsp;<a href="E27DB326">Pelorus.Core.Data</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.1 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public static Nullable<Guid> GetNullableGuid(
	this IDataRecord reader,
	string name
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function GetNullableGuid ( 
	reader As IDataRecord,
	name As String
) As Nullable(Of Guid)
```

**C++**<br />
``` C++
public:
[ExtensionAttribute]
static Nullable<Guid> GetNullableGuid(
	IDataRecord^ reader, 
	String^ name
)
```


#### Parameters
&nbsp;<dl><dt>reader</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/93wb1heh" target="_blank">System.Data.IDataRecord</a><br />Reader to get the value from.</dd><dt>name</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Name of the column to get the value of.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/b3h38hb0" target="_blank">Nullable</a>(<a href="http://msdn2.microsoft.com/en-us/library/cey1zx63" target="_blank">Guid</a>)<br />Guid or null value of the specified column.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type <a href="http://msdn2.microsoft.com/en-us/library/93wb1heh" target="_blank">IDataRecord</a>. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="412D3D25">DataRecordExtensions Class</a><br /><a href="E27DB326">Pelorus.Core.Data Namespace</a><br />