using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;

namespace Dibware.StoredProcedureFramework.Tests.Examples.Tests
{
    [TestClass]
    public class NormalStoredProcedureTestsWithDbContext : BaseIntegrationTestWithDbContext
    {
        [TestMethod]
        public void EXAMPLE_NormalStoredProcedure_WhenCalledOnDbContext_ReturnsCorrectValues()
        {
            // ARRANGE  
            const int expectedId = 10;
            const string expectedName = @"Dave";
            const bool expectedActive = true;

            var parameters = new NormalStoredProcedureParameters
            {
                Id = expectedId
            };
            var procedure = new NormalStoredProcedure(parameters);
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
            NormalStoredProcedureResultSet resultSet;

            // ACT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                resultSet = connection.ExecuteStoredProcedure(procedure);
            }
            var results = resultSet.RecordSet1;
            var result = results.First();

            // ASSERT
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(expectedActive, result.Active);
        }

        [TestMethod]
        public void EXAMPLE_NormalStoredProcedure_WhenCalledOnSqlConnection_ReturnsCorrectValues()
        {
            // ARRANGE  
            const int expectedId = 10;
            const string expectedName = @"Dave";
            const bool expectedActive = true;

            var parameters = new NormalStoredProcedureParameters
            {
                Id = expectedId
            };
            NormalStoredProcedureResultSet resultSet;
            var procedure = new NormalStoredProcedure(parameters);
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

            // ACT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                resultSet = connection.ExecuteStoredProcedure(procedure);
            }
            var results = resultSet.RecordSet1;
            var result = results.First();

            // ASSERT
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(expectedActive, result.Active);
        }
    }
}
