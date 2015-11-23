using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithParametersAndReturnType
        : SqlConnectionExampleTestBase
    {
        [TestMethod]
        public void CompanyGetAllForTenantId()
        {
            // ARRANGE
            var parameters = new TenantIdParameters
            {
                TenantId = 1
            };
            var procedure = new CompanyGetAllForTenantID(parameters);
            const int expectedCompanyCount = 2;

            // ACT
            List<CompanyDto> companies = Connection.ExecuteStoredProcedure(procedure);
            CompanyDto company1 = companies.FirstOrDefault();

            // ASSERT
            Assert.AreEqual(expectedCompanyCount, companies.Count);
            Assert.IsNotNull(company1);
        }
    }
}