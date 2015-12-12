using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.Functions;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    //[Ignore]
    public class FunctionTests : BaseSqlConnectionIntegrationTest
    {
        // This is a a little investigation into what it maight take to
        // adapt framework to handle scalar value functions..
        // Note need to change:
        //  command.CommandType = CommandType.Text
        //  command.CommandText = "SELECT  " + command.CommandText + "(PARAMS)"
        //  cannot access field by name, but by ordinal

        [TestMethod]
        public void ScalarValueFunctionWithParameterAndReturn()
        {
            // ARRANGE
            var parameters = new ScalarValueFunctionWithParameterAndReturn.Parameter
            {
                Value1 = 100
            };
            var function = new ScalarValueFunctionWithParameterAndReturn(parameters);

            // ACT
            var results = Connection.ExecuteStoredProcedure(function);

            // ASSERT
            Assert.IsNotNull(results);
            Assert.Fail("Hmm... reulsts have zero count!");
        }
    }
}