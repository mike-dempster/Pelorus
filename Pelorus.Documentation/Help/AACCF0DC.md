# IBaseRepository.UpdateAsync(*TEntity*) Method (*TEntity*, CancellationToken)
 

Update an existing entity asynchronously.

**Namespace:**&nbsp;<a href="E27DB326">Pelorus.Core.Data</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.1 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
Task<TEntity> UpdateAsync<TEntity>(
	TEntity entity,
	CancellationToken cancellationToken
)
where TEntity : class

```

**VB**<br />
``` VB
Function UpdateAsync(Of TEntity As Class) ( 
	entity As TEntity,
	cancellationToken As CancellationToken
) As Task(Of TEntity)
```

**C++**<br />
``` C++
generic<typename TEntity>
where TEntity : ref class
Task<TEntity>^ UpdateAsync(
	TEntity entity, 
	CancellationToken cancellationToken
)
```


#### Parameters
&nbsp;<dl><dt>entity</dt><dd>Type: *TEntity*<br />Updated entity to update in the data store.</dd><dt>cancellationToken</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/dd384802" target="_blank">System.Threading.CancellationToken</a><br />Cancellation token for the asynchronous operation.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity being updated.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/dd321424" target="_blank">Task</a>(*TEntity*)<br />The updated entity.

## See Also


#### Reference
<a href="30329654">IBaseRepository Interface</a><br /><a href="2BCBC11C">UpdateAsync Overload</a><br /><a href="E27DB326">Pelorus.Core.Data Namespace</a><br />