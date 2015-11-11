CREATE TABLE [app].[Company] (
    [CompanyId]             INT            IDENTITY (1, 1) NOT NULL,
    [TenantId]              INT            NOT NULL,
    [IsActive]              BIT            NOT NULL,
    [CompanyName]           NVARCHAR (100) NULL,
    [RecordCreatedDateTime] DATETIME       NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT [PK_app.Company] PRIMARY KEY CLUSTERED ([CompanyId] ASC),
    CONSTRAINT [FK_app.Company_app.Tenant_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [app].[Tenant] ([TenantId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_TenantId]
    ON [app].[Company]([TenantId] ASC);

