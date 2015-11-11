-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015/11/04
-- Description:	Resets all Account's LastUpdatedDateTime to teh current time
-- =============================================
CREATE PROCEDURE [dbo].[AccountLastUpdatedDateTimeReset] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE
        [app].[Account]
    SET
        [LastUpdatedDateTime] = GETDATE();
END