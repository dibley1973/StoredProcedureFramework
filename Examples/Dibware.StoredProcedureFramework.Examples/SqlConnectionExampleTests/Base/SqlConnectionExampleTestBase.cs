using System;
using System.Data.SqlClient;
using System.Transactions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Base
{
    [TestClass]
    public abstract class SqlConnectionExampleTestBase
    {
        #region Fields

        private SqlConnection _connection;
        private TransactionScope _transaction;

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
            string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
            _connection = new SqlConnection(connectionName);
            _connection.Open();
            _transaction = new TransactionScope(TransactionScopeOption.RequiresNew);
        }

        private void CleanupDatabase()
        {
            if (_transaction != null) _transaction.Dispose();
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        #endregion
    }
}