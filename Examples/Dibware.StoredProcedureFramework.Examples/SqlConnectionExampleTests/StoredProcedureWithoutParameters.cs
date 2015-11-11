using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests
{
    [TestClass]
    public class StoredProcedureWithoutParameters
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
        public void TenantGetAll()
        {
            // ARRANGE
            var procedure = new TenantGetAll();
            List<TenantDto> results;
            const int expectedTenantCount = 1;

            // ACT
            using (DbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                results = connection.ExecuteStoredProcedure(procedure);
                connection.Close();
            }
            TenantDto firstResult = results.FirstOrDefault();

            // ASSERT
            Assert.AreEqual(expectedTenantCount, results.Count);
            Assert.IsNotNull(firstResult);
        }
    }
}