CREATE PROCEDURE [dbo].[DtoColumnAliasTestProcedure]
AS
BEGIN
    SELECT 
        100 AS Id
    ,   'Dave' AS Name
    ,   CAST(1 AS BIT) AS Active
END