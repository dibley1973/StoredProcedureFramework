using System.Transactions;
using Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Context;
using Dibware.StoredProcedureFramework.Examples.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Base
{
    [TestClass]
    public abstract class DbContextExampleTestBase
    {
        #region Fields

        private ApplicationDbContext _context;
        private TransactionScope _transaction;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        internal ApplicationDbContext Context
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

        #region Database Initialization / Cleanup

        /// <summary>
        /// Prepares the database.
        /// </summary>
        private void PrepareDatabase()
        {
            string connectionName = Settings.Default.ExampleDatabaseConnection;
            _context = new ApplicationDbContext(connectionName);
            _context.Database.CreateIfNotExists();
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