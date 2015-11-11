-- =============================================
-- Author:      Duane Wingett
-- Create date: 2015-11-04
-- Description: Gets all accounts for teh company specified by Id
-- =============================================
CREATE PROCEDURE [app].[AccountGetAllForCompanyId]
(
    @CompanyId INT
)
AS
BEGIN
    SELECT 
        AccountId
    ,   CompanyId
    ,   IsActive
    ,   AccountName
    ,   RecordCreatedDateTime
    ,   LastUpdatedDateTime
    FROM
        app.Account
    WHERE
        CompanyId = CompanyId;
END
