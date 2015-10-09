-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015-08-19
-- Description:	Used to test incorrect string parameters
-- =============================================
CREATE PROCEDURE [app].[IncorrectStringParameter] 
	-- Add the parameters for the stored procedure here
	@Value1 VARCHAR(10),
    @Value2 INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
        @Value1                         [Value1]
    ,   CAST(@Value2 as VARCHAR(10))  [Value2];
END