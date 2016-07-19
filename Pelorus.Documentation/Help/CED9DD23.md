# BaseReadOnlyRepository.GetCountAsync(*TEntity*) Method (Int32, Int32, CancellationToken)
 

Get a collection of 'length' entities starting at the position of 'startIndex' asynchronously.

**Namespace:**&nbsp;<a href="55312241">Pelorus.Data.EntityFramework</a><br />**Assembly:**&nbsp;Pelorus.Data.EntityFramework (in Pelorus.Data.EntityFramework.dll) Version: 1.0.1.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public virtual Task<IEnumerable<TEntity>> GetCountAsync<TEntity>(
	int startIndex,
	int length,
	CancellationToken cancellationToken
)
where TEntity : class

```

**VB**<br />
``` VB
Public Overridable Function GetCountAsync(Of TEntity As Class) ( 
	startIndex As Integer,
	length As Integer,
	cancellationToken As CancellationToken
) As Task(Of IEnumerable(Of TEntity))
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
where TEntity : ref class
virtual Task<IEnumerable<TEntity>^>^ GetCountAsync(
	int startIndex, 
	int length, 
	CancellationToken cancellationToken
)
```


#### Parameters
&nbsp;<dl><dt>startIndex</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/td2s409d" target="_blank">System.Int32</a><br />Position in the full collection to select from.</dd><dt>length</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/td2s409d" target="_blank">System.Int32</a><br />Number of items to return.</dd><dt>cancellationToken</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/dd384802" target="_blank">System.Threading.CancellationToken</a><br />Cancellation token for the asynchronous operation.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity to query.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/dd321424" target="_blank">Task</a>(<a href="http://msdn2.microsoft.com/en-us/library/9eekhta0" target="_blank">IEnumerable</a>(*TEntity*))<br />Collection of entities.

#### Implements
<a href="4D3D93F7">IBaseReadOnlyRepository.GetCountAsync(TEntity)(Int32, Int32, CancellationToken)</a><br />

## See Also


#### Reference
<a href="7A83640C">BaseReadOnlyRepository Class</a><br /><a href="4A807ED0">GetCountAsync Overload</a><br /><a href="55312241">Pelorus.Data.EntityFramework Namespace</a><br />