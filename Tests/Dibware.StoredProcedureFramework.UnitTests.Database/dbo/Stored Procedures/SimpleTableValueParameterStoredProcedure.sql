CREATE PROCEDURE [dbo].[SimpleTableValueParameterStoredProcedure]
(
    @TvpParameters [dbo].[SimpleTableValueParameterTableType] READONLY
)
AS
BEGIN
    DECLARE @A INT;
    
END