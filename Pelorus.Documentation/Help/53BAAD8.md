# DataRecordExtensions.GetBytes Method 
 

Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the given buffer offset.

**Namespace:**&nbsp;<a href="E27DB326">Pelorus.Core.Data</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public static long GetBytes(
	this IDataRecord reader,
	string name,
	long fieldOffset,
	byte[] buffer,
	int bufferoffset,
	int length
)
```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function GetBytes ( 
	reader As IDataRecord,
	name As String,
	fieldOffset As Long,
	buffer As Byte(),
	bufferoffset As Integer,
	length As Integer
) As Long
```

**C++**<br />
``` C++
public:
[ExtensionAttribute]
static long long GetBytes(
	IDataRecord^ reader, 
	String^ name, 
	long long fieldOffset, 
	array<unsigned char>^ buffer, 
	int bufferoffset, 
	int length
)
```


#### Parameters
&nbsp;<dl><dt>reader</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/93wb1heh" target="_blank">System.Data.IDataRecord</a><br />Reader to get the bytes from.</dd><dt>name</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Name of the column to get the bytes from.</dd><dt>fieldOffset</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/6yy583ek" target="_blank">System.Int64</a><br />The index within the field from which to start the read operation.</dd><dt>buffer</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/yyb1w04y" target="_blank">System.Byte</a>[]<br />The buffer into which to read the stream of bytes.</dd><dt>bufferoffset</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/td2s409d" target="_blank">System.Int32</a><br />The index for buffer to start the read operation.</dd><dt>length</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/td2s409d" target="_blank">System.Int32</a><br />The number of bytes to read.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/6yy583ek" target="_blank">Int64</a><br />The actual number of bytes read.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type <a href="http://msdn2.microsoft.com/en-us/library/93wb1heh" target="_blank">IDataRecord</a>. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="412D3D25">DataRecordExtensions Class</a><br /><a href="E27DB326">Pelorus.Core.Data Namespace</a><br />