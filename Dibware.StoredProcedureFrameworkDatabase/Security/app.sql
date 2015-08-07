CREATE SCHEMA [app]
    AUTHORIZATION [dbo];






GO
GRANT SELECT
    ON SCHEMA::[app] TO [TestRole]
    AS [dbo];


GO
GRANT EXECUTE
    ON SCHEMA::[app] TO [TestRole]
    AS [dbo];

