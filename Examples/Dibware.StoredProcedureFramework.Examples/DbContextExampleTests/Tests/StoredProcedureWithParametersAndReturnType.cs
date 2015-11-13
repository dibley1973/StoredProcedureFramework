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

        [TestMethod]
        public void CompanyGetAllForTenantId()
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
    }
}