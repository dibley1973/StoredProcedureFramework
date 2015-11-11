;WITH Sequence AS (
    SELECT RowNumber = row_number() over (order by @@spid)
    FROM sys.all_columns c1, sys.all_columns c2
)
INSERT INTO [app].[Account]
(
    CompanyId
,   IsActive
,   AccountName
)
SELECT
    1   AS [CompanyId]
,   1   AS [IsActive]
,   'John Doe ' + right('0000000' + cast(RowNumber as varchar(10)), 7) AS [AccountName]
FROM 
    Sequence 
WHERE 
    RowNumber <= 1000000;

;WITH Sequence AS (
    SELECT RowNumber = row_number() over (order by @@spid)
    FROM sys.all_columns c1, sys.all_columns c2
)
INSERT INTO [app].[Account]
(
    CompanyId
,   IsActive
,   AccountName
)
SELECT
    2   AS [CompanyId]
,   1   AS [IsActive]
,   'John Doe ' + right('0000000' + cast(RowNumber as varchar(10)), 7) AS [AccountName]
FROM 
    Sequence 
WHERE 
    RowNumber <= 2000000;