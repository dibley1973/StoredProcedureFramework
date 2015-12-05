using Dibware.StoredProcedureFramework.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public class StoredProcedureExecuter<TResultSetType> : IDisposable
            where TResultSetType : class, new()
    {
        #region Fields

        private DbConnection _connection;
        private string _procedureName;
        private IEnumerable<SqlParameter> _procedureParameters;
        private int? _commandTimeoutOverride;
        private CommandBehavior _commandBehavior;
        private SqlTransaction _transaction;
        private bool _connectionWasOpen;
        private DbCommand _command;

        #endregion

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
        private StoredProcedureExecuter(
            DbConnection connection,
            string procedureName)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");

            _connection = connection;
            _procedureName = procedureName;
        }

        #endregion

        #region Dispose and Finalise

        public bool Disposed { get; private set; }

        /// <summary>
        /// Finalizes an instance of the <see cref="StoredProcedureExecuter{TResultSetType}"/> class.
        /// </summary>
        ~StoredProcedureExecuter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
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
        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    if (_command != null)
                    {
                        _command.Dispose();
                    }
                }

                // There are no unmanaged resources to release, but
                // if we add them, they need to be released here.
            }
            Disposed = true;
        }

        #endregion

        #region Public Members

        public void Execute()
        {
            if (Disposed) throw new ObjectDisposedException("Cannot call Execute when this object is disposed");

            CacheOriginalConnectionState();

            try
            {
                OpenClosedConnection();
                CreateCommand();
                CreateAndExecuteCommand();
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
        }

        public TResultSetType Results { get; private set; }

        public StoredProcedureExecuter<TResultSetType> WithParameters(IEnumerable<SqlParameter> procedureParameters)
        {
            if (procedureParameters == null) throw new ArgumentNullException("procedureParameters");

            _procedureParameters = procedureParameters;

            return this;
        }

        public StoredProcedureExecuter<TResultSetType> WithCommandTimeoutOverride(int commandTimeoutOverride)
        {
            _commandTimeoutOverride = commandTimeoutOverride;

            return this;
        }

        public StoredProcedureExecuter<TResultSetType> WithTransaction(SqlTransaction transaction)
        {
            _transaction = transaction;

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

        private void AddMoreInformativeInformationToExecuteError(ref Exception ex)
        {
            var detailedMessage = string.Format(
                ExceptionMessages.ErrorReadingStoredProcedure,
                _procedureName,
                ex.Message);
            Type exceptionType = ex.GetType();
            var fieldInfo = exceptionType.GetField("_message", BindingFlags.Instance | BindingFlags.NonPublic);

            if (fieldInfo != null) fieldInfo.SetValue(ex, detailedMessage);
        }

        private void CreateAndExecuteCommand()
        {
            //// Create a command to execute the stored storedProcedure...
            //using (DbCommand command = _connection.CreateStoredProcedureCommand(
            //    _procedureName,
            //    _procedureParameters,
            //    _commandTimeoutOverride,
            //    _transaction))
            //{
            //    Results = ExecuteCommand<TResultSetType>(_commandBehavior, command);
            //}
        }



        private void CreateCommand()
        {
            if (_command != null)
            {
                DisposeCommand();
                _command = null;
            }

            //_command = DbCommandFactory.CreateStoredProcedureCommand()


            throw new NotImplementedException();
        }

        private void DisposeCommand()
        {
            if (_command != null)
            {
                _command.Dispose();
            }
        }

        private void CacheOriginalConnectionState()
        {
            _connectionWasOpen = (_connection.State == ConnectionState.Open);
        }

        private void ExecuteCommand()
        {
            var procedureHasNoReturnType = (typeof(TResultSetType) == typeof(NullStoredProcedureResult));

            //var _Results = procedureHasNoReturnType
            //    ? ExecuteCommandWithNoReturnType<TResultSetType>(_command)
            //    : ExecuteCommandWithResultSet<TResultSetType>(_commandBehavior, _command);
        }


        //private static TResultSetType ExecuteCommand(
        //    CommandBehavior commandBehavior,
        //    DbCommand command)
        //{
        //    var procedureHasNoReturnType =
        //        (typeof(TResultSetType) == typeof(NullStoredProcedureResult));

        //    var results = procedureHasNoReturnType
        //        ? ExecuteCommandWithNoReturnType<TResultSetType>(command)
        //        : ExecuteCommandWithResultSet<TResultSetType>(commandBehavior, command);

        //    return results;
        //}


        private void OpenClosedConnection()
        {
            if (!_connectionWasOpen) _connection.Open();
        }

        private void RestoreOriginalConnectionState()
        {
            if (!_connectionWasOpen) _connection.Close(); // Close connection if it arrived closed
        }

        #endregion
    }
}