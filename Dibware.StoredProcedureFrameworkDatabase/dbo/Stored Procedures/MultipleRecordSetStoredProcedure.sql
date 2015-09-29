CREATE PROCEDURE [dbo].[MultipleRecordSetStoredProcedure]
    @Id                 INT
,   @Name               VARCHAR(20)
,   @Active             BIT
,   @Price              DECIMAL(10, 4)
,   @UniqueIdentifier   UNIQUEIDENTIFIER
,   @Count              TINYINT
AS
BEGIN
    /* First Record Set */
    SELECT 
        @Id     AS Id
    ,   @Name   AS Name
    UNION
    SELECT
        17      AS Id
    ,   'Bill'  AS Name;

    /* Second Record Set */
    SELECT 
        @Active as Active
    ,   @Price  AS Price
    --UNION
    --SELECT
    --    CAST(0 AS BIT)                  AS Active
    --,   CAST(99.99 AS DECIMAL(10,4))    AS Price

    /* Third Record Set */
    SELECT
        @UniqueIdentifier   AS [UniqueIdentifier]
    ,   @Count              AS [Count]
    
END