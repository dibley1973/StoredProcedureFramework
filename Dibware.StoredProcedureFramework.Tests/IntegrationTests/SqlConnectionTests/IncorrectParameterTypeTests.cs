using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.InvalidParameterType;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class IncorrectParameterTypeTests : BaseIntegrationTest
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

            // ACT
            Connection.Open();
            Connection.ExecuteStoredProcedure(procedure);
            Connection.Close();

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

            // ACT
            Connection.Open();
            Connection.ExecuteStoredProcedure(procedure);
            Connection.Close();

            // ASSERT
            // should experience an exception before here!
        }
    }
}