using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Linq;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class ConnectionStateTests : BaseIntegrationTest
    {
        [TestMethod]
        public void ExecuteStoredProcedure_WhenNotAlreadyOpened_ClosesConnection()
        {
            // ARRANGE  
            const int expectedId = 10;
            var parameters = new ParametersAndReturnTypeStoredProcedure.Parameter()
            {
                Id = expectedId
            };
            var procedure = new ParametersAndReturnTypeStoredProcedure(parameters);

            // ACT
            Connection.ExecuteStoredProcedure(procedure);
            var connectionStillOpen = Connection.State == ConnectionState.Open;

            // ASSERT
            Assert.IsFalse(connectionStillOpen);
        }

        [TestMethod]
        public void ExecuteStoredProcedure_WhenAlreadyOpened_KeepsConnectionOpen()
        {
            // ARRANGE  
            const int expectedId = 10;
            var parameters = new ParametersAndReturnTypeStoredProcedure.Parameter()
            {
                Id = expectedId
            };
            var procedure = new ParametersAndReturnTypeStoredProcedure(parameters);

            // ACT
            Connection.Open();
            Connection.ExecuteStoredProcedure(procedure);
            var connectionStillOpen = Connection.State == ConnectionState.Open;
            Connection.Close();

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
            var parameters = new ParametersAndReturnTypeStoredProcedure.Parameter()
            {
                Id = expectedId
            };
            var procedure = new ParametersAndReturnTypeStoredProcedure(parameters);

            // ACT
            Connection.Open();
            var resultList = Connection.ExecuteStoredProcedure(procedure);
            Connection.Close();

            var result = resultList.First();

            // ASSERT
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(expectedActive, result.Active);
        }
    }
}