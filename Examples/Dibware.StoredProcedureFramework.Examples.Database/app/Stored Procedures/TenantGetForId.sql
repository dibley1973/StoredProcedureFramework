
-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015/11/12
-- Description:	Gets the Tenant for the specified tenant id
-- =============================================
CREATE PROCEDURE [app].[TenantGetForId]
	-- Add the parameters for the stored procedure here
	@TenantId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT      [TenantId]
    ,           [IsActive]
    ,           [TenantName]
    ,           [RecordCreatedDateTime]
    FROM        [app].[Tenant]
    WHERE       [TenantId] = @TenantId;
END