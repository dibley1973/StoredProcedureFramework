using Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithParametersButWithoutReturnType
        : DbContextExampleTestBase
    {
        [TestMethod]
        public void TenantDeleteId()
        {
            // ACT
            Context.TenantDeleteForId.ExecuteFor(new { TenantId = 100 });
        }

        [TestMethod]
        public void CompanyGetAllForTenantId()
        {
            // ARRANGE
            var parameters = new TenantIdParameters
            {
                TenantId = 1
            };
            var procedure = new CompanyDeleteForTenantId(parameters);

            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            // Nothing to assert
        }
    }
}