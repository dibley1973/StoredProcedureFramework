-- =============================================
-- Author:		Duane Wingett
-- Create date: 16-dec-2015
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[ScalarValueFunctionWithParameterAndNullReturn]
(
	@Value1 int
)
RETURNS int
AS
BEGIN
	DECLARE @Result int

	SELECT @Result = NULL

	RETURN @Result
END