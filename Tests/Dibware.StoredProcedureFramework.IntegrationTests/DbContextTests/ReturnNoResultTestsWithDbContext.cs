using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests
{
    [TestClass]
    public class ReturnNoResultTestsWithDbContext : BaseDbContextIntegrationTest
    {
        [TestMethod]
        public void ReturnNoResultsProcedure_ReturnsNull()
        {
            // ARRANGE
            var parameters = new NullStoredProcedureParameters();
            var procedure = new ReturnNoResultStoredProcedure(parameters);

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.IsNull(results);
        }
    }
}