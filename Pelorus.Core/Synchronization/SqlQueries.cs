﻿namespace Pelorus.Core.Synchronization
{
	internal class SqlQueries
	{
        public const string EnterLockState = "IF OBJECT_ID('tempdb..##tblPelorusMutualExclusionCoordinator2710546D7E744286A08AD70A221B6633') IS NULL BEGIN CREATE TABLE ##tblPelorusMutualExclusionCoordinator2710546D7E744286A08AD70A221B6633 ( [Name] [varchar](50) NOT NULL, CONSTRAINT [AK_LockName] UNIQUE NONCLUSTERED ([Name] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] END; BEGIN TRY INSERT INTO ##tblPelorusMutualExclusionCoordinator2710546D7E744286A08AD70A221B6633 ([Name]) VALUES (@globallyUniqueName) END TRY BEGIN CATCH IF ERROR_NUMBER() <> 2627 THROW END CATCH;";
        public const string ReleaseLockState = "DELETE FROM ##tblPelorusMutualExclusionCoordinator2710546D7E744286A08AD70A221B6633 WHERE [Name] = @globallyUniqueName";
	}
}
