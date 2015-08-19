-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015-08-19
-- Description:	Used to test decimal precision
-- =============================================
CREATE PROCEDURE [app].[DecimalPrecisionAndScale ]
	-- Add the parameters for the stored procedure here
	@Value1 decimal(10,3) = 0, 
	@Value2 decimal(7,1) = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
        @Value1, 
        CAST(@Value2 as decimal(10,3));
END