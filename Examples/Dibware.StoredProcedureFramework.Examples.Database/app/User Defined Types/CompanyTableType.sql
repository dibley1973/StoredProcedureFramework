CREATE TYPE [app].[CompanyTableType] AS TABLE (
    [TenantId]    INT            NOT NULL,
    [IsActive]    BIT            NOT NULL,
    [CompanyName] NVARCHAR (100) NULL);

