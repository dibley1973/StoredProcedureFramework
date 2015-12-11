using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class DistinctRecordsTest
        : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        public void DistinctRecords_EnsurefirstAndLastRecordsDiffer()
        {
            // ARRANGE
            var procedure = new DistinctRecordsStoredProcedure();

            // ACT
            var results = Connection.ExecuteStoredProcedure(procedure);
            var firstResult = results.First();
            var lastResult = results.Last();

            // ASSERT
            Assert.AreNotSame(firstResult, lastResult);
        }
    }
}