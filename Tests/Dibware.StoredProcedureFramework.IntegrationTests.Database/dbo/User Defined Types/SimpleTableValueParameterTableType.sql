CREATE TYPE [dbo].[SimpleTableValueParameterTableType] AS TABLE (
    [Id]        INT            NOT NULL,
    [IsActive]  BIT            NOT NULL,
    [Name]      NVARCHAR (100) NULL
);