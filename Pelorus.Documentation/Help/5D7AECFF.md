# DbContextExtensions.GetSchemaAndTableName(*TEntity*) Method 
 

Gets the schema and name the table that that TEntity is mapped to in the data context.

**Namespace:**&nbsp;<a href="55312241">Pelorus.Data.EntityFramework</a><br />**Assembly:**&nbsp;Pelorus.Data.EntityFramework (in Pelorus.Data.EntityFramework.dll) Version: 1.0.1.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public static string GetSchemaAndTableName<TEntity>(
	this DbContext context
)
where TEntity : class

```

**VB**<br />
``` VB
<ExtensionAttribute>
Public Shared Function GetSchemaAndTableName(Of TEntity As Class) ( 
	context As DbContext
) As String
```

**C++**<br />
``` C++
public:
[ExtensionAttribute]
generic<typename TEntity>
where TEntity : ref class
static String^ GetSchemaAndTableName(
	DbContext^ context
)
```


#### Parameters
&nbsp;<dl><dt>context</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/gg679505" target="_blank">DbContext</a><br />Data context with the entity mapping.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Entity type to get the schema and table name for.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">String</a><br />Schema and table name that TEntity is mapped to.

#### Usage Note
In Visual Basic and C#, you can call this method as an instance method on any object of type <a href="http://msdn2.microsoft.com/en-us/library/gg679505" target="_blank">DbContext</a>. When you use instance method syntax to call this method, omit the first parameter. For more information, see <a href="http://msdn.microsoft.com/en-us/library/bb384936.aspx">Extension Methods (Visual Basic)</a> or <a href="http://msdn.microsoft.com/en-us/library/bb383977.aspx">Extension Methods (C# Programming Guide)</a>.

## See Also


#### Reference
<a href="7F5D0833">DbContextExtensions Class</a><br /><a href="55312241">Pelorus.Data.EntityFramework Namespace</a><br />