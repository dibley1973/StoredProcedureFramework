CREATE TABLE [app].[Tenant] (
    [TenantId]              INT            IDENTITY (1, 1) NOT NULL,
    [IsActive]              BIT            NOT NULL,
    [TenantName]            NVARCHAR (100) NULL,
    [RecordCreatedDateTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_app.Tenant] PRIMARY KEY CLUSTERED ([TenantId] ASC)
);

