CREATE TABLE [app].[Company] (
    [CompanyID]             INT            NOT NULL,
    [TenantID]              INT            NOT NULL,
    [IsActive]              BIT            NOT NULL,
    [CompanyName]           NVARCHAR (100) NULL,
    [RecordCreatedDateTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_app.Company] PRIMARY KEY CLUSTERED ([CompanyID] ASC),
    CONSTRAINT [FK_app.Company_app.Tenant_TenantID] FOREIGN KEY ([TenantID]) REFERENCES [app].[Tenant] ([TenantID]) ON DELETE CASCADE
);

