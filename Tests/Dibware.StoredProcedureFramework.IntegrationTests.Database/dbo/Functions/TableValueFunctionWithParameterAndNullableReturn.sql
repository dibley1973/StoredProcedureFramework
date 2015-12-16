-- =============================================
-- Author:		Duane Wingett
-- Create date: 16-12-2015
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[TableValueFunctionWithParameterAndNullableReturn]
(
	@Value1 int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT 
        CAST(NULL As INT)                      AS Value1
    ,   CAST('Billy Bob' AS VARCHAR(25))    AS Value2

    UNION

    SELECT 
        CAST(2 * @Value1 As INT)                      AS Value1
    ,   CAST('John Conner' AS VARCHAR(25))  AS Value2

    UNION

    SELECT 
        CAST(3 * @Value1 As INT)                              AS Value1
    ,   CAST(Null AS VARCHAR(25))   AS Value2

)