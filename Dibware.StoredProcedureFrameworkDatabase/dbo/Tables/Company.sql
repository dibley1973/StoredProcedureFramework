CREATE TABLE [dbo].[Company] (
    [CompanyId]             INT            IDENTITY (1, 1) NOT NULL,
    [TenantID]              INT            NOT NULL,
    [IsActive]              BIT            NOT NULL,
    [CompanyName]           NVARCHAR (100) NULL,
    [RecordCreatedDateTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Company] PRIMARY KEY CLUSTERED ([CompanyId] ASC),
    CONSTRAINT [FK_dbo.Company_dbo.Tenant_TenantID] FOREIGN KEY ([TenantID]) REFERENCES [dbo].[Tenant] ([TenantId]) ON DELETE CASCADE
);




GO
CREATE NONCLUSTERED INDEX [IX_TenantID]
    ON [dbo].[Company]([TenantID] ASC);

