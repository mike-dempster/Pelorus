# IBaseReadOnlyRepository.GetById(*TEntity*, *TKey*) Method 
 

Returns an entity by Id.

**Namespace:**&nbsp;<a href="E27DB326">Pelorus.Core.Data</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.1 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
TEntity GetById<TEntity, TKey>(
	TKey entityId
)
where TEntity : Entity<TKey>
where TKey : struct, new()

```

**VB**<br />
``` VB
Function GetById(Of TEntity As Entity(Of TKey), TKey As {Structure, New}) ( 
	entityId As TKey
) As TEntity
```

**C++**<br />
``` C++
generic<typename TEntity, typename TKey>
where TEntity : Entity<TKey>
where TKey : value class, gcnew()
TEntity GetById(
	TKey entityId
)
```


#### Parameters
&nbsp;<dl><dt>entityId</dt><dd>Type: *TKey*<br />Id of the entity to get.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity to query.</dd><dt>TKey</dt><dd>Type of the entity's key.</dd></dl>

#### Return Value
Type: *TEntity*<br />The entity with the given Id or null if the entity does not exist.

## See Also


#### Reference
<a href="E4B31551">IBaseReadOnlyRepository Interface</a><br /><a href="E27DB326">Pelorus.Core.Data Namespace</a><br />