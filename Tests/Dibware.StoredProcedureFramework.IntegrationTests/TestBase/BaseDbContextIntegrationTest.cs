﻿using Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests.Context;
using Dibware.StoredProcedureFramework.IntegrationTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;

namespace Dibware.StoredProcedureFramework.IntegrationTests.TestBase
{
    [TestClass]
    public abstract class BaseDbContextIntegrationTest
    {
        #region Fields

        private IntegrationTestDbContext _context;
        private TransactionScope _transaction;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        internal IntegrationTestDbContext Context
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
            _context = new IntegrationTestDbContext(connectionName);
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