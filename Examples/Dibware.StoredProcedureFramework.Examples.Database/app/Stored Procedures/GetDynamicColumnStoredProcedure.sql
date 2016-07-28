CREATE PROCEDURE [app].[GetDynamicColumnStoredProcedure]
AS
BEGIN
	SELECT  'Dave'      [Firstname],
            'Smith'     [Surname],
            32          [Age],
            GETDATE()   [DateOfBirth]
    UNION

    SELECT  'Peter'     [Firstname],
            'Pan'       [Surname],
            134         [Age],
            GETDATE()   [DateOfBirth];
END