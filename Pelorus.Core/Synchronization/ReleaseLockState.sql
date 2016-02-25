DECLARE @SQL NVARCHAR(MAX) = (SELECT 'USE tempdb; EXEC sp_releaseapplock @Resource =''' + @globallyUniqueName + ''', @LockOwner = ''Transaction'', @DbPrincipal = ''dbo'';');
EXEC(@SQL);
