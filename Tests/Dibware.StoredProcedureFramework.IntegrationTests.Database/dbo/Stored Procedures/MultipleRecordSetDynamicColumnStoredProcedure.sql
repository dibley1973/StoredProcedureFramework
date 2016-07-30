
CREATE PROCEDURE [dbo].[MultipleRecordSetDynamicColumnStoredProcedure]
AS
BEGIN
	/* First Record Set */
    SELECT  'Dave'      [Firstname],
            'Smith'     [Surname],
            32          [Age],
            GETDATE()   [DateOfBirth]
    UNION

    SELECT  'Peter'     [Firstname],
            'Pan'       [Surname],
            134         [Age],
            GETDATE()   [DateOfBirth];

    /* Second Record Set */
    SELECT 
        CAST(1 AS BIT)        AS [Active]
    ,   CAST(10.99 AS MONEY)  AS [Price];

    /* Third Record Set */
    SELECT
        NEWID()   AS [UniqueIdentifier]
    ,   1         AS [Count];
END