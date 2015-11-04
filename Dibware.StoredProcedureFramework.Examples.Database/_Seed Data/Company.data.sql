DECLARE @FirstTenantId INT;
SELECT  TOP 1 
            @FirstTenantId = TenantId
FROM        [app].[Tenant]
ORDER BY    [TenantId] DESC;


INSERT INTO [app].[Company]
(   [TenantId]
,   [IsActive]
,   [CompanyName]
)
VALUES
(   @FirstTenantId
,   1
,   'Ultra Mart'
);
INSERT INTO [app].[Company]
(   [TenantId]
,   [IsActive]
,   [CompanyName]
)
VALUES
(   @FirstTenantId
,   1
,   'Mart Plus'
);