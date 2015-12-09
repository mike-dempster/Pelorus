-- To get the desired behavior from the SQL database, the global temp table must be created outside of any transaction. However, ADO.NET requires that the 
-- transaction count is the same before and after the SQL statement is executed or it will throw an exception. To get around this a transaction is initiated 
-- from the application, that transaction is rolled back in the SQL statement, the global temp table is created with the unique index, a new transaction is 
-- initiazted in the SQL statement, and a record is inserted with the name of the mutual exclusion to create. This ensures that ADO.NET's transaction count 
-- requirement is met and that SQL will only block when trying to insert the name that is being inserted inside another transaction but NOT for a different 
-- name.
ROLLBACK TRANSACTION;

IF OBJECT_ID('tempdb..##tblPelorusMutualExclusionCoordinator2710546D7E744286A08AD70A221B6633') IS NULL
BEGIN
	CREATE TABLE ##tblPelorusMutualExclusionCoordinator2710546D7E744286A08AD70A221B6633 (
		[Name] [varchar](50) NOT NULL,
		CONSTRAINT [AK_LockName] UNIQUE NONCLUSTERED ([Name] ASC)
		WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END;

BEGIN TRANSACTION
INSERT INTO ##tblPelorusMutualExclusionCoordinator2710546D7E744286A08AD70A221B6633 ([Name]) VALUES (@globallyUniqueName);
