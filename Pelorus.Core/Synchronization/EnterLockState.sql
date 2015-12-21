DECLARE @sqlStmt AS NVARCHAR(MAX) = CONCAT('USE tempdb; EXEC sp_getapplock @Resource = ''', @globallyUniqueName, ''', @LockMode = ''Exclusive'', @LockOwner = ''Transaction'', @DbPrincipal = ''dbo'';');
EXECUTE (@sqlStmt)
