using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Dibware.StoredProcedureFramework.IntegrationTests.TestBase
{
    [TestClass]
    public abstract class BaseSqlConnectionIntegrationTest
    {
        #region Fields

        private string _connectionString;
        private SqlConnection _connection;
        private TransactionScope _transaction;

        #endregion

        #region Properties

        protected SqlConnection Connection
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
            _connection.Open();
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
    }
}
