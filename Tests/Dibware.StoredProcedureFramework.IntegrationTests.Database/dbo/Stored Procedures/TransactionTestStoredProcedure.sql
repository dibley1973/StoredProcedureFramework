CREATE PROCEDURE [dbo].[TransactionTestStoredProcedure]
(
    @TvpParameters [dbo].[SimpleTableValueParameterTableType] READONLY
)
AS
BEGIN
    MERGE INTO [dbo].[TransactionTest] AS [target]
    USING   @TvpParameters AS [source]
    ON      [target].[Id] = [source].[Id]
    AND     [target].[Name] = [source].[Name]
    WHEN MATCHED THEN UPDATE SET 
        [target].[IsActive] = [source].[IsActive]
    WHEN NOT MATCHED THEN INSERT VALUES
    (
        [source].[Id]
    ,   [source].[IsActive]
    ,   [source].[Name]
    );
    
END