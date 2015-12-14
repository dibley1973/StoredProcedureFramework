using Dibware.StoredProcedureFramework.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Helpers
{
    /// <summary>
    /// Responsible for executing Sql Functions
    /// </summary>
    /// <remarks>
    /// TODO: This will need to be adpated to use a base class alog with 
    /// <see cref="Dibware.StoredProcedureFramework.Helpers.StoredProcedureExecuter{TResultSetType}"/>
    /// </remarks>
    public class SqlScalarFunctionExecuter<TResultSetType>
        : IDisposable
        where TResultSetType : new()
    {
        #region Fields

        private readonly IDbConnection _connection;
        private readonly string _functionName;
        private readonly Type _resultSetType;
        private bool _connectionAlreadyOpen;
        private IEnumerable<SqlParameter> _procedureParameters;
        private int? _commandTimeoutOverride;
        private CommandBehavior _commandBehavior;
        private SqlTransaction _transaction;
        private IDbCommand _command;

        #endregion

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
        public SqlScalarFunctionExecuter(
            IDbConnection connection,
            string functionName)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(functionName)) throw new ArgumentNullException("functionName");

            _connection = connection;
            _functionName = functionName;
            _resultSetType = typeof(TResultSetType);
        }

        #endregion

        #region Dispose and Finalise

        /// <summary>
        /// Gets a value indicating whether this <see cref="SqlScalarFunctionExecuter{TResultSetType}"/> 
        /// is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disposed; otherwise, <c>false</c>.
        /// </value>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Finalizes an instance of the <see cref="SqlScalarFunctionExecuter{TResultSetType}"/> class.
        /// </summary>
        ~SqlScalarFunctionExecuter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, 
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; 
        /// <c>false</c> to release only unmanaged resources.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    DisposeCommand();
                }

                // There are no unmanaged resources to release, but
                // if we add them, they need to be released here.
            }
            Disposed = true;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ObjectDisposedException">
        /// Cannot call Execute when this object is disposed</exception>
        public SqlScalarFunctionExecuter<TResultSetType> Execute()
        {
            if (Disposed) throw new ObjectDisposedException("Cannot call Execute when this object is disposed");

            CacheOriginalConnectionState();

            try
            {
                OpenClosedConnection();
                CreateCommand();
                ExecuteCommand();
            }
            catch (Exception ex)
            {
                AddMoreInformativeInformationToExecuteError(ref ex);
                throw;
            }
            finally
            {
                DisposeCommand();
                RestoreOriginalConnectionState();
            }
            return this;
        }

        /// <summary>
        /// Gets the results of the stored procedure call.
        /// </summary>
        /// <value>
        /// The results of the stored procedure call.
        /// </value>
        public TResultSetType Results { get; private set; }

        public SqlScalarFunctionExecuter<TResultSetType> WithCommandBehavior(CommandBehavior commandBehavior)
        {
            _commandBehavior = commandBehavior;
            return this;
        }

        public SqlScalarFunctionExecuter<TResultSetType> WithParameters(IEnumerable<SqlParameter> procedureParameters)
        {
            if (procedureParameters == null) throw new ArgumentNullException("procedureParameters");

            _procedureParameters = procedureParameters;

            return this;
        }

        public SqlScalarFunctionExecuter<TResultSetType> WithCommandTimeoutOverride(int commandTimeoutOverride)
        {
            _commandTimeoutOverride = commandTimeoutOverride;

            return this;
        }

        public SqlScalarFunctionExecuter<TResultSetType> WithTransaction(SqlTransaction transaction)
        {
            _transaction = transaction;

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
        public static SqlScalarFunctionExecuter<TResultSetType> CreateSqlFunctionExecuter(
            DbConnection connection,
            string procedureName)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");

            return new SqlScalarFunctionExecuter<TResultSetType>(connection, procedureName);
        }

        #endregion

        #region Private Members

        private void AddMoreInformativeInformationToExecuteError(ref Exception ex)
        {
            var detailedMessage = string.Format(
                ExceptionMessages.ErrorReadingStoredProcedure,
                _functionName,
                ex.Message);
            Type exceptionType = ex.GetType();
            var fieldInfo = exceptionType.GetField("_message", BindingFlags.Instance | BindingFlags.NonPublic);

            if (fieldInfo != null) fieldInfo.SetValue(ex, detailedMessage);
        }

        private void CacheOriginalConnectionState()
        {
            _connectionAlreadyOpen = (_connection.State == ConnectionState.Open);
        }

        private void CreateCommand()
        {
            DisposeCommand();

            if (!HasCommandTimeoutOverride && !HasParameters && !HasTransaction)
            {
                CreateCommandWithoutParametersOrCommandTimeoutOrTransaction();
            }
            else if (HasCommandTimeoutOverride && !HasParameters && !HasTransaction)
            {
                CreateCommandWithoutParametersOrTransactionButWithCommandTimeout();
            }
            else if (!HasCommandTimeoutOverride && !HasParameters && HasTransaction)
            {
                CreateCommandWithoutParametersOrCommandTimeoutButWithTransaction();
            }
            else if (HasCommandTimeoutOverride && !HasParameters && HasTransaction)
            {
                CreateCommandWithoutParametersButWithCommandTimeoutAndTransaction();
            }
            else if (!HasCommandTimeoutOverride && HasParameters && !HasTransaction)
            {
                CreateCommandWithParametersButWithoutCommandTimeoutOrTransaction();
            }
            else if (HasCommandTimeoutOverride && HasParameters & !HasTransaction)
            {
                CreateCommandWithParametersAndCommandTimeoutButWithoutTransaction();
            }
            else if (!HasCommandTimeoutOverride && HasParameters & HasTransaction)
            {
                CreateCommandWithParametersAndTransactionButWithoutCommandTimeout();
            }
            else if (HasCommandTimeoutOverride && HasParameters && HasTransaction)
            {
                CreateCommandWithParametersCommandTimeoutAndTransaction();
            }
            else
            {
                throw new InvalidOperationException("An invalid combination of command attributes have been set!");
            }
        }

        private void CreateCommandWithoutParametersOrCommandTimeoutOrTransaction()
        {
            _command = SqlScalarFunctionDbCommandCreator
                .CreateSqlFunctionDbCommandCreator(_connection, _functionName)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithoutParametersOrTransactionButWithCommandTimeout()
        {
            _command = SqlScalarFunctionDbCommandCreator
                .CreateSqlFunctionDbCommandCreator(_connection, _functionName)
                .WithCommandTimeout(_commandTimeoutOverride)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithoutParametersOrCommandTimeoutButWithTransaction()
        {
            _command = SqlScalarFunctionDbCommandCreator
                .CreateSqlFunctionDbCommandCreator(_connection, _functionName)
                .WithTransaction(_transaction)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithoutParametersButWithCommandTimeoutAndTransaction()
        {
            _command = SqlScalarFunctionDbCommandCreator
                .CreateSqlFunctionDbCommandCreator(_connection, _functionName)
                .WithCommandTimeout(_commandTimeoutOverride)
                .WithTransaction(_transaction)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithParametersButWithoutCommandTimeoutOrTransaction()
        {
            _command = SqlScalarFunctionDbCommandCreator
                .CreateSqlFunctionDbCommandCreator(_connection, _functionName)
                .WithParameters(_procedureParameters)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithParametersAndCommandTimeoutButWithoutTransaction()
        {
            _command = SqlScalarFunctionDbCommandCreator
                .CreateSqlFunctionDbCommandCreator(_connection, _functionName)
                .WithParameters(_procedureParameters)
                .WithCommandTimeout(_commandTimeoutOverride)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithParametersAndTransactionButWithoutCommandTimeout()
        {
            _command = SqlScalarFunctionDbCommandCreator
                .CreateSqlFunctionDbCommandCreator(_connection, _functionName)
                .WithParameters(_procedureParameters)
                .WithTransaction(_transaction)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithParametersCommandTimeoutAndTransaction()
        {
            _command = SqlScalarFunctionDbCommandCreator
                .CreateSqlFunctionDbCommandCreator(_connection, _functionName)
                .WithParameters(_procedureParameters)
                .WithCommandTimeout(_commandTimeoutOverride)
                .WithTransaction(_transaction)
                .BuildCommand()
                .Command;
        }

        private void DisposeCommand()
        {
            if (_command != null)
            {
                _command.Dispose();
                _command = null;
            }
        }

        private void ExecuteCommand()
        {
            if (HasNoReturnType)
            {
                throw new InvalidOperationException("Scalar function must have a return type! ");
            }

            ExecuteScalarCommandForSingleRecordSet();
        }

        private void ExecuteScalarCommandForSingleRecordSet()
        {
            Results = (TResultSetType)_command.ExecuteScalar();
        }

        private void OpenClosedConnection()
        {
            if (!_connectionAlreadyOpen) _connection.Open();
        }

        private void RestoreOriginalConnectionState()
        {
            if (!_connectionAlreadyOpen) _connection.Close();
        }

        private bool HasCommandTimeoutOverride
        {
            get { return _commandTimeoutOverride.HasValue; }
        }

        private static bool HasNoReturnType
        {
            get { return (typeof(TResultSetType) == typeof(NullStoredProcedureResult)); }
        }

        private bool HasParameters
        {
            get { return _procedureParameters != null; }
        }

        private bool HasTransaction
        {
            get { return _transaction != null; }
        }

        #endregion
    }
}
