


-- =============================================
-- Author:      Duane Wingett
-- Create date: 2015-07-24
-- Description: Gets all companies
-- =============================================
CREATE PROCEDURE [app].[Company_GetAll] 
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
        app.Company;
END