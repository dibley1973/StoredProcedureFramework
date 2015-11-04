-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015/11/04
-- Description:	Resets all Account's LastUpdatedDateTime to teh current time
-- =============================================
CREATE PROCEDURE [dbo].[AccountLastUpdatedDateTimeReset] 
AS
BEGIN
    UPDATE
        [app].[Account]
    SET
        [LastUpdatedDateTime] = GETDATE();
END