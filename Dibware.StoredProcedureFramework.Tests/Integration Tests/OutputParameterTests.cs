using Dibware.StoredProcedureFramework.Tests.Integration_Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class OutputParameterTests : BaseIntegrationTest
    {
        [TestMethod]
        public void NullValueParameterProcedure_WithNullableParamatersAndReturnType_ReturnsCorrectValues()
        {
            // ARRANGE
            //int? expectedValue1 = 10;
            ////int? expectedvalue2 = null;
            //var parameters = new NullValueParameterParameters
            //{
            //    Value1 = expectedValue1,
            //    Value2 = null
            //};
            //var procedure = new NullValueParameterStoreProcedure(parameters);
            //procedure.InitializeFromAttributes();

            // ACT
            //var results = Context.ExecuteStoredProcedure(procedure);
            //var result = results.First();

            // ASSERT
            //Assert.AreEqual(expectedValue1, result.Value1);
            //Assert.IsNull(result.Value2);
        }
    }
}
