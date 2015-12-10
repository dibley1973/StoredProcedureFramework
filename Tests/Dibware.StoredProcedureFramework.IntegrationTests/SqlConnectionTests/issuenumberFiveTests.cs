using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    /// <summary>
    /// These tests are for the issue identified in Issue No 5
    /// </summary>
    [TestClass]
    public class IssueNumberFiveTests
         : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        public void IssueNumberFiveWithCorrectDataType_ExecuteswithoutIssue()
        {
            // ARRANGE
            var procedure = new IssueNumberFiveWithCorrectDataType();

            // ACT
            var result = Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            
        }

        [TestMethod]
        public void IssueNumberFiveWithIncorrectDataType_doesWhat()
        {
            // ARRANGE
            var procedure = new IssueNumberFiveWithIncorrectDataType();

            // ACT
            var result = Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
        }
    }
}
