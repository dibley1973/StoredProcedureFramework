-- =============================================
-- Author:		Duane Wingett
-- Create date: 2016-01-26
-- Description:	Returns the count of characters of first 
--              parameter in return paraemeter.
-- =============================================
CREATE PROCEDURE [dbo].[CountCharsInReturnParameterStoredProcedure]
    @Value1 varchar(100)
--,   @Value2 int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Does nothing!
    --SET @Value2 = LEN(@Value1);
    RETURN LEN(@Value1);
END