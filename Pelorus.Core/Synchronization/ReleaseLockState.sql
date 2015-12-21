DECLARE @sqlStmt AS NVARCHAR(MAX) = CONCAT('USE tempdb; EXEC sp_releaseapplock @Resource =''', @globallyUniqueName, ''', @LockOwner = ''Transaction'', @DbPrincipal = ''dbo'';');
EXECUTE (@sqlStmt)
