
CREATE PROCEDURE [dbo].[TableValueParameterWithoutReturnTypeStoredProcedure]
(
    @TvpParameters [dbo].[SimpleTableValueParameterTableType] READONLY
)
AS
BEGIN
    DECLARE @A INT;
    
END