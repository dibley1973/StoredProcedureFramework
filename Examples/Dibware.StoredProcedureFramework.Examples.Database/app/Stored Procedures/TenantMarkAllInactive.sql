
-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015/11/12
-- Description:	Marks all Tenants as inactive
CREATE PROCEDURE [app].[TenantMarkAllInactive]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE  [app].[Tenant]
    SET     [IsActive] = 0;
END