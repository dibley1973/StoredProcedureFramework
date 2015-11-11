-- =============================================
-- Author:      Duane Wingett
-- Create date: 2015-07-24
-- Description: Gets all companies
-- =============================================
CREATE PROCEDURE [app].[CompanyGetAll] 
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
        app.Company;
END