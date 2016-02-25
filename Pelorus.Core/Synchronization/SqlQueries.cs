namespace Pelorus.Core.Synchronization
{
	internal class SqlQueries
	{
        public const string EnterLockState = "DECLARE @SQL NVARCHAR(MAX) = (SELECT 'USE tempdb; EXEC sp_getapplock @Resource = ''' + @globallyUniqueName + ''', @LockMode = ''Exclusive'', @LockOwner = ''Transaction'', @DbPrincipal = ''dbo'';'); EXEC(@SQL);";
        public const string ReleaseLockState = "DECLARE @SQL NVARCHAR(MAX) = (SELECT 'USE tempdb; EXEC sp_releaseapplock @Resource =''' + @globallyUniqueName + ''', @LockOwner = ''Transaction'', @DbPrincipal = ''dbo'';'); EXEC(@SQL);";
	}
}
