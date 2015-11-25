CREATE PROCEDURE [app].[CompanyCountAll]
AS
BEGIN
    SELECT 
        COUNT ([CompanyId]) [CountOfCompanies]
    FROM 
        [app].[Company];
END