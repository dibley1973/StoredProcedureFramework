using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class ConnectionStateTests
    {
        #region Fields

        private string _connectionString;
        private SqlConnection _connection;
        private TransactionScope _transaction;

        #endregion

        #region Properties

        private SqlConnection Connection
        {
            get { return _connection; }
        }

        #endregion

        #region Test Pre and Clear down

        [TestInitialize]
        public void TestSetup()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
            _connection = new SqlConnection(_connectionString);
            _transaction = new TransactionScope(TransactionScopeOption.RequiresNew);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }
                _connection.Dispose();
            }
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
        }

        #endregion

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