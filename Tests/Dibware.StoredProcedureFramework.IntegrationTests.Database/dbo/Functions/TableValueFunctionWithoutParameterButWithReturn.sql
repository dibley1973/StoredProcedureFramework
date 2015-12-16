-- =============================================
-- Author:		Duane Wingett
-- Create date: 16-12-2015
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[TableValueFunctionWithoutParameterButWithReturn] ()
RETURNS TABLE 
AS
RETURN 
(
	SELECT 
        CAST(1 * 100 As INT)                      AS Value1
    ,   CAST('Billy Bob' AS VARCHAR(25))    AS Value2

    UNION

    SELECT 
        CAST(2 * 100 As INT)                      AS Value1
    ,   CAST('John Conner' AS VARCHAR(25))  AS Value2

    UNION

    SELECT 
        CAST(3 * 100 As INT)                              AS Value1
    ,   CAST('Raymond Reddington' AS VARCHAR(25))   AS Value2

)