CREATE TABLE [dbo].[VolumnAndPerformance] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_VolumnAndPerformance_Id] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [FirstName]   VARCHAR (50)     NOT NULL,
    [LastName]    VARCHAR (50)     NOT NULL,
    [Address1]    VARCHAR (50)     NOT NULL,
    [Address2]    VARCHAR (50)     NOT NULL,
    [City]        VARCHAR (50)     NOT NULL,
    [County]      VARCHAR (50)     NOT NULL,
    [DateOfBirth] SMALLDATETIME    NOT NULL
);

