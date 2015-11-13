using Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithParametersButWithoutReturnType
        : SqlConnectionExampleTestBase
    {
        [TestMethod]
        public void CompanyDeleteForTenantID()
        {
            // ARRANGE
            var parameters = new TenantIdParameters
            {
                TenantId = 1
            };
            var procedure = new CompanyDeleteForTenantId(parameters);

            // ACT
            Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            // Nothing to Assert
        }
    }
}