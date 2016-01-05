using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.Properties;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureFromSqlConnection
    {
        [TestMethod]
        public void CompanyGetAllForTenantId()
        {
            // ARRANGE
            const int expectedCompanyCount = 2;
            string connectionName = Settings.Default.ExampleDatabaseConnection;
            var parameters = new TenantIdParameters { TenantId = 1 };
            var procedure = new CompanyGetAllForTenantID(parameters);
            List<CompanyDto> companies;
            CompanyDto company1;

            // ACT
            using (SqlConnection connection = new SqlConnection(connectionName))
            {
                companies = connection.ExecuteStoredProcedure(procedure);
                company1 = companies.FirstOrDefault();
            }

            // ASSERT
            Assert.AreEqual(expectedCompanyCount, companies.Count);
            Assert.IsNotNull(company1);
        }
    }
}
