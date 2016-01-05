using System.Data.SqlClient;
using System.Transactions;
using Dibware.StoredProcedureFramework.Examples.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Base
{
    [TestClass]
    public abstract class SqlConnectionExampleTestBase
    {
        #region Fields

        private SqlConnection _connection;
        private TransactionScope _transactionScope;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        internal SqlConnection Connection
        {
            get { return _connection; }
        }

        #endregion

        #region Test Pre and Clear down

        [TestInitialize]
        public void TestSetup()
        {
            PrepareDatabase();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            CleanupDatabase();
        }

        #endregion

        #region Database Initialization / Cleanup

        /// <summary>
        /// Prepares the database.
        /// </summary>
        private void PrepareDatabase()
        {
            _transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew);
            string connectionName = Settings.Default.ExampleDatabaseConnection;
            _connection = new SqlConnection(connectionName);
            _connection.Open();
        }

        /// <summary>
        /// Cleanups the database.
        /// </summary>
        private void CleanupDatabase()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
            if (_transactionScope != null) _transactionScope.Dispose();
        }

        #endregion
    }
}