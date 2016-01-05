using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithTableValueParameters : DbContextExampleTestBase
    {
        [TestMethod]
        public void CompaniesAdd()
        {
            // ARRANGE
            var companiesToAdd = new List<CompaniesAdd.CompanyTableType>
            {
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 1", IsActive = true, TenantId = 2 },
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 2", IsActive = false, TenantId = 2 },
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 3", IsActive = true, TenantId = 2 }
            };
            var parameters = new CompaniesAdd.CompaniesAddParameters
            {
                Companies = companiesToAdd
            };
            var procedure = new CompaniesAdd(parameters);

            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
        }
    }
}