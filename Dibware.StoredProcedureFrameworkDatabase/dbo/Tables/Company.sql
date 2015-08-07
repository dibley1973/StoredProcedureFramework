CREATE TABLE [dbo].[Company] (
    [CompanyID]             INT            NOT NULL,
    [TenantID]              INT            NOT NULL,
    [IsActive]              BIT            NOT NULL,
    [CompanyName]           NVARCHAR (100) NULL,
    [RecordCreatedDateTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Company] PRIMARY KEY CLUSTERED ([CompanyID] ASC),
    CONSTRAINT [FK_dbo.Company_dbo.Tenant_TenantID] FOREIGN KEY ([TenantID]) REFERENCES [dbo].[Tenant] ([TenantID]) ON DELETE CASCADE
);

