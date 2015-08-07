
CREATE VIEW [app].[CompanyForTenant]
AS
    SELECT      [TenantID]              = [tenant].[TenantID]
    ,           [TenantName]            = [tenant].[TenantName]
    ,           [CompanyID]             = [company].[CompanyID]
    ,           [IsActive]              = [company].[IsActive]
    ,           [CompanyName]           = [company].[CompanyName]
    ,           [RecordCreatedDateTime] = [company].[RecordCreatedDateTime]
    FROM        [app].[Company] [company]
    INNER JOIN  [app].[Tenant]  [tenant]
    ON          [company].[TenantID]    = [tenant].[TenantID]