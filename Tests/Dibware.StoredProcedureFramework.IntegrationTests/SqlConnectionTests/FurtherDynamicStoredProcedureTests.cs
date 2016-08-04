using System.Collections.Generic;
using System.Linq;
using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class FurtherDynamicStoredProcedureTests
        : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        public void IssueNumberTenTests_ReturnsResults_AfterCallingStoreProcedure()
        {
            // ARRANGE
            IStoredProcedure<List<dynamic>, NullStoredProcedureParameters> procedure = (IStoredProcedure<List<dynamic>, NullStoredProcedureParameters>)new DynamicColumnStoredProcedure2();

            // ACT
            List<dynamic> results = Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.IsTrue(results.Any());
        }
    }
}