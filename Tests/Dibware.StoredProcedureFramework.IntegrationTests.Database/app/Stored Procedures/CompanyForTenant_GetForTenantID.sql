


-- =============================================
-- Author:      Duane Wingett
-- Create date: 2015-08-01
-- Description: Gets all companies for a tenant specified by TenantID
-- =============================================
CREATE PROCEDURE [app].[CompanyForTenant_GetForTenantID]
(
    @TenantId INT
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Select data
    SELECT  [TenantId]
    ,       [TenantName]
    ,       [CompanyId]
    ,       [IsActive]
    ,       [CompanyName]
    ,       [RecordCreatedDateTime]
    FROM    [app].[CompanyForTenant]
    WHERE   [TenantId] = @TenantId;
END