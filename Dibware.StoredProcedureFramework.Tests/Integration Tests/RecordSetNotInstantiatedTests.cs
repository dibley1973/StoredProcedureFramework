using System;
using System.Configuration;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class RecordSetNotInstantiatedTests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CallStoredProcedure_WithRecordSetNotInstantiatedInResultSet_ThrowsNullReferenceException()
        {
            // ARRANGE
            var parameters = new NullStoredProcedureParameters();
            var procedure = new NotInstantiatedRecordSetStoredProcedure(parameters);
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

            // ACT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ExecuteStoredProcedure(procedure);
            }

            // ASSERT
            Assert.Fail();
        }
    }
}
