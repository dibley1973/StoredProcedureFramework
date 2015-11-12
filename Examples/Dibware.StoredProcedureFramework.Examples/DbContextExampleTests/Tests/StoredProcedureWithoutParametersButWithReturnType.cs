using Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithoutParametersButWithReturnType
        : DbContextExampleTestBase
    {
        /// <summary>
        /// Tenants the get all.
        /// </summary>
        /// <remarks>Takes no parameters but returns results</remarks>
        [TestMethod]
        public void TenantGetAll()
        {
            // ARRANGE
            const int expectedTenantCount = 1;

            // ACT
            var tenants = Context.TenantGetAll.Execute();
            TenantDto tenant1 = tenants.FirstOrDefault();

            // ASSERT
            Assert.AreEqual(expectedTenantCount, tenants.Count);
            Assert.IsNotNull(tenant1);
        }

        [TestMethod]
        public void CompanyGetAll()
        {
            // ARRANGE
            var procedure = new CompanyGetAll();
            const int expectedCompanyCount = 2;

            // ACT
            var companies = Context.ExecuteStoredProcedure(procedure);
            CompanyDto company1 = companies.FirstOrDefault();

            // ASSERT
            Assert.AreEqual(expectedCompanyCount, companies.Count);
            Assert.IsNotNull(company1);
        }
    }
}