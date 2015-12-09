namespace Pelorus.Core.Synchronization
{
	internal class SqlQueries
	{
        public const string EnterLockState = "ROLLBACK TRANSACTION; IF OBJECT_ID('tempdb..##tblPelorusMutualExclusionCoordinator2710546D7E744286A08AD70A221B6633') IS NULL BEGIN CREATE TABLE ##tblPelorusMutualExclusionCoordinator2710546D7E744286A08AD70A221B6633 ( [Name] [varchar](50) NOT NULL, CONSTRAINT [AK_LockName] UNIQUE NONCLUSTERED ([Name] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = OFF) ON [PRIMARY] ) ON [PRIMARY] END; BEGIN TRANSACTION INSERT INTO ##tblPelorusMutualExclusionCoordinator2710546D7E744286A08AD70A221B6633 ([Name]) VALUES (@globallyUniqueName);";
	}
}
