using Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithoutParametersOrReturnType : DbContextExampleTestBase
    {
        /// <summary>
        /// Tenants the delete identifier.
        /// </summary>
        /// <remarks>Takes no parameters and returns no results</remarks>
        [TestMethod]
        public void TenantMarkAllinactive()
        {
            // ACT
            Context.TenantMarkAllInactive.Execute();
        }

        [TestMethod]
        public void AccountLastUpdatedDateTimeReset()
        {
            // ARRANGE
            var procedure = new AccountLastUpdatedDateTimeReset();

            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            // Nothing to assert
        }
    }
}