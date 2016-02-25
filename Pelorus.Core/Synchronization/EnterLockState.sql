DECLARE @SQL NVARCHAR(MAX) = (SELECT 'USE tempdb; EXEC sp_getapplock @Resource = ''' + @globallyUniqueName + ''', @LockMode = ''Exclusive'', @LockOwner = ''Transaction'', @DbPrincipal = ''dbo'';');
EXEC(@SQL);
