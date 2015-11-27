CREATE PROCEDURE [dbo].[TransactionTestDeleteAllStoredProcedure]
AS
BEGIN
    DELETE FROM
    [dbo].[TransactionTest];
END