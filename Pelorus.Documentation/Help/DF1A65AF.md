# BaseRepository.ExecuteSqlCommand Method 
 

Execute a SQL command in the database.

**Namespace:**&nbsp;<a href="55312241">Pelorus.Data.EntityFramework</a><br />**Assembly:**&nbsp;Pelorus.Data.EntityFramework (in Pelorus.Data.EntityFramework.dll) Version: 1.0.1.0 (1.0.0.0)

## Syntax

**C#**<br />
``` C#
protected int ExecuteSqlCommand(
	string sqlQuery,
	params Object[] args
)
```

**VB**<br />
``` VB
Protected Function ExecuteSqlCommand ( 
	sqlQuery As String,
	ParamArray args As Object()
) As Integer
```

**C++**<br />
``` C++
protected:
int ExecuteSqlCommand(
	String^ sqlQuery, 
	... array<Object^>^ args
)
```


#### Parameters
&nbsp;<dl><dt>sqlQuery</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/s1wwdcbf" target="_blank">System.String</a><br />SQL command to execute.</dd><dt>args</dt><dd>Type: <a href="http://msdn2.microsoft.com/en-us/library/e5kfa45b" target="_blank">System.Object</a>[]<br />Arguments for the SQL command.</dd></dl>

#### Return Value
Type: <a href="http://msdn2.microsoft.com/en-us/library/td2s409d" target="_blank">Int32</a><br />Number of rows that were affected by the SQL command.

## See Also


#### Reference
<a href="D8FCD057">BaseRepository Class</a><br /><a href="55312241">Pelorus.Data.EntityFramework Namespace</a><br />