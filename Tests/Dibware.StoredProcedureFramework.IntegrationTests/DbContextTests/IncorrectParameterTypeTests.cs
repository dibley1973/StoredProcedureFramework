using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests
{
    [TestClass]
    public class IncorrectParameterTypeTests : BaseDbContextIntegrationTest
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
            Context.ExecuteStoredProcedure(procedure);

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
            Context.ExecuteStoredProcedure(procedure);
            
            // ASSERT
            // should experience an exception before here!
        }
    }
}