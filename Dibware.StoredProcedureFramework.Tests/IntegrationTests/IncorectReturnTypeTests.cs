using System;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.DecimalTests;
using Dibware.StoredProcedureFrameworkForEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests
{
    [TestClass]
    public class IncorectReturnTypeTests : BaseIntegrationTest
    {
        [TestMethod]
        [ExpectedException(typeof (InvalidCastException))]
        public void DifferentDataTypeInReturnTypeThanProcedureResultSet_ThrowsInvalidCastException()
        {
            var procedure = new DecimalWrongReturnTestStoredProcedure();
            
            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.Fail();
        }

    }
}