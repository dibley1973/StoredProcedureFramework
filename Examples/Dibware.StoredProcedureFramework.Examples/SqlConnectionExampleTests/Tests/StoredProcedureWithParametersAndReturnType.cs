using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithParametersAndReturnType
    {
        #region Fields

        private string _connectionString;

        #endregion

        #region TestInitialize

        [TestInitialize]
        public void TestInitialize()
        {
            _connectionString = Properties.Settings.Default.ExampleDatabaseConnection;
        }

        #endregion

        [TestMethod]
        public void CompanyGetAllForTenantId()
        {
            // ARRANGE
            var parameters = new CompanyGetAllForTenantID.TenantIdParameters
            {
                TenantId = 1
            };
            var procedure = new CompanyGetAllForTenantID(parameters);
            List<CompanyDto> results;
            const int expectedCompanyCount = 2;

            // ACT
            using (DbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                results = connection.ExecuteStoredProcedure(procedure);
                connection.Close();
            }
            CompanyDto firstResult = results.FirstOrDefault();

            // ASSERT
            Assert.AreEqual(expectedCompanyCount, results.Count);
            Assert.IsNotNull(firstResult);
        }
    }
}
