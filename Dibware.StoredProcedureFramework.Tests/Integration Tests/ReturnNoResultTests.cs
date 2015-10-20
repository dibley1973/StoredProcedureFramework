using Dibware.StoredProcedureFramework.Tests.Integration_Tests.Base;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures;
using Dibware.StoredProcedureFrameworkForEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class ReturnNoResultTests : BaseIntegrationTest
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