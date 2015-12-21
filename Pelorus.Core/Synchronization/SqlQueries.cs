namespace Pelorus.Core.Synchronization
{
	internal class SqlQueries
	{
        public const string EnterLockState = "DECLARE @sqlStmt AS NVARCHAR(MAX) = CONCAT('USE tempdb; EXEC sp_getapplock @Resource = ''', @globallyUniqueName, ''', @LockMode = ''Exclusive'', @LockOwner = ''Transaction'', @DbPrincipal = ''dbo'';'); EXECUTE (@sqlStmt)";
        public const string ReleaseLockState = "DECLARE @sqlStmt AS NVARCHAR(MAX) = CONCAT('USE tempdb; EXEC sp_releaseapplock @Resource =''', @globallyUniqueName, ''', @LockOwner = ''Transaction'', @DbPrincipal = ''dbo'';'); EXECUTE (@sqlStmt)";
	}
}
