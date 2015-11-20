
-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015-09-29
-- Description:	Truncates the volumn and performance table
-- =============================================
CREATE PROCEDURE [dbo].[VolumeAndPerformanceTruncate] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    TRUNCATE TABLE [dbo].[VolumnAndPerformance];
END