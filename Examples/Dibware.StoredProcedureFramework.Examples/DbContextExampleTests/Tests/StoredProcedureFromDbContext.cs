using Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Context;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureFromDbContext
    {
        [TestMethod]
        public void CompanyGetAllForTenantId()
        {
            // ARRANGE
            const int expectedCompanyCount = 2;
            string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
            var parameters = new TenantIdParameters { TenantId = 1 };
            var procedure = new CompanyGetAllForTenantID(parameters);
            List<CompanyDto> companies;
            CompanyDto company1;

            // ACT
            using (ApplicationDbContext context = new ApplicationDbContext(connectionName))
            {
                companies = context.ExecuteStoredProcedure(procedure);
                company1 = companies.FirstOrDefault();
            }

            // ASSERT
            Assert.AreEqual(expectedCompanyCount, companies.Count);
            Assert.IsNotNull(company1);
        }
    }
}