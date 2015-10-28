using System.Data.Entity;
using System.Transactions;
using Dibware.StoredProcedureFramework.Tests.Context;
using Dibware.StoredProcedureFramework.Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base
{
    [TestClass]
    public abstract class BaseIntegrationTestWithDbContext
    {
        #region Fields

        private IntegrationTestContext _context;
        private TransactionScope _transaction;
        //private const bool CreateTransaction = true;

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
            _context = new IntegrationTestContext("IntegrationTestConnection");
            _context.Database.CreateIfNotExists();
            ClearDownAllTables();
            //if (CreateTransaction)
            //{
            _transaction = new TransactionScope(TransactionScopeOption.RequiresNew);
            //}
        }

        private void CleanupDatabase()
        {
            //if (CreateTransaction)
            //{
            if (_transaction != null) _transaction.Dispose();
            //}
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
            TruncateTable(SchemaNames.App, TableNames.Company);
            DeleteAllFromTable(SchemaNames.App, TableNames.Tenant);
        }

        private void DeleteAllFromTable(string tableName)
        {
            string statement =
                string.Format(SqlStatements.DeleteAllFromTable,
                tableName);
            ExecuteStatement(statement);
        }

        private void DeleteAllFromTable(string schema, string tableName)
        {
            string statement =
                string.Format(SqlStatements.DeleteAllFromTableWithSchema,
                schema,
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

        private void TruncateTable(string schema, string tableName)
        {
            string statement =
                string.Format(SqlStatements.TruncateTableWithSchema,
                schema,
                tableName);
            ExecuteStatement(statement);
        }

        #endregion
    }
}