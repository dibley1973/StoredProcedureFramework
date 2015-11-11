-- =============================================
-- Author:      Duane Wingett
-- Create date: 2015-11-04
-- Description: Gets all accounts
-- =============================================
CREATE PROCEDURE [app].[AccountGetAll]
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
        app.Account;
END
