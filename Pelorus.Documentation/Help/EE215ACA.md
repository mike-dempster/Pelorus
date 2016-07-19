# LocalizeProxy.Invoke Method 
 

Invoke a method on the target instance.

**Namespace:**&nbsp;<a href="99F211A">Pelorus.Core.Localization</a><br />**Assembly:**&nbsp;Pelorus.Core (in Pelorus.Core.dll) Version: 1.0.0.1 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
public override IMessage Invoke(
	IMessage msg
)
```

**VB**<br />
``` VB
Public Overrides Function Invoke ( 
	msg As IMessage
) As IMessage
```

**C++**<br />
``` C++
public:
virtual IMessage^ Invoke(
	IMessage^ msg
) override
```


#### Parameters
&nbsp;<dl><dt>msg</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/5c8cd72b" target="_blank">System.Runtime.Remoting.Messaging.IMessage</a><br />Method call message.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/5c8cd72b" target="_blank">IMessage</a><br />Return message with the data returned from the method.

## See Also


#### Reference
<a href="C3FA92A5">LocalizeProxy Class</a><br /><a href="99F211A">Pelorus.Core.Localization Namespace</a><br />