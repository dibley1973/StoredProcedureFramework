using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFrameworkForEF;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests
{
    [TestClass]
    public class ReturnNoResultTestsWithDbContext : BaseIntegrationTestWithDbContext
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