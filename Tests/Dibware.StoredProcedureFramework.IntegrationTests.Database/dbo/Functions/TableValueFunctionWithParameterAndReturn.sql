-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[TableValueFunctionWithParameterAndReturn]
(
	@Value1 int
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT 
        CAST(1 * @Value1 As INT)                      AS Value1
    ,   CAST('Billy Bob' AS VARCHAR(25))    AS Value2

    UNION

    SELECT 
        CAST(2 * @Value1 As INT)                      AS Value1
    ,   CAST('John Conner' AS VARCHAR(25))  AS Value2

    UNION

    SELECT 
        CAST(3 * @Value1 As INT)                              AS Value1
    ,   CAST('Raymond Reddington' AS VARCHAR(25))   AS Value2

)