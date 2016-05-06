-- =============================================
-- Author:		Duane Wingett
-- Create date: 2016-01-29
-- Description:	Returns a couple of double and a couple ofdecimals.
-- =============================================
CREATE PROCEDURE [dbo].[DoublesAndDecimalsTestStoredProcedure]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
        CAST(1.1 AS FLOAT)          [Value1]
    ,   CAST(2.2 AS FLOAT)          [Value2]
    ,   CAST(3.3 AS DECIMAL(5,2))   [Value3]
    ,   CAST(4.4 AS DECIMAL(5,2))   [Value4]

END