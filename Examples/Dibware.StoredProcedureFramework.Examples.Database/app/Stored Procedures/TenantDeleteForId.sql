CREATE PROCEDURE [app].[TenantDeleteForId] 
    @TenantId INT
AS
BEGIN
    -- Insert statements for procedure here
	DELETE FROM 
        [app].[Tenant]
    WHERE 
        [TenantId] = @TenantId;
END