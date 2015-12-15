using Dibware.Helpers.Validation;
using Dibware.StoredProcedureFramework.Helpers.Base;
using Dibware.StoredProcedureFramework.Helpers.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace Dibware.StoredProcedureFramework.Helpers
{
    /// <summary>
    /// Responsible for executing Sql Functions
    /// </summary>
    /// <remarks>
    /// TODO: This will need to be adapted to use a base class along with 
    /// <see cref="Dibware.StoredProcedureFramework.Helpers.StoredProcedureExecuter{TResultSetType}"/>
    /// </remarks>
    internal class SqlTableFunctionExecuter<TResultSetType>
        : SqlProgrammabilityObjectExecuterBase<TResultSetType>
        where TResultSetType : class, new()
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTableFunctionExecuter{TResultSetType}"/> class.
        /// </summary>
        /// <param name="connection">The databse connection to execute the Dql function against.</param>
        /// <param name="functionName">Name of the function to execute.</param>
        /// <exception cref="System.ArgumentNullException">
        /// connection
        /// or
        /// functionName
        /// </exception>
        public SqlTableFunctionExecuter(IDbConnection connection, string functionName)
            : base(
                Ensure<IDbConnection>.ArgumentIsNotNull(connection, "connection"),
                Ensure.ArgumentIsNotNullOrWhiteSpace(functionName, "functionName"))
        { }

        #endregion

        #region Dispose and Finalise

        ///// <summary>
        ///// Gets a value indicating whether this <see cref="SqlTableFunctionExecuter{TResultSetType}"/> 
        ///// is disposed.
        ///// </summary>
        ///// <value>
        /////   <c>true</c> if disposed; otherwise, <c>false</c>.
        ///// </value>
        //public bool Disposed { get; private set; }

        ///// <summary>
        ///// Finalizes an instance of the <see cref="SqlTableFunctionExecuter{TResultSetType}"/> class.
        ///// </summary>
        //~SqlTableFunctionExecuter()
        //{
        //    Dispose(false);
        //}

        ///// <summary>
        ///// Performs application-defined tasks associated with freeing, releasing, 
        ///// or resetting unmanaged resources.
        ///// </summary>
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        ///// <summary>
        ///// Releases unmanaged and - optionally - managed resources.
        ///// </summary>
        ///// <param name="disposing">
        ///// <c>true</c> to release both managed and unmanaged resources; 
        ///// <c>false</c> to release only unmanaged resources.
        ///// </param>
        //private void Dispose(bool disposing)
        //{
        //    if (!Disposed)
        //    {
        //        if (disposing)
        //        {
        //            DisposeCommand();
        //        }

        //        // There are no unmanaged resources to release, but
        //        // if we add them, they need to be released here.
        //    }
        //    Disposed = true;
        //}

        #endregion

        #region Public Members

        public new SqlTableFunctionExecuter<TResultSetType> WithCommandBehavior(CommandBehavior commandBehavior)
        {
            base.WithCommandBehavior(commandBehavior);
            return this;
        }

        public new SqlTableFunctionExecuter<TResultSetType> WithParameters(IEnumerable<SqlParameter> functionParameters)
        {
            if (functionParameters == null) throw new ArgumentNullException("functionParameters");

            base.WithParameters(functionParameters);
            return this;
        }

        public new SqlTableFunctionExecuter<TResultSetType> WithCommandTimeoutOverride(int commandTimeoutOverride)
        {
            base.WithCommandTimeoutOverride(commandTimeoutOverride);
            return this;
        }

        public new SqlTableFunctionExecuter<TResultSetType> WithTransaction(SqlTransaction transaction)
        {
            base.WithTransaction(transaction);
            return this;
        }

        #endregion

        #region Public Factory Methods

        /// <summary>
        /// Creates a new instance of the <see cref="SqlTableFunctionExecuter{TResultSetType}"/> class.
        /// </summary>
        /// <param name="connection">The databse connection to execute the procedure against.</param>
        /// <param name="procedureName">Name of the procedure to execute.</param>
        /// <exception cref="System.ArgumentNullException">
        /// connection
        /// or
        /// procedureName
        /// </exception>
        public static SqlTableFunctionExecuter<TResultSetType> CreateSqTablelFunctionExecuter(
            DbConnection connection,
            string procedureName)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");

            return new SqlTableFunctionExecuter<TResultSetType>(connection, procedureName);
        }

        #endregion

        #region Private Members

        private string FunctionName { get { return ProgrammabilityObjectName; } }

        protected override IDbCommandCreator CreateCommandCreator()
        {
            return SqlTableFunctionDbCommandCreator
                .CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);
        }

        //protected override void CreateCommand()
        //{
        //    DisposeCommand();

        //    IDbCommandCreator creator = SqlTableFunctionDbCommandCreator
        //        .CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);

        //    if (HasCommandTimeoutOverride)
        //    {
        //        creator.WithCommandTimeout(_commandTimeoutOverride);
        //    }

        //    if (HasParameters)
        //    {
        //        creator.WithParameters(_procedureParameters);
        //    }

        //    if (HasTransaction)
        //    {
        //        creator.WithTransaction(_transaction);
        //    }

        //    _command = creator
        //            .BuildCommand()
        //            .Command;
        //}

        //protected override void CreateCommandWithoutParametersOrCommandTimeoutOrTransaction()
        //{
        //    _command = SqlTableFunctionDbCommandCreator
        //        .CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName)
        //        .BuildCommand()
        //        .Command;
        //}

        //protected override void CreateCommandWithoutParametersOrTransactionButWithCommandTimeout()
        //{
        //    _command = SqlTableFunctionDbCommandCreator
        //        .CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName)
        //        .WithCommandTimeout(_commandTimeoutOverride)
        //        .BuildCommand()
        //        .Command;
        //}

        //protected override void CreateCommandWithoutParametersOrCommandTimeoutButWithTransaction()
        //{
        //    _command = SqlTableFunctionDbCommandCreator
        //        .CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName)
        //        .WithTransaction(_transaction)
        //        .BuildCommand()
        //        .Command;
        //}

        //protected override void CreateCommandWithoutParametersButWithCommandTimeoutAndTransaction()
        //{
        //    _command = SqlTableFunctionDbCommandCreator
        //        .CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName)
        //        .WithCommandTimeout(_commandTimeoutOverride)
        //        .WithTransaction(_transaction)
        //        .BuildCommand()
        //        .Command;
        //}

        //protected override void CreateCommandWithParametersButWithoutCommandTimeoutOrTransaction()
        //{
        //    _command = SqlTableFunctionDbCommandCreator
        //        .CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName)
        //        .WithParameters(_procedureParameters)
        //        .BuildCommand()
        //        .Command;
        //}

        //protected override void CreateCommandWithParametersAndCommandTimeoutButWithoutTransaction()
        //{
        //    _command = SqlTableFunctionDbCommandCreator
        //        .CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName)
        //        .WithParameters(_procedureParameters)
        //        .WithCommandTimeout(_commandTimeoutOverride)
        //        .BuildCommand()
        //        .Command;
        //}

        //protected override void CreateCommandWithParametersAndTransactionButWithoutCommandTimeout()
        //{
        //    _command = SqlTableFunctionDbCommandCreator
        //        .CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName)
        //        .WithParameters(_procedureParameters)
        //        .WithTransaction(_transaction)
        //        .BuildCommand()
        //        .Command;
        //}

        //protected override void CreateCommandWithParametersCommandTimeoutAndTransaction()
        //{
        //    _command = SqlTableFunctionDbCommandCreator
        //        .CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName)
        //        .WithParameters(_procedureParameters)
        //        .WithCommandTimeout(_commandTimeoutOverride)
        //        .WithTransaction(_transaction)
        //        .BuildCommand()
        //        .Command;
        //}

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
