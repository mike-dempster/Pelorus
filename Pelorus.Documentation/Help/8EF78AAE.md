# BaseReadOnlyRepository.GetAll(*TEntity*) Method 
 

Get a collection of entities

**Namespace:**&nbsp;<a href="55312241">Pelorus.Data.EntityFramework</a><br />**Assembly:**&nbsp;Pelorus.Data.EntityFramework (in Pelorus.Data.EntityFramework.dll) Version: 1.0.1.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public virtual IEnumerable<TEntity> GetAll<TEntity>()
where TEntity : class

```

**VB**<br />
``` VB
Public Overridable Function GetAll(Of TEntity As Class) As IEnumerable(Of TEntity)
```

**C++**<br />
``` C++
public:
generic<typename TEntity>
where TEntity : ref class
virtual IEnumerable<TEntity>^ GetAll()
```


#### Type Parameters
&nbsp;<dl><dt>TEntity</dt><dd>Type of the entity to query.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/9eekhta0" target="_blank">IEnumerable</a>(*TEntity*)<br />Collection of entities.

#### Implements
<a href="7E6707CF">IBaseReadOnlyRepository.GetAll(TEntity)()</a><br />

## See Also


#### Reference
<a href="7A83640C">BaseReadOnlyRepository Class</a><br /><a href="A334F638">GetAll Overload</a><br /><a href="55312241">Pelorus.Data.EntityFramework Namespace</a><br />