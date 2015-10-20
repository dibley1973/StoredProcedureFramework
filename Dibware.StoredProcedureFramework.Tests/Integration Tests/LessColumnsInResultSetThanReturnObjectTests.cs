using Dibware.StoredProcedureFramework.Tests.Integration_Tests.Base;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.DecimalTests;
using Dibware.StoredProcedureFrameworkForEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class LessColumnsInResultSetThanReturnObjectTests : BaseIntegrationTest
    {
        [TestMethod]
        [ExpectedException(typeof(MissingFieldException))]
        public void LessColumnsInProcedureResultSetThanReturnObject_ThrowsMissingFieldException()
        {
            var procedure = new DecimalTestStoredProcedure();
            
            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.Fail();
        }
    }
}