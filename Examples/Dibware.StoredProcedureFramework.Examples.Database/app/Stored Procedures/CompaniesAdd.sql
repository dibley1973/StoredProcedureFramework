
CREATE PROCEDURE [app].[CompaniesAdd]
(
    @Companies [app].[CompanyTableType] READONLY
)
AS
BEGIN
    MERGE INTO [app].[Company] AS [target]
    USING   @Companies AS [source]
    ON      [target].[TenantId] = [source].[TenantId]
    AND     [target].[CompanyName] = [source].[CompanyName]
    WHEN MATCHED THEN UPDATE SET 
        [target].[IsActive] = [source].[IsActive]
    WHEN NOT MATCHED THEN INSERT VALUES
    (
        [source].[TenantId]
    ,   [source].[IsActive]
    ,   [source].[CompanyName]
    ,   GETDATE()
    );
END