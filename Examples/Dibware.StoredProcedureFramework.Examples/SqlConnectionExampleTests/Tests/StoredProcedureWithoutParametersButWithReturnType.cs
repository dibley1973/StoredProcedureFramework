using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithoutParametersButWithReturnType
        : SqlConnectionExampleTestBase
    {
        [TestMethod]
        public void TenantGetAll()
        {
            // ARRANGE
            var procedure = new TenantGetAll();
            const int expectedTenantCount = 2;

            // ACT
            // TODO: Investigate if stored procs can be called using the simplified API like DbContext has
            //var tenants = Connection.TenantGetAll.Execute();
            List<TenantDto> tenants = Connection.ExecuteStoredProcedure(procedure);
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
            var companies = Connection.ExecuteStoredProcedure(procedure);
            CompanyDto company1 = companies.FirstOrDefault();

            // ASSERT
            Assert.AreEqual(expectedCompanyCount, companies.Count);
            Assert.IsNotNull(company1);
        }
    }
}