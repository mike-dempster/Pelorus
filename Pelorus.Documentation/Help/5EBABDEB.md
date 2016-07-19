# ContextFactory(*TContext*).Create Method (Boolean)
 

Creates a data context.

**Namespace:**&nbsp;<a href="55312241">Pelorus.Data.EntityFramework</a><br />**Assembly:**&nbsp;Pelorus.Data.EntityFramework (in Pelorus.Data.EntityFramework.dll) Version: 1.0.1.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public DbContext Create(
	bool createNew
)
```

**VB**<br />
``` VB
Public Function Create ( 
	createNew As Boolean
) As DbContext
```

**C++**<br />
``` C++
public:
virtual DbContext^ Create(
	bool createNew
) sealed
```


#### Parameters
&nbsp;<dl><dt>createNew</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">System.Boolean</a><br />true if a new context should be created; otherwise false to reuse a previously created context instance.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/gg679505" target="_blank">DbContext</a><br />Entity Framework data context.

#### Implements
<a href="31674AF1">IContextFactory.Create(Boolean)</a><br />

## See Also


#### Reference
<a href="EC90D80">ContextFactory(TContext) Class</a><br /><a href="2495EF78">Create Overload</a><br /><a href="55312241">Pelorus.Data.EntityFramework Namespace</a><br />