# BaseRepository.ExecuteStoredProcedureAsync(*TEntity*) Method (String, CancellationToken, IDictionary(String, Object))
 

Executes a stored procedure and maps the output to type TEntity asynchronously.

**Namespace:**&nbsp;<a href="55312241">Pelorus.Data.EntityFramework</a><br />**Assembly:**&nbsp;Pelorus.Data.EntityFramework (in Pelorus.Data.EntityFramework.dll) Version: 1.0.1.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public Task<IEnumerable<TEntity>> ExecuteStoredProcedureAsync<TEntity>(
	string storedProcedureName,
	CancellationToken cancellationToken,
	IDictionary<string, Object> args
)
where TEntity : class

```

**VB**<br />
``` VB
Public Function ExecuteStoredProcedureAsync(Of TEntity As Class) ( 
	storedProcedureName As String,
	cancellationToken As CancellationToken,
	args As IDictionary(Of String, Object)
) As Task(Of IEnumerable(Of TEntity))
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
where TEntity : ref class
Task<IEnumerable<TEntity>^>^ ExecuteStoredProcedureAsync(
	String^ storedProcedureName, 
	CancellationToken cancellationToken, 
	IDictionary<String^, Object^>^ args
)
```


#### Parameters
&nbsp;<dl><dt>storedProcedureName</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />Name of the stored procedure to execute.</dd><dt>cancellationToken</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/dd384802" target="_blank">System.Threading.CancellationToken</a><br />Cancellation token for the asynchronous operation.</dd><dt>args</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s4ys34ea" target="_blank">System.Collections.Generic.IDictionary</a>(<a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">String</a>, <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">Object</a>)<br />Arguments to pass to the stored procedure.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>\[Missing <typeparam name="TEntity"/> documentation for "M:Pelorus.Data.EntityFramework.BaseRepository.ExecuteStoredProcedureAsync``1(System.String,System.Threading.CancellationToken,System.Collections.Generic.IDictionary{System.String,System.Object})"\]</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/dd321424" target="_blank">Task</a>(<a href="http://msdn2.microsoft.com/en-us/library/9eekhta0" target="_blank">IEnumerable</a>(*TEntity*))<br />Results of the stored procedure.

## See Also


#### Reference
<a href="D8FCD057">BaseRepository Class</a><br /><a href="902C3CFE">ExecuteStoredProcedureAsync Overload</a><br /><a href="55312241">Pelorus.Data.EntityFramework Namespace</a><br />