using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.Functions;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class FunctionTests : BaseSqlConnectionIntegrationTest
    {
        // This is a a little investigation into what it maight take to
        // adapt framework to handle scalar-value and table-value functions..
        // Note need to change:
        //  command.CommandType = CommandType.Text
        //  command.CommandText = "SELECT  " + command.CommandText + "(PARAMS)"
        //  cannot access field by name, but by ordinal

        [TestMethod]
        public void ScalarValueFunctionWithParameterAndReturn()
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
        public void TableValueFunctionWithParameterAndReturn()
        {
            // ARRANGE
            const int expectedCount = 3;
            const int expectedLastValue1 = 300;
            const string expectedLastValue2 = "Raymond Reddington";
            var parameters = new TableValueFunctionWithParameterAndReturn.Parameter
            {
                Value1 = 100
            };
            var function = new TableValueFunctionWithParameterAndReturn(parameters);

            // ACT
            var results = Connection.ExecuteSqlTableFunction(function);

            // ASSERT
            Assert.IsNotNull(results);
            Assert.AreEqual(expectedCount, results.Count);
            Assert.AreEqual(expectedLastValue1, results.Last().Value1);
            Assert.AreEqual(expectedLastValue2, results.Last().Value2);
        }
    }
}