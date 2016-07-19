# IBaseRepository.DeleteById(*TEntity*, *TKey*) Method 
 

Delete an entity from the data store by Id.

**Namespace:**&nbsp;<a href="E27DB326">Pelorus.Core.Data</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
bool DeleteById<TEntity, TKey>(
	TKey entityId
)
where TEntity : Entity<TKey>
where TKey : struct, new()

```

**VB**<br />
``` VB
Function DeleteById(Of TEntity As Entity(Of TKey), TKey As {Structure, New}) ( 
	entityId As TKey
) As Boolean
```

**C++**<br />
``` C++
generic<typename TEntity, typename TKey>
where TEntity : Entity<TKey>
where TKey : value class, gcnew()
bool DeleteById(
	TKey entityId
)
```


#### Parameters
&nbsp;<dl><dt>entityId</dt><dd>Type: *TKey*<br />Id of the entity to delete from the data store.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity being deleted.</dd><dt>TKey</dt><dd>Type of the key of the entity being deleted.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">Boolean</a><br />True if the entity was found and deleted.

## See Also


#### Reference
<a href="30329654">IBaseRepository Interface</a><br /><a href="E27DB326">Pelorus.Core.Data Namespace</a><br />