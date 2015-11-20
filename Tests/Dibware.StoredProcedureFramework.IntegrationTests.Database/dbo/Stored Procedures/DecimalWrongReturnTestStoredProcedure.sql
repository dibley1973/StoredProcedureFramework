-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015-08-18
-- Description:	Returns a couple of decimals.
-- =============================================
CREATE PROCEDURE [dbo].[DecimalWrongReturnTestStoredProcedure]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
        CAST(123456.789 AS DECIMAL(9,3))        [Value1]
    ,   CAST(1234567890123.45 AS DECIMAL(15,2)) [Value2]
    ,   CAST(123.45 AS DECIMAL(5,2))            [Value3]

END