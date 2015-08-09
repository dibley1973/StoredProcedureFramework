using Dibware.StoredProcedureFramework.Tests.Context;
using Dibware.StoredProcedureFramework.Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Transactions;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.Base
{
    [TestClass]
    public abstract class BaseIntegrationTest
    {
        #region Fields

        private IntegrationTestContext _context;
        private TransactionScope _transaction;

        #endregion

        #region Test Initialization / Cleanup

        /// <summary>
        /// Prepares the database.
        /// </summary>
        public void PrepareDatabase()
        {
            _context = new IntegrationTestContext("IntegrationTestConnection");
            _context.Database.CreateIfNotExists();
            ClearDownAllTables();
            _transaction = new TransactionScope(TransactionScopeOption.RequiresNew);

        }

        protected void CleanupDatabase()
        {
            if (_transaction != null) _transaction.Dispose();
            _context.Dispose();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        internal IntegrationTestContext Context
        {
            get { return _context; }
        }

        #endregion

        #region Methods

        private void ClearDownAllTables()
        {
            TruncateTable(TableNames.Companies);
            DeleteAllFromTable(TableNames.Tenants);
        }

        private void DeleteAllFromTable(string tableName)
        {
            string statement =
                string.Format(SqlStatements.DeleteAllFromTable,
                tableName);
            ExecuteStatement(statement);
        }

        private void ExecuteStatement(string statement)
        {
            Context.Database.ExecuteSqlCommand(
                TransactionalBehavior.DoNotEnsureTransaction,
                statement);
        }

        private void TruncateTable(string tableName)
        {
            string statement =
                string.Format(SqlStatements.TruncateTable,
                tableName);
            ExecuteStatement(statement);
        }

        #endregion
    }
}