using Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithParametersAndReturnType : DbContextExampleTestBase
    {
        /// <summary>
        /// Tenants for identifier.
        /// </summary>
        /// <remarks>Takes parameters and returns results</remarks>
        [TestMethod]
        public void TenantGetForId()
        {
            // ACT
            var tenant = Context.TenantGetForId.ExecuteFor(new { TenantId = 1 });

            // ASSERT
            Assert.IsNotNull(tenant);
        }
    }
}