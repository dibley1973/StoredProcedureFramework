using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Dibware.Helpers.Validation;
using Dibware.StoredProcedureFramework.Helpers.Base;
using Dibware.StoredProcedureFramework.Helpers.Contracts;

namespace Dibware.StoredProcedureFramework.Helpers
{
    /// <summary>
    /// Responsible for execetuting stored procedures
    /// </summary>
    /// <typeparam name="TResultSetType">
    /// The type of the result set type.
    /// </typeparam>
    internal class StoredProcedureExecuter<TResultSetType>
        : SqlProgrammabilityObjectExecuterBase<TResultSetType>
        where TResultSetType : class, new()
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureExecuter{TResultSetType}"/> class.
        /// </summary>
        /// <param name="connection">The databse connection to execute the procedure against.</param>
        /// <param name="procedureName">Name of the procedure to execute.</param>
        /// <exception cref="System.ArgumentNullException">
        /// connection
        /// or
        /// procedureName
        /// </exception>
        public StoredProcedureExecuter(IDbConnection connection, string procedureName)
            : base(
                Ensure<IDbConnection>.IsNotNull(connection, "connection"),
                Ensure.ArgumentIsNotNullOrWhiteSpace(procedureName, "procedureName"))
        { }

        #endregion

        #region Public Members

        public new StoredProcedureExecuter<TResultSetType> WithCommandBehavior(CommandBehavior commandBehavior)
        {
            base.WithCommandBehavior(commandBehavior);
            return this;
        }

        public new StoredProcedureExecuter<TResultSetType> WithParameters(IEnumerable<SqlParameter> procedureParameters)
        {
            if (procedureParameters == null) throw new ArgumentNullException("procedureParameters");

            base.WithParameters(procedureParameters);
            return this;
        }

        public new StoredProcedureExecuter<TResultSetType> WithCommandTimeoutOverride(int commandTimeoutOverride)
        {
            base.WithCommandTimeoutOverride(commandTimeoutOverride);
            return this;
        }

        public new StoredProcedureExecuter<TResultSetType> WithTransaction(SqlTransaction transaction)
        {
            base.WithTransaction(transaction);
            return this;
        }

        #endregion

        #region Public Factory Methods

        /// <summary>
        /// Creates a new instance of the <see cref="StoredProcedureExecuter{TResultSetType}"/> class.
        /// </summary>
        /// <param name="connection">The databse connection to execute the procedure against.</param>
        /// <param name="procedureName">Name of the procedure to execute.</param>
        /// <exception cref="System.ArgumentNullException">
        /// connection
        /// or
        /// procedureName
        /// </exception>
        public static StoredProcedureExecuter<TResultSetType> CreateStoredProcedureExecuter(
            DbConnection connection,
            string procedureName)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");

            return new StoredProcedureExecuter<TResultSetType>(connection, procedureName);
        }

        #endregion

        #region Private Members

        private string ProcedureName { get { return ProgrammabilityObjectName; } }

        protected override IDbCommandCreator CreateCommandCreator()
        {
            return StoredProcedureDbCommandCreator
                .CreateStoredProcedureDbCommandCreator(Connection, ProcedureName);
        }

        protected override void ExecuteCommand()
        {
            if (HasNoReturnType)
            {
                ExecuteCommandWithNoReturnType();
                return;
            }

            ExecuteCommandWithResultSet();
        }

        #endregion
    }
}