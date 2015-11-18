
CREATE PROCEDURE [dbo].[TableValueParameterStoredProcedureWithReturn]
(
    @TvpParameters [dbo].[SimpleTableValueParameterTableType] READONLY
)
AS
BEGIN
    SELECT 
        Id
    ,   IsActive
    ,   Name
    FROM
        @TvpParameters
END