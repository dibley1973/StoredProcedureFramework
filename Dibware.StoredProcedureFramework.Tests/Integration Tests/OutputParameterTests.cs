﻿using Dibware.StoredProcedureFramework.Tests.Integration_Tests.Base;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.CountCharsInOutputParameterProcedures;
using Dibware.StoredProcedureFrameworkForEF;
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
            const string expectedValue1 = "MonkeyTube";
            const int initialValue2 = 0;
            int expectedvalue2 = expectedValue1.Length;
            var parameters = new CountCharsInOutputParameterParameters
            {
                Value1 = expectedValue1,
                Value2 = initialValue2
            };
            var procedure = new CountCharsInOutputParameterStoredProcedure(parameters);
            procedure.InitializeFromAttributes();

            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.AreEqual(expectedvalue2, parameters.Value2);
        }
    }
}