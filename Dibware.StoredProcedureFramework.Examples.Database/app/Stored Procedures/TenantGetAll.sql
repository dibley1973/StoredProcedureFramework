
-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015/08/10
-- Description:	Gets all the Tenants
-- =============================================
CREATE PROCEDURE [app].[TenantGetAll]
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
    FROM        [app].[Tenant];
END