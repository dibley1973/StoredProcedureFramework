CREATE PROCEDURE TransactionTestCountAllStoredProcedure
AS
BEGIN
    SELECT COUNT (*) [Count]
    FROM    [dbo].[TransactionTest];
END