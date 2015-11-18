
-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015/08/10
-- Description:	Gets all the Tenants
-- =============================================
CREATE PROCEDURE [app].[Tenant_GetAll]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT      [TenantID]
    ,           [IsActive]
    ,           [TenantName]
    ,           [RecordCreatedDateTime]
    FROM        [app].[Tenant];
END