-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015-11-17
-- Description:	A stored procedure that has a string parameter
-- =============================================
CREATE PROCEDURE [dbo].[StringParameterStoredProcedure]
	@Parameter1 VARCHAR(MAX)
AS
BEGIN
	SELECT  @Parameter1     [Value1]
END