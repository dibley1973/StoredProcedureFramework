
CREATE VIEW [app].[CompanyForTenant]
AS
    SELECT      [TenantId]              = [tenant].[TenantId]
    ,           [TenantName]            = [tenant].[TenantName]
    ,           [CompanyId]             = [company].[CompanyId]
    ,           [IsActive]              = [company].[IsActive]
    ,           [CompanyName]           = [company].[CompanyName]
    ,           [RecordCreatedDateTime] = [company].[RecordCreatedDateTime]
    FROM        [app].[Company] [company]
    INNER JOIN  [app].[Tenant]  [tenant]
    ON          [company].[TenantID]    = [tenant].[TenantID]