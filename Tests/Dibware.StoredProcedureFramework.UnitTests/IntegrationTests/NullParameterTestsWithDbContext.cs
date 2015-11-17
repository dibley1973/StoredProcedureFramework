using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.NullValueParameter;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests
{
    [TestClass]
    public class NullParameterTestsWithDbContext : BaseIntegrationTestWithDbContext
    {
        [TestMethod]
        public void NullValueParameterProcedure_WithNullableParamatersAndReturnType_ReturnsCorrectValues()
        {
            // ARRANGE
            int? expectedValue1 = 10;
            //int? expectedvalue2 = null;
            var parameters = new NullValueParameterParameters
            {
                Value1 = expectedValue1,
                Value2 = null
            };
            var procedure = new NullValueParameterStoreProcedure(parameters);

            // ACT
            var resultSet = Context.ExecuteStoredProcedure(procedure);
            //var results = resultSet.RecordSet1;
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
            //int? expectedvalue2 = null;
            var parameters = new NullValueParameterParameters
            {
                Value1 = expectedValue1,
                Value2 = null
            };
            var procedure = new NullValueParameterStoreProcedure2(parameters);

            // ACT
            var resultSet = Context.ExecuteStoredProcedure(procedure);
            //var results = resultSet.RecordSet1;
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
            var parameters = new StringParameterStoredProcedure.StringParameterStoredProcedureParameters
            {
                Parameter1 = nullValueString
            };
            var procedure = new StringParameterStoredProcedure(parameters);

            // ACT
            var resultSet = Context.ExecuteStoredProcedure(procedure);
            var result = resultSet.First();

            // ASSERT
            Assert.IsNotNull(resultSet);
            Assert.IsNull(result.Value1);
        }
    }
}