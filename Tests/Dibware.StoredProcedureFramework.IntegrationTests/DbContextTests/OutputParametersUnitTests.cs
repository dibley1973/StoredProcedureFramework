using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests
{
    [TestClass]
    public class OutputParametersUnitTests : BaseDbContextIntegrationTest
    {
        [TestMethod]
        public void CountCharsInOutputParameterStoredProcedure_WithOutputParamatersAndNoReturnType_ReturnsOutputParamtersCorrectly()
        {
            // ARRANGE
            const string expectedValue1 = "MonkeyTube";
            const int initialValue2 = 0;
            int expectedvalue2 = expectedValue1.Length;
            var parameters = new CountCharsInOutputParameterStoredProcedure.Parameter
            {
                Value1 = expectedValue1,
                Value2 = initialValue2
            };
            var procedure = new CountCharsInOutputParameterStoredProcedure(parameters);

            // ACT
            Context.ExecuteStoredProcedure(procedure);
            
            // ASSERT
            Assert.AreEqual(expectedvalue2, parameters.Value2);
        }
    }
}