using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class SqlConnectionTests
    {
        [TestMethod]
        public void ExecuteStoredProcedure_WhenNotAlreadyOpened_ClosesConnection()
        {
            // ARRANGE  
            const int expectedId = 10;
            bool connectionStillOpen;

            var parameters = new NormalStoredProcedureParameters
            {
                Id = expectedId
            };
            var procedure = new NormalStoredProcedure(parameters);
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

            // ACT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //connection.Open();
                connection.ExecuteStoredProcedure(procedure);
                connectionStillOpen = connection.State == ConnectionState.Open;
            }

            // ASSERT
            Assert.IsFalse(connectionStillOpen);
        }

        [TestMethod]
        public void ExecuteStoredProcedure_WhenAlreadyOpened_KeepsConnectionOpen()
        {
            // ARRANGE  
            const int expectedId = 10;
            bool connectionStillOpen;

            var parameters = new NormalStoredProcedureParameters
            {
                Id = expectedId
            };
            var procedure = new NormalStoredProcedure(parameters);
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

            // ACT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ExecuteStoredProcedure(procedure);
                connectionStillOpen = connection.State == ConnectionState.Open;
            }

            // ASSERT
            Assert.IsTrue(connectionStillOpen);
        }


        [TestMethod]
        public void NormalStoredProcedure_WhenCalledOnSqlConnection_ReturnsCorrectValues()
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