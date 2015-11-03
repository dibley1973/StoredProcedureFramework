using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class SqlConnectionTests : BaseIntegrationTest
    {
        [TestMethod]
        public void ExecuteStoredProcedure_WhenNotAlreadyOpened_ClosesConnection()
        {
            // ARRANGE  
            const int expectedId = 10;

            var parameters = new NormalStoredProcedureParameters
            {
                Id = expectedId
            };
            var procedure = new NormalStoredProcedure(parameters);

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

            var parameters = new NormalStoredProcedureParameters
            {
                Id = expectedId
            };
            var procedure = new NormalStoredProcedure(parameters);

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

            var parameters = new NormalStoredProcedureParameters
            {
                Id = expectedId
            };
            var procedure = new NormalStoredProcedure(parameters);

            // ACT
            Connection.Open();
            //var resultSet = Connection.ExecuteStoredProcedure(procedure);
            var resultList = Connection.ExecuteStoredProcedure(procedure);
            Connection.Close();

            //var results = resultSet.RecordSet1;
            //var result = results.First();
            var result = resultList.First();
            //var result = resultSet;

            // ASSERT
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(expectedActive, result.Active);
        }
    }
}