# LocalizeProxy.CreateTransparentProxy(*T*) Method 
 

Create a new instance of the localize proxy and return a transparent proxy to the given instance.

**Namespace:**&nbsp;<a href="99F211A">Pelorus.Core.Localization</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public static T CreateTransparentProxy<T>(
	T instance,
	TimeZoneInfo timeZone
)

```

**VB**<br />
``` VB
Public Shared Function CreateTransparentProxy(Of T) ( 
	instance As T,
	timeZone As TimeZoneInfo
) As T
```

**C++**<br />
``` C++
public:
generic<typename T>
static T CreateTransparentProxy(
	T instance, 
	TimeZoneInfo^ timeZone
)
```


#### Parameters
&nbsp;<dl><dt>instance</dt><dd>Type: *T*<br />Instance of the object to create a proxy for.</dd><dt>timeZone</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/bb396389" target="_blank">System.TimeZoneInfo</a><br />Time zone to use for localizing the DateTime values.</dd></dl>

#### Type Parameters
&nbsp;<dl><dt>T</dt><dd>Type of the target instance that the proxy will use.</dd></dl>

#### Return Value
Type: *T*<br />Transparent localize proxy.

## See Also


#### Reference
<a href="C3FA92A5">LocalizeProxy Class</a><br /><a href="99F211A">Pelorus.Core.Localization Namespace</a><br />