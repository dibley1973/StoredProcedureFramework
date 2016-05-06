using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class IssueNumberEightTests
         : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void IssueNumberEight_WhenWrongDataType_ThrowsException()
        {
            // ARRANGE
            var procedure = new DoublesAndDecimalsTestStoredProcedure();

            // ACT
            Connection.ExecuteStoredProcedure(procedure);

            // ASSERT

        }
    }
}
