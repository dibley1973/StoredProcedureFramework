CREATE PROCEDURE [dbo].[NormalStoredProcedureForEF]
    @Id  INT
AS
BEGIN
    SELECT 
        @Id AS Id
    ,   'Dave' AS Name
    ,   CAST(1 AS BIT) AS Active
END