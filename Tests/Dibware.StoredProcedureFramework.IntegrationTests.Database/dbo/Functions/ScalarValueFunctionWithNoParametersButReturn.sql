-- =============================================
-- Author:		Duane Wingett
-- Create date: 16-Dec-2015
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[ScalarValueFunctionWithNoParametersButReturn] ()
RETURNS int
AS
BEGIN
	DECLARE @Result int

	SELECT @Result = 202

	RETURN @Result
END