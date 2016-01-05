using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithParametrsAndMultipleRecordSets
        : DbContextExampleTestBase
    {
        [TestMethod]
        public void TenantCompanyAccountGetForTenantId_ReturnsAllThreeRecordSets()
        {
            // ARRANGE
            var parameters = new TenantIdParameters
            {
                TenantId = 1
            };
            var procedure = new TenantCompanyAccountGetForTenantId(parameters);
            const int expectedTenantCount = 1;
            const int expectedCompanyCount = 2;
            const int expectedAccountCount = 3000000;

            // ACT
            var resultSet = Context.ExecuteStoredProcedure(procedure);
            List<TenantDto> tenants = resultSet.Tenants;
            List<CompanyDto> companies = resultSet.Companies;
            List<AccountDto> accounts = resultSet.Accounts;

            // ASSERT
            Assert.IsNotNull(tenants);
            Assert.IsNotNull(companies);
            Assert.IsNotNull(accounts);
            Assert.AreEqual(expectedTenantCount, tenants.Count);
            Assert.AreEqual(expectedCompanyCount, companies.Count);
            Assert.AreEqual(expectedAccountCount, accounts.Count);
        }
    }
}