using Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithParametersButWithoutReturnType : DbContextExampleTestBase
    {
        /// <summary>
        /// Tenants the delete identifier.
        /// </summary>
        /// <remarks>Takes parameters but returns no results</remarks>
        [TestMethod]
        public void TenantDeleteId()
        {
            // ACT
            Context.TenantDeleteForId.ExecuteFor(new { Id = 100 });
        }
    }
}