CREATE ROLE [TestRole]
    AUTHORIZATION [dbo];


GO
ALTER ROLE [TestRole] ADD MEMBER [TestUser];

