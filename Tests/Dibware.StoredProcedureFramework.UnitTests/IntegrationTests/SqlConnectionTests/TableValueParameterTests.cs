using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.TableValueParameter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class TableValueParameterTests : BaseIntegrationTest
    {
        [TestMethod]
        [Ignore] // until stored procedure is added to integration database!
        public void CompaniesAdd()
        {
            // ARRANGE
            var companiesToAdd = new List<CompanyTableType>
            {
                new CompanyTableType { CompanyName = "Company 1", IsActive = true, TenantId = 2 },
                new CompanyTableType { CompanyName = "Company 2", IsActive = false, TenantId = 2 },
                new CompanyTableType { CompanyName = "Company 3", IsActive = true, TenantId = 2 }
            };
            var parameters = new CompaniesAddParameters
            {
                Companies = companiesToAdd
            };
            var procedure = new CompaniesAdd(parameters);

            // ACT
            Connection.Open();
            Connection.ExecuteStoredProcedure(procedure);
            Connection.Close();

            // ASSERT
        }
    }
}