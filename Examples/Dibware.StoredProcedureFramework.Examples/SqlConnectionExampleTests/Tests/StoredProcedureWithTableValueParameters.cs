using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithTableValueParameters
        : SqlConnectionExampleTestBase
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
            Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
        }
    }
}
