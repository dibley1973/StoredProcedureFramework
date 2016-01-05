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
    /// Responsible for executing Sql Functions
    /// </summary>
    internal class SqlScalarFunctionExecuter<TResultSetType>
        : SqlProgrammabilityObjectExecuterBase<TResultSetType>
        where TResultSetType : new()
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlScalarFunctionExecuter{TResultSetType}"/> class.
        /// </summary>
        /// <param name="connection">The databse connection to execute the Dql function against.</param>
        /// <param name="functionName">Name of the function to execute.</param>
        /// <exception cref="System.ArgumentNullException">
        /// connection
        /// or
        /// functionName
        /// </exception>
        public SqlScalarFunctionExecuter(IDbConnection connection, string functionName)
            : base(
                Ensure<IDbConnection>.IsNotNull(connection, "connection"),
                Ensure.ArgumentIsNotNullOrWhiteSpace(functionName, "functionName"))
        { }

        #endregion

        #region Public Members

        public new SqlScalarFunctionExecuter<TResultSetType> WithCommandBehavior(CommandBehavior commandBehavior)
        {
            base.WithCommandBehavior(commandBehavior);
            return this;
        }

        public new SqlScalarFunctionExecuter<TResultSetType> WithParameters(IEnumerable<SqlParameter> procedureParameters)
        {
            if (procedureParameters == null) throw new ArgumentNullException("procedureParameters");

            base.WithParameters(procedureParameters);
            return this;
        }

        public new SqlScalarFunctionExecuter<TResultSetType> WithCommandTimeoutOverride(int commandTimeoutOverride)
        {
            base.WithCommandTimeoutOverride(commandTimeoutOverride);
            return this;
        }

        public new SqlScalarFunctionExecuter<TResultSetType> WithTransaction(SqlTransaction transaction)
        {
            base.WithTransaction(transaction);
            return this;
        }

        #endregion

        #region Public Factory Methods

        /// <summary>
        /// Creates a new instance of the <see cref="SqlScalarFunctionExecuter{TResultSetType}"/> class.
        /// </summary>
        /// <param name="connection">The databse connection to execute the procedure against.</param>
        /// <param name="procedureName">Name of the procedure to execute.</param>
        /// <exception cref="System.ArgumentNullException">
        /// connection
        /// or
        /// procedureName
        /// </exception>
        public static SqlScalarFunctionExecuter<TResultSetType> CreateSqlScalarFunctionExecuter(
            DbConnection connection,
            string procedureName)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");

            return new SqlScalarFunctionExecuter<TResultSetType>(connection, procedureName);
        }

        #endregion

        #region Private and Protected Members

        private string FunctionName { get { return ProgrammabilityObjectName; } }

        protected override IDbCommandCreator CreateCommandCreator()
        {
            return SqlScalarFunctionDbCommandCreator
                .CreateSqlScalarFunctionDbCommandCreator(Connection, FunctionName);
        }

        protected override void ExecuteCommand()
        {
            if (HasNoReturnType)
            {
                throw new InvalidOperationException("Scalar function must have a return type! ");
            }

            ExecuteScalarCommandForSingleRecordSet();
        }

        private void ExecuteScalarCommandForSingleRecordSet()
        {
            var result = Command.ExecuteScalar();
            if (result == DBNull.Value)
            {
                Results = default(TResultSetType);
            }
            else
            {
                Results = (TResultSetType) result;
            }
        }

        #endregion
    }
}