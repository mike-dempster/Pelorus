# ExceptionLoggingHttpModule.Init Method 
 

Initialize the HTTP module to respond to unhandled exceptions.

**Namespace:**&nbsp;<a href="F7316212">Pelorus.Web.ExceptionLogging</a><br />**Assembly:**&nbsp;Pelorus.Web (in Pelorus.Web.dll) Version: 1.0.0.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public void Init(
	HttpApplication context
)
```

**VB**<br />
``` VB
Public Sub Init ( 
	context As HttpApplication
)
```

**C++**<br />
``` C++
public:
virtual void Init(
	HttpApplication^ context
) sealed
```


#### Parameters
&nbsp;<dl><dt>context</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/4wt3wttw" target="_blank">System.Web.HttpApplication</a><br />An HttpApplication that provides access to the methods, properties, and events common to all application objects within an ASP.NET application.</dd></dl>

#### Implements
<a href="http://msdn2.microsoft.com/en-us/library/4fxtd0dz" target="_blank">IHttpModule.Init(HttpApplication)</a><br />

## See Also


#### Reference
<a href="4B299149">ExceptionLoggingHttpModule Class</a><br /><a href="F7316212">Pelorus.Web.ExceptionLogging Namespace</a><br />