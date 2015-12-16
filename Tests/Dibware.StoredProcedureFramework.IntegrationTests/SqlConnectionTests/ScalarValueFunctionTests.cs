using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.Functions;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class ScalarValueFunctionTests : BaseSqlConnectionIntegrationTest
    {
        // This is a a little investigation into what it maight take to
        // adapt framework to handle scalar-value and table-value functions..
        // Note need to change:
        //  command.CommandType = CommandType.Text
        //  command.CommandText = "SELECT  " + command.CommandText + "(PARAMS)"
        //  cannot access field by name, but by ordinal

        [TestMethod]
        public void ScalarValueFunctionWithNoParametersButReturn_ReturnsCorrectValue()
        {
            // ARRANGE
            const int expectedValue = 202;
            var function = new ScalarValueFunctionWithNoParametersButReturn();

            // ACT
            var results = Connection.ExecuteSqlScalarFunction(function);

            // ASSERT
            Assert.IsNotNull(results);
            Assert.AreEqual(expectedValue, results);
        }

        [TestMethod]
        public void ScalarValueFunctionWithParameterAndReturn_ReturnsCorrectValue()
        {
            // ARRANGE
            const int expectedValue = 100;
            var parameters = new ScalarValueFunctionWithParameterAndReturn.Parameter
            {
                Value1 = expectedValue
            };
            var function = new ScalarValueFunctionWithParameterAndReturn(parameters);

            // ACT
            var results = Connection.ExecuteSqlScalarFunction(function);

            // ASSERT
            Assert.IsNotNull(results);
            Assert.AreEqual(expectedValue, results);
        }

        [TestMethod]
        public void ScalarValueFunctionWithParameterAndNullReturn_ReturnsNullValue()
        {
            // ARRANGE
            const int expectedValue = 100;
            var parameters = new ScalarValueFunctionWithParameterAndNullReturn.Parameter
            {
                Value1 = expectedValue
            };
            var function = new ScalarValueFunctionWithParameterAndNullReturn(parameters);

            // ACT
            var results = Connection.ExecuteSqlScalarFunction(function);

            // ASSERT
            Assert.IsNull(results);
        }
    }
}