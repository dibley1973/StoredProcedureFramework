
CREATE PROCEDURE [dbo].[TransactionTestAddStoredProcedure]
(
    @TvpParameters [dbo].[TransactionTestParameterTableType] READONLY
)
AS
BEGIN
    MERGE INTO [dbo].[TransactionTest] AS [target]
    USING   @TvpParameters AS [source]
    ON      [target].[Id] = [source].[Id]
    WHEN MATCHED THEN UPDATE SET 
        [target].[Name] = [source].[Name]
    ,   [target].[IsActive] = [source].[IsActive]
    WHEN NOT MATCHED THEN INSERT VALUES
    (
        [source].[Id]
    ,   [source].[IsActive]
    ,   [source].[Name]
    );
    
END