
-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015/07/31
-- Description:	Gets the Tenant for the specified tenant name
-- =============================================
CREATE PROCEDURE [app].[Tenant_GetForTenantName]
	-- Add the parameters for the stored procedure here
	@TenantName varchar(100)
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
    WHERE       [TenantName] = @TenantName;
END