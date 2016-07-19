# IBaseRepository.Delete(*TEntity*) Method 
 

Delete an entity from the data store.

**Namespace:**&nbsp;<a href="E27DB326">Pelorus.Core.Data</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
TEntity Delete<TEntity>(
	TEntity entity
)
where TEntity : class

```

**VB**<br />
``` VB
Function Delete(Of TEntity As Class) ( 
	entity As TEntity
) As TEntity
```

**C++**<br />
``` C++
generic<typename TEntity>
where TEntity : ref class
TEntity Delete(
	TEntity entity
)
```


#### Parameters
&nbsp;<dl><dt>entity</dt><dd>Type: *TEntity*<br />Entity to delete from the data store.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity being deleted.</dd></dl>

#### Return Value
Type: *TEntity*<br />Deleted entity.

## See Also


#### Reference
<a href="30329654">IBaseRepository Interface</a><br /><a href="E27DB326">Pelorus.Core.Data Namespace</a><br />