
-- =============================================
-- Author:		Duane Wingett
-- Create date: 2015/11/12
-- Description:	Gets the Tenant for the specified tenant id
-- =============================================
CREATE PROCEDURE [app].[TenantGetForId]
	@TenantId INT
AS
BEGIN
    SELECT      [TenantId]
    ,           [IsActive]
    ,           [TenantName]
    ,           [RecordCreatedDateTime]
    FROM        [app].[Tenant]
    WHERE       [TenantId] = @TenantId;
END