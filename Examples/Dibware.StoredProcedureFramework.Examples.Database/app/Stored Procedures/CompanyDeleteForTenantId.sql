
-- =============================================
-- Author:      Duane Wingett
-- Create date: 2015-11-13
-- Description: Deletes all of the Companies for the Tenant specified by for specified TenandID
-- =============================================
CREATE PROCEDURE [app].[CompanyDeleteForTenantId]
(
    @TenantId INT
)
AS
BEGIN
   -- Insert statements for procedure here
    DELETE FROM
        app.Company
    WHERE
        TenantId = @TenantId;
END