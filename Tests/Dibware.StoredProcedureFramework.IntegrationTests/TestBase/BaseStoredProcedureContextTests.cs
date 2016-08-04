using Dibware.StoredProcedureFramework.IntegrationTests.Properties;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedureContextTests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

namespace Dibware.StoredProcedureFramework.IntegrationTests.TestBase
{
    [TestClass]
    public abstract class BaseStoredProcedureContextTests
    {
        #region Fields

        private IntegrationTestStoredProcedureContext _context;
        private TransactionScope _transaction;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        internal IntegrationTestStoredProcedureContext Context
        {
            get { return _context; }
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

        #region Test Initialization / Cleanup

        /// <summary>
        /// Prepares the database.
        /// </summary>
        private void PrepareDatabase()
        {
            string connectionName = Settings.Default.IntegrationTestConnection;
            _context = new IntegrationTestStoredProcedureContext(connectionName);
            _transaction = new TransactionScope(TransactionScopeOption.RequiresNew);
        }

        private void CleanupDatabase()
        {
            if (_transaction != null) _transaction.Dispose();
            _context.Dispose();
        }

        #endregion
    }
}