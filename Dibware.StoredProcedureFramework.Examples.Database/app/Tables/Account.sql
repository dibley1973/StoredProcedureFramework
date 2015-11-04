CREATE TABLE [app].[Account] (
    [AccountId]             INT            IDENTITY (1, 1) NOT NULL,
    [CompanyId]             INT            NOT NULL,
    [IsActive]              BIT            NOT NULL,
    [AccountName]           NVARCHAR (100) NOT NULL,
    [RecordCreatedDateTime] DATETIME       NOT NULL DEFAULT (GETDATE()),
    [LastUpdatedDateTime]   DATETIME       NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT [PK_app.Account] PRIMARY KEY CLUSTERED ([AccountId] ASC),
    CONSTRAINT [FK_app.Account_app.Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [app].[Company] ([CompanyId]) ON DELETE CASCADE
);
