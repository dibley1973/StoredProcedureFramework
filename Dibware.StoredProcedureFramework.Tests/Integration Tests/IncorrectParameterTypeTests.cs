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
            var parameters = new IncorrectParameterTypeStoredProcedureParameters()
            {
                Value1 = 10,
                Value2 = 5
            };
            var procedure = new IncorrectParameterTypeStoredProcedure(parameters);
            procedure.InitializeFromAttributes();
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
            IncorrectParameterTypeStoredProcedureResultSet resultSet;

            // ACT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ExecuteStoredProcedure(procedure);
            }
            
            // ASSERT
            // should experience an exception before here!
        }

        [TestMethod]
        [ExpectedException(typeof(SqlParameterInvalidDataTypeException))]
        public void IncorrectDecimalParameterType_ThrowsSqlParameterInvalidDataTypeException()
        {
            var parameters = new IncorrectParameterTypeStoredProcedureParameters()
            {
                Value1 = 10,
                Value2 = "EEE"
            };
            var procedure = new IncorrectParameterTypeStoredProcedure(parameters);
            procedure.InitializeFromAttributes();
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
            IncorrectParameterTypeStoredProcedureResultSet resultSet;

            // ACT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ExecuteStoredProcedure(procedure);
            }

            // ASSERT
            // should experience an exception before here!
        }
    }
}