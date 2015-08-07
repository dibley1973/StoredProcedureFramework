


-- =============================================
-- Author:      Duane Wingett
-- Create date: 2015-07-30
-- Description: Gets all companies for specified TenandID
-- =============================================
CREATE PROCEDURE [app].[Company_GetAllForTenantID]
(
    @TenantID INT
)
AS
BEGIN
       -- SET NOCOUNT ON added to prevent extra result sets from
       -- interfering with SELECT statements.
       SET NOCOUNT ON;

    -- Insert statements for procedure here
       SELECT 
        CompanyID
    ,   TenantID
    ,   IsActive
    ,   CompanyName
    ,   RecordCreatedDateTime
    FROM
        app.Company
    WHERE
        TenantID = @TenantID;
END