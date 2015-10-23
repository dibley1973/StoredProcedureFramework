using System;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.DecimalTests;
using Dibware.StoredProcedureFrameworkForEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests
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