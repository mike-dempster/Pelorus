# BaseReadOnlyRepository.PreProcessQuery(*TEntity*) Method 
 

Include children entities in the query using the IncludeExpression.

**Namespace:**&nbsp;<a href="55312241">Pelorus.Data.EntityFramework</a><br />**Assembly:**&nbsp;Pelorus.Data.EntityFramework (in Pelorus.Data.EntityFramework.dll) Version: 1.0.1.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
protected IQueryable<TEntity> PreProcessQuery<TEntity>(
	IQueryable<TEntity> query
)
where TEntity : class

```

**VB**<br />
``` VB
Protected Function PreProcessQuery(Of TEntity As Class) ( 
	query As IQueryable(Of TEntity)
) As IQueryable(Of TEntity)
```

**C++**<br />
``` C++
protected:
generic<typename TEntity>
where TEntity : ref class
IQueryable<TEntity>^ PreProcessQuery(
	IQueryable<TEntity>^ query
)
```


#### Parameters
&nbsp;<dl><dt>query</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/bb351562" target="_blank">System.Linq.IQueryable</a>(*TEntity*)<br />Query to include the child items with.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>\[Missing <typeparam name="TEntity"/> documentation for "M:Pelorus.Data.EntityFramework.BaseReadOnlyRepository.PreProcessQuery``1(System.Linq.IQueryable{``0})"\]</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/bb351562" target="_blank">IQueryable</a>(*TEntity*)<br />Query including the child entities.

## See Also


#### Reference
<a href="7A83640C">BaseReadOnlyRepository Class</a><br /><a href="55312241">Pelorus.Data.EntityFramework Namespace</a><br />