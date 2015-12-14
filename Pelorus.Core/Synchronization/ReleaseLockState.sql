-- TODO: For some reason if I rollback the transaction from ADO.NET it does not release the lock. This needs to be explained before the code can be 
-- used in a production environment
ROLLBACK TRANSACTION;
-- See notes in EnterLockState.sql for details on why we do this with the transactions.
BEGIN TRANSACTION;
