using System;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class LessColumnsInResultSetThanReturnObjectTest : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        [ExpectedException(typeof(MissingFieldException))]
        public void LessColumnsInProcedureResultSetThanReturnObject_ThrowsMissingFieldException()
        {
            var procedure = new LessColumnsInProcedureResultSetThanReturnStoredProcedure();

            // ACT
            Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.Fail();
        }
    }
}