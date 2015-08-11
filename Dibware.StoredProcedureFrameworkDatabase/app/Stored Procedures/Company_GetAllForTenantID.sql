


-- =============================================
-- Author:      Duane Wingett
-- Create date: 2015-07-30
-- Description: Gets all companies for specified TenandID
-- =============================================
CREATE PROCEDURE [app].[Company_GetAllForTenantID]
(
    @TenantId INT
)
AS
BEGIN
       -- SET NOCOUNT ON added to prevent extra result sets from
       -- interfering with SELECT statements.
       SET NOCOUNT ON;

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