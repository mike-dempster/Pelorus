# IContainer.Register(*TContract*, *TClass*) Method 
 

Register an implementation of a contract type.

**Namespace:**&nbsp;<a href="D77506BC">Pelorus.Core.IoC</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.1 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
void Register<TContract, TClass>()
where TContract : class
where TClass : class, TContract

```

**VB**<br />
``` VB
Sub Register(Of TContract As Class, TClass As {Class, TContract})
```

**C++**<br />
``` C++
generic<typename TContract, typename TClass>
where TContract : ref class
where TClass : ref class, TContract
void Register()
```


#### Type Parameters
&nbsp;<dl><dt>TContract</dt><dd>Type of the contract being registered.</dd><dt>TClass</dt><dd>Type of the implementation being registered.</dd></dl>

## See Also


#### Reference
<a href="E534F261">IContainer Interface</a><br /><a href="1DE2BA86">Register Overload</a><br /><a href="D77506BC">Pelorus.Core.IoC Namespace</a><br />