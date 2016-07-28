


-- =============================================
-- Author:      Duane Wingett
-- Create date: 2015-07-30
-- Description: Gets all companies for specified TenandID
-- =============================================
CREATE PROCEDURE [app].[CompanyGetAllForTenantID]
(
    @TenantId INT
)
AS
BEGIN
    -- Insert statements for procedure here
       SELECT 
        CompanyId
    ,   TenantId
    ,   IsActive
    ,   CompanyName
    ,   RecordCreatedDateTime
    FROM
        app.Company
    WHERE
        TenantId = @TenantId;
END