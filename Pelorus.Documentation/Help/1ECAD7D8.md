# MutualExclusion.ExclusionOwned Method 
 

Checks if ownership of the lock is already owned by the current thread.

**Namespace:**&nbsp;<a href="3DF715C2">Pelorus.Core.Synchronization</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.1 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
protected bool ExclusionOwned()
```

**VB**<br />
``` VB
Protected Function ExclusionOwned As Boolean
```

**C++**<br />
``` C++
protected:
bool ExclusionOwned()
```


#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/a28wyd50" target="_blank">Boolean</a><br />true if the thread already owns the exclusive lock otherwise false.

## See Also


#### Reference
<a href="516E972A">MutualExclusion Class</a><br /><a href="3DF715C2">Pelorus.Core.Synchronization Namespace</a><br />