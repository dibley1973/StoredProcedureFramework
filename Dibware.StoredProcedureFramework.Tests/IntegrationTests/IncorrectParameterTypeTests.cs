using System.Configuration;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.InvalidParameterType;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests
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
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
            
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
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
            
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