using Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithParametersAndReturnType : DbContextExampleTestBase
    {
        [TestMethod]
        // This method of calling uses the traditional method
        public void CompanyGetAllForTenantId_TRADITIONAL()
        {
            // ARRANGE
            const int expectedCompanyCount = 2;
            var parameters = new TenantIdParameters
            {
                TenantId = 1
            };
            var procedure = new CompanyGetAllForTenantID(parameters);

            // ACT
            var companies = Context.ExecuteStoredProcedure(procedure);
            CompanyDto company1 = companies.FirstOrDefault();

            // ASSERT
            Assert.AreEqual(expectedCompanyCount, companies.Count);
            Assert.IsNotNull(company1);
        }

        [TestMethod]
        // This method of calling uses the simplified method
        public void AccountGetAllForCompanyId_SIMPLIFIED()
        {
            // ARRANGE
            const int expectedAccountCount = 3000000;
            var parameters = new CompanyIdParameters
            {
                CompanyId = 1
            };

            // ACT
            var accounts = Context.AccountGetAllForCompanyId.ExecuteFor(parameters);
            var actualAccountcount = accounts.Count;

            // ASSERT
            Assert.AreEqual(expectedAccountCount, actualAccountcount);
        }

        [TestMethod]
        // This method of calling uses the simplified in-line method
        public void TenantGetForId_SIMPLIFIED_INLINE()
        {
            // ACT
            var tenant = Context.TenantGetForId.ExecuteFor(new { TenantId = 1 });

            // ASSERT
            Assert.IsNotNull(tenant);
        }


    }
}