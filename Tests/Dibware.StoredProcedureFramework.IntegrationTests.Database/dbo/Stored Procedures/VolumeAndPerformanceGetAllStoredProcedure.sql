
-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015-09-29
-- Description:	Selects all from the volumn and performance table
-- =============================================
CREATE PROCEDURE [dbo].[VolumeAndPerformanceGetAllStoredProcedure]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT
        Id, 
        FirstName, 
        LastName, 
        Address1, 
        Address2, 
        City, 
        County, 
        DateOfBirth
    FROM
        [dbo].[VolumnAndPerformance];
END