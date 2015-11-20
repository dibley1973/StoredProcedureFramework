using System;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests
{
    [TestClass]
    public class LessColumnsInResultSetThanReturnObjectTest : BaseDbContextIntegrationTest
    {
        [TestMethod]
        [ExpectedException(typeof(MissingFieldException))]
        public void LessColumnsInProcedureResultSetThanReturnObject_ThrowsMissingFieldException()
        {
            var procedure = new LessColumnsInProcedureResultSetThanReturnStoredProcedure();

            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.Fail();
        }
    }
}