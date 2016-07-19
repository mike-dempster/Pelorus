# IBaseReadOnlyRepository.GetAsync(*TEntity*) Method 
 

Returns an entity based on a search predicate asynchronously. The predicate must only select a single entity.

**Namespace:**&nbsp;<a href="E27DB326">Pelorus.Core.Data</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
Task<TEntity> GetAsync<TEntity>(
	Expression<Func<TEntity, bool>> predicate,
	CancellationToken cancellationToken
)
where TEntity : class

```

**VB**<br />
``` VB
Function GetAsync(Of TEntity As Class) ( 
	predicate As Expression(Of Func(Of TEntity, Boolean)),
	cancellationToken As CancellationToken
) As Task(Of TEntity)
```

**C++**<br />
``` C++
generic<typename TEntity>
where TEntity : ref class
Task<TEntity>^ GetAsync(
	Expression<Func<TEntity, bool>^>^ predicate, 
	CancellationToken cancellationToken
)
```


#### Parameters
&nbsp;<dl><dt>predicate</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/bb335710" target="_blank">System.Linq.Expressions.Expression</a>(<a href="http://msdn2.microsoft.com/en-us/library/bb549151" target="_blank">Func</a>(*TEntity*, <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">Boolean</a>))<br />Search predicate for the operations.</dd><dt>cancellationToken</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/dd384802" target="_blank">System.Threading.CancellationToken</a><br />Cancellation token for the asynchronous operation.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity to query.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/dd321424" target="_blank">Task</a>(*TEntity*)<br />The entity that satisfies the search predicate's criteria or null if the entity does not exist.

## See Also


#### Reference
<a href="E4B31551">IBaseReadOnlyRepository Interface</a><br /><a href="E27DB326">Pelorus.Core.Data Namespace</a><br />