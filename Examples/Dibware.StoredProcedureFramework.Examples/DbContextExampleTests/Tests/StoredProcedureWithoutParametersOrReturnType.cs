using Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithoutParametersOrReturnType : DbContextExampleTestBase
    {
        [TestMethod]
        public void TenantMarkAllinactive()
        {
            // ARRANGE

            // ACT
            Context.TenantMarkAllInactive.Execute();

            // ASSERT
            // Nothing to assert
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