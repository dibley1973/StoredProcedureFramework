using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class IncorrectParameterTypeTestsWithSqlConnection
        : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        [ExpectedException(typeof(SqlParameterInvalidDataTypeException))]
        public void IncorrectStringParameterType_ThrowsSqlParameterInvalidDataTypeException()
        {
            var parameters = new IncorrectParameterTypeStoredProcedure.Parameter
            {
                Value1 = 10,
                Value2 = 5
            };
            var procedure = new IncorrectParameterTypeStoredProcedure(parameters);

            // ACT
            Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            // should experience an exception before here!
        }

        [TestMethod]
        [ExpectedException(typeof(SqlParameterInvalidDataTypeException))]
        public void IncorrectDecimalParameterType_ThrowsSqlParameterInvalidDataTypeException()
        {
            var parameters = new IncorrectParameterTypeStoredProcedure.Parameter
            {
                Value1 = 10,
                Value2 = "EEE"
            };
            var procedure = new IncorrectParameterTypeStoredProcedure(parameters);

            // ACT
            Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            // should experience an exception before here!
        }
    }
}