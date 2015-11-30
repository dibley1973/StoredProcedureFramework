-- =============================================
-- Author:		Duane Wingett
-- Create date: 29-Nov-2015
-- Description:	
-- =============================================
CREATE FUNCTION [dbo].[ScalarValueFunctionWithParameterAndReturn]
(
	@Value1 int
)
RETURNS int
AS
BEGIN
	DECLARE @Result int

	SELECT @Result = @Value1

	RETURN @Result
END