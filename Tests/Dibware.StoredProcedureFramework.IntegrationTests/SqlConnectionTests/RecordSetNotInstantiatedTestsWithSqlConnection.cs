using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class RecordSetNotInstantiatedTestsWithSqlConnection : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        [Ignore] // Need to find out if there is a way to set this test up!
        [ExpectedException(typeof(NullReferenceException))]
        public void CallStoredProcedure_WithRecordSetNotInstantiatedInResultSet_ThrowsNullReferenceException()
        {
            // ARRANGE
            var parameters = new NullStoredProcedureParameters();
            var procedure = new NotInstantiatedRecordSetStoredProcedure(parameters);

            // ACT
            Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
        }
    }
}