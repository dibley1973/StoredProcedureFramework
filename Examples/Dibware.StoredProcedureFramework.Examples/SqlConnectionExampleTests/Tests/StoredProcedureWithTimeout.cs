using Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Tests
{
    [TestClass]
    [Ignore] // Because low timeouts are being ignored!
    public class StoredProcedureWithTimeout
        : SqlConnectionExampleTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public void StoredProcedure_WithTimeoutLessThanExecutionTime_TimesOut()
        {
            // ARRANGE
            const int expectedTimeout = 10;
            var parameters = new CompanyIdParameters
            {
                CompanyId = 1
            };
            var procedure = new AccountGetAllForCompanyId(parameters);

            // ACT
            Connection.ExecuteStoredProcedure(procedure,
                commandTimeout: expectedTimeout);

            // ASSERT
        }

        [TestMethod]
        public void StoredProcedure_WithTimeoutGreaterThanExecutionTime_CompletesExecution()
        {
            // ARRANGE
            const int expectedTimeout = 30;
            var parameters = new CompanyIdParameters
            {
                CompanyId = 1
            };
            var procedure = new AccountGetAllForCompanyId(parameters);

            // ACT

            Connection.ExecuteStoredProcedure(procedure,
                commandTimeout: expectedTimeout);


            // ASSERT

        }
    }
}
