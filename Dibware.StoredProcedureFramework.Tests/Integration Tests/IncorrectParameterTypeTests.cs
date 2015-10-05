using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.InvalidParameterType;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class IncorrectParameterTypeTests
    {
        [TestMethod]
        [ExpectedException(typeof(SqlParameterInvalidDataTypeException))]
        public void IncorrectStringParameterType_ThrowsSqlParameterInvalidDataTypeException()
        {
            var procedure = new IncorrectStringParameterStoredProcedure();
            procedure.InitializeFromAttributes();
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