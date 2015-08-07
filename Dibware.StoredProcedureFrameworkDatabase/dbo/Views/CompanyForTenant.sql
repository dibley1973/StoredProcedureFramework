
CREATE VIEW [dbo].[CompanyForTenant]
AS
    SELECT      [TenantID]              = [tenant].[TenantID]
    ,           [TenantName]            = [tenant].[TenantName]
    ,           [CompanyID]             = [company].[CompanyID]
    ,           [IsActive]              = [company].[IsActive]
    ,           [CompanyName]           = [company].[CompanyName]
    ,           [RecordCreatedDateTime] = [company].[RecordCreatedDateTime]
    FROM        [dbo].[Company] [company]
    INNER JOIN  [dbo].[Tenant]  [tenant]
    ON          [company].[TenantID]    = [tenant].[TenantID]