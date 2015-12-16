using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.Functions;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    /// <summary>
    /// Summary description for TableValueFunctionTests
    /// </summary>
    [TestClass]
    public class TableValueFunctionTests : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        public void TableValueFunctionWithNoParameterbutWithReturn_ReturnsCorrectRecords()
        {
            // ARRANGE
            const int expectedCount = 3;
            const int expectedLastValue1 = 300;
            const string expectedLastValue2 = "Raymond Reddington";
            var function = new TableValueFunctionWithoutParameterButWithReturn();

            // ACT
            var results = Connection.ExecuteSqlTableFunction(function);

            // ASSERT
            Assert.IsNotNull(results);
            Assert.AreEqual(expectedCount, results.Count);
            Assert.AreEqual(expectedLastValue1, results.Last().Value1);
            Assert.AreEqual(expectedLastValue2, results.Last().Value2);
        }

        [TestMethod]
        public void TableValueFunctionWithParameterAndReturn_ReturnsCorrectRecords()
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

        [TestMethod]
        public void TableValueFunctionWithParameterAndNullableReturn_ReturnsCorrectRecords()
        {
            // ARRANGE
            const int expectedCount = 3;
            //const int? expectedLastValue3 = null;
            //const string expectedLastValue3 = null;
            var parameters = new TableValueFunctionWithParameterAndNullableReturn.Parameter
            {
                Value1 = 100
            };
            var function = new TableValueFunctionWithParameterAndNullableReturn(parameters);

            // ACT
            var results = Connection.ExecuteSqlTableFunction(function);

            // ASSERT
            Assert.IsNotNull(results);
            Assert.AreEqual(expectedCount, results.Count);
            Assert.IsNull(results.First().Value1);
            Assert.IsNull(results.Last().Value2);
        }
    }
}
