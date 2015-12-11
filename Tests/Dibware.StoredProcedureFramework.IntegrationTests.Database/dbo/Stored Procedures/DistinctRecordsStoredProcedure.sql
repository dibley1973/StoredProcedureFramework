

CREATE PROCEDURE [dbo].[DistinctRecordsStoredProcedure]
AS
BEGIN
    SELECT
        CAST(1 AS INT)          [Value1]
    ,   CAST(1 As BIT)          [Value2]
    ,   CAST(1 AS VARCHAR(1))   [Value3]
    UNION
        SELECT
        CAST(2 AS INT)          [Value1]
    ,   CAST(0 As BIT)          [Value2]
    ,   CAST(2 AS VARCHAR(1))   [Value3]
    UNION
        SELECT
        CAST(3 AS INT)          [Value1]
    ,   CAST(0 As BIT)          [Value2]
    ,   CAST(3 AS VARCHAR(1))   [Value3]
END