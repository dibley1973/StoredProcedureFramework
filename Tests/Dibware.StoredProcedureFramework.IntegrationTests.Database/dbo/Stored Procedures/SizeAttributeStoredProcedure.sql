-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015-08-26
-- Description:	Used for testing size attribute.
-- =============================================
CREATE PROCEDURE [dbo].[SizeAttributeStoredProcedure]
    @Value1 VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT CAST(@Value1 AS VARCHAR(255))   [Value1];

END