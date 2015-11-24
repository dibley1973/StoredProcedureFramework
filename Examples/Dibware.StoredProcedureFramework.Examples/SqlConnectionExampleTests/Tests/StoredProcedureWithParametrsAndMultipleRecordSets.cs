using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithParametrsAndMultipleRecordSets
        : SqlConnectionExampleTestBase
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
            TenantCompanyAccountGetForTenantId.TenantCompanyAccountGetForTenantIdResultSet resultSet;
            const int expectedTenantCount = 1;
            const int expectedCompanyCount = 2;
            const int expectedAccountCount = 3000000;

            // ACT
            resultSet = Connection.ExecuteStoredProcedure(procedure);
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