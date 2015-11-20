-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015-08-25
-- Description:	Returns the count of characters of first 
--              parameter in output paraemeter.
-- =============================================
CREATE PROCEDURE [dbo].[CountCharsInOutputParameterStoredProcedure]
    @Value1 varchar(100)
,   @Value2 int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Does nothing!
    SET @Value2 = LEN(@Value1);
END