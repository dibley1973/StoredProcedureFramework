
--DROP PROCEDURE app.TenantCompanyAccountGetForTenantId
--go
CREATE PROCEDURE app.TenantCompanyAccountGetForTenantId
    @TenantId int
AS
BEGIN
    SELECT
        [TenantId]
    ,   [IsActive]
    ,   [TenantName]
    ,   [RecordCreatedDateTime]
    FROM
        [app].[Tenant]
    WHERE
        [TenantId] = @TenantId;

    SELECT 
        [CompanyId]
    ,   [TenantId]
    ,   [IsActive]
    ,   [CompanyName]
    ,   [RecordCreatedDateTime]
    FROM
        [app].[Company]
    WHERE
        [TenantId] = @TenantId;

    SELECT 
        [AccountId]
    ,   [company].[CompanyId]
    ,   [account].[IsActive]
    ,   [AccountName]
    ,   [account].[RecordCreatedDateTime]
    ,   [LastUpdatedDateTime]
    FROM
        [app].[Account] [account]
    INNER JOIN
        [app].[Company] [company]
    ON 
        [company].[CompanyId] = [account].[CompanyId]
    AND
        [company].[TenantId] = @TenantId;

END