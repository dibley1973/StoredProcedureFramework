using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Dibware.StoredProcedureFramework.Exceptions;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    /// <summary>
    /// Summary description for NullParameterTests
    /// </summary>
    [TestClass]
    public class NullParameterTests : BaseIntegrationTest
    {
        [TestMethod]
        public void NullValueParameterProcedure_WithNullableParamatersAndReturnType_ReturnsCorrectValues()
        {
            // ARRANGE
            int? expectedValue1 = 10;
            var parameters = new NullValueParameterAndNullableResultStoredProcedure.Parameter
            {
                Value1 = expectedValue1,
                Value2 = null
            };
            var procedure = new NullValueParameterAndNullableResultStoredProcedure(parameters);

            // ACT 
            Connection.Open();
            var resultSet = Connection.ExecuteStoredProcedure(procedure);
            Connection.Close();
            var result = resultSet.First();

            // ASSERT
            Assert.AreEqual(expectedValue1, result.Value1);
            Assert.IsNull(result.Value2);
        }

        [TestMethod]
        [ExpectedException(typeof(NullableFieldTypeException))]
        public void NullValueParameterProcedure_WithNonNullableParamatersAndReturnType_ThrowsNullableFieldTypeException()
        {
            // ARRANGE
            int? expectedValue1 = 10;
            var parameters = new NullValueParameterAndNonNullableResultStoredProcedure.Parameter
            {
                Value1 = expectedValue1,
                Value2 = null
            };
            var procedure = new NullValueParameterAndNonNullableResultStoredProcedure(parameters);

            // ACT
            Connection.Open();
            var resultSet = Connection.ExecuteStoredProcedure(procedure);
            Connection.Close();
            var result = resultSet.First();

            // ASSERT
            Assert.AreEqual(expectedValue1, result.Value1);
            Assert.IsNull(result.Value2);
        }

        [TestMethod]
        public void StringParameterStoredProcedure_WithNullValue_CorrectlyPassesNullValueToProcedure()
        {
            // ARRANGE
            const string nullValueString = null;
            var parameters = new StringParameterStoredProcedure.Parameter
            {
                Parameter1 = nullValueString
            };
            var procedure = new StringParameterStoredProcedure(parameters);

            // ACT
            Connection.Open();
            var resultSet = Connection.ExecuteStoredProcedure(procedure);
            Connection.Close();
            var result = resultSet.First();

            // ASSERT
            Assert.IsNotNull(resultSet);
            Assert.IsNull(result.Value1);
        }
    }
}
