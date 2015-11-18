
CREATE PROCEDURE [dbo].[TableValueParameterWithReturnTypeStoredProcedure]
(
    @TvpParameters [dbo].[SimpleTableValueParameterTableType] READONLY
)
AS
BEGIN
    SELECT 
        Id
    ,   IsActive
    ,   Name
    FROM
        @TvpParameters
END