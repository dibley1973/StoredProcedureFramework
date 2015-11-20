using System;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class RecordSetNotInstantiatedTests : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        [Ignore] // Need to find out if there is a way to set this test up!
        [ExpectedException(typeof (NullReferenceException))]
        public void CallStoredProcedure_WithRecordSetNotInstantiatedInResultSet_ThrowsNullReferenceException()
        {
            // ARRANGE
            var parameters = new NullStoredProcedureParameters();
            var procedure = new NotInstantiatedRecordSetStoredProcedure(parameters);

            // ACT
            Connection.Open();
            Connection.ExecuteStoredProcedure(procedure);
            Connection.Close();

            // ASSERT
        }
    }
}