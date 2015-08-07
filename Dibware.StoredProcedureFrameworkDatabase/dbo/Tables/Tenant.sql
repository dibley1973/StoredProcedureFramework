CREATE TABLE [dbo].[Tenant] (
    [TenantID]              INT            NOT NULL,
    [IsActive]              BIT            NOT NULL,
    [TenantName]            NVARCHAR (100) NULL,
    [RecordCreatedDateTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_app.Tenant] PRIMARY KEY CLUSTERED ([TenantID] ASC)
);

