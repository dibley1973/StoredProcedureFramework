using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.Base;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.NullValueParameter;
using Dibware.StoredProcedureFrameworkForEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class NullParameterTests : BaseIntegrationTest
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
            procedure.InitializeFromAttributes();

            // ACT
            var resultSet = Context.ExecuteStoredProcedure(procedure);
            var results = resultSet.RecordSet1; 
            var result = results.First();

            // ASSERT
            Assert.AreEqual(expectedValue1, result.Value1);
            Assert.IsNull(result.Value2);
        }

        [TestMethod]
        [ExpectedException(typeof(NullableFieldTypeException))]
        public void NullValueParameterProcedure_WithNonNullableParamatersAndReturnType_Throws()
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
            procedure.InitializeFromAttributes();

            // ACT
            var resultSet = Context.ExecuteStoredProcedure(procedure);
            var results = resultSet.RecordSet1;
            var result = results.First();

            // ASSERT
            Assert.AreEqual(expectedValue1, result.Value1);
            Assert.IsNull(result.Value2);
        }
    }
}