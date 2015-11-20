-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015-08-19
-- Description:	Used to test decimal precision
-- =============================================
CREATE PROCEDURE [dbo].[DecimalPrecisionAndScaleStoredProcedure] 
	-- Add the parameters for the stored procedure here
	@Value1 decimal(10,3) = 0, 
	@Value2 decimal(7,1) = 0
AS
BEGIN
    -- Insert statements for procedure here
	SELECT 
        @Value1                         [Value1]
    ,   CAST(@Value2 as decimal(10,3))  [Value2];
END