-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015-08-24
-- Description:	A stored procedure that allows for check for null parameter
-- =============================================
CREATE PROCEDURE [app].[NullValueParameterAndResult]
	
	@Value1 int,
	@Value2 int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  @Value1     [Value1]
    ,       @value2     [Value2]
END