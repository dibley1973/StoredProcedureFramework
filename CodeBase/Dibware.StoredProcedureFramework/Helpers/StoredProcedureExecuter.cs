using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Resources;
using System;
using System.Collections;
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

        private readonly IDbConnection _connection;
        private readonly string _procedureName;
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
        /// Initializes a new instance of the <see cref="StoredProcedureExecuter{TResultSetType}"/> class.
        /// </summary>
        /// <param name="connection">The databse connection to execute the procedure against.</param>
        /// <param name="procedureName">Name of the procedure to execute.</param>
        /// <exception cref="System.ArgumentNullException">
        /// connection
        /// or
        /// procedureName
        /// </exception>
        public StoredProcedureExecuter(
            IDbConnection connection,
            string procedureName)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");

            _connection = connection;
            _procedureName = procedureName;
            _resultSetType = typeof(TResultSetType);
        }

        #endregion

        #region Dispose and Finalise

        /// <summary>
        /// Gets a value indicating whether this <see cref="StoredProcedureExecuter{TResultSetType}"/> 
        /// is disposed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disposed; otherwise, <c>false</c>.
        /// </value>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Finalizes an instance of the <see cref="StoredProcedureExecuter{TResultSetType}"/> class.
        /// </summary>
        ~StoredProcedureExecuter()
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
        public StoredProcedureExecuter<TResultSetType> Execute()
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

        public StoredProcedureExecuter<TResultSetType> WithCommandBehavior(CommandBehavior commandBehavior)
        {
            _commandBehavior = commandBehavior;
            return this;
        }

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
            _command = StoredProcedureDbCommandCreator
                .CreateStoredProcedureDbCommandCreator(_connection, _procedureName)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithoutParametersOrTransactionButWithCommandTimeout()
        {
            _command = StoredProcedureDbCommandCreator
                .CreateStoredProcedureDbCommandCreator(_connection, _procedureName)
                .WithCommandTimeout(_commandTimeoutOverride)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithoutParametersOrCommandTimeoutButWithTransaction()
        {
            _command = StoredProcedureDbCommandCreator
                .CreateStoredProcedureDbCommandCreator(_connection, _procedureName)
                .WithTransaction(_transaction)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithoutParametersButWithCommandTimeoutAndTransaction()
        {
            _command = StoredProcedureDbCommandCreator
                .CreateStoredProcedureDbCommandCreator(_connection, _procedureName)
                .WithCommandTimeout(_commandTimeoutOverride)
                .WithTransaction(_transaction)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithParametersButWithoutCommandTimeoutOrTransaction()
        {
            _command = StoredProcedureDbCommandCreator
                .CreateStoredProcedureDbCommandCreator(_connection, _procedureName)
                .WithParameters(_procedureParameters)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithParametersAndCommandTimeoutButWithoutTransaction()
        {
            _command = StoredProcedureDbCommandCreator
                .CreateStoredProcedureDbCommandCreator(_connection, _procedureName)
                .WithParameters(_procedureParameters)
                .WithCommandTimeout(_commandTimeoutOverride)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithParametersAndTransactionButWithoutCommandTimeout()
        {
            _command = StoredProcedureDbCommandCreator
                .CreateStoredProcedureDbCommandCreator(_connection, _procedureName)
                .WithParameters(_procedureParameters)
                .WithTransaction(_transaction)
                .BuildCommand()
                .Command;
        }

        private void CreateCommandWithParametersCommandTimeoutAndTransaction()
        {
            _command = StoredProcedureDbCommandCreator
                .CreateStoredProcedureDbCommandCreator(_connection, _procedureName)
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
                ExecuteCommandWithNoReturnType();
                return;
            }

            ExecuteCommandWithResultSet();
        }

        private void ExecuteCommandWithNoReturnType()
        {
            _command.ExecuteNonQuery();
        }

        private void ExecuteCommandWithResultSet()
        {
            if (HasSingleRecordSetOnly)
            {
                ExecuteCommandForSingleRecordSet();
            }
            else
            {
                ExecuteCommandForMultipleRecordSets();
            }
        }

        private void ExecuteCommandForMultipleRecordSets()
        {
            Results = new TResultSetType();
            var recordSetIndex = 0;
            var resultSetTypeProperties = _resultSetType.GetMappedProperties();

            using (IDataReader reader = _command.ExecuteReader(_commandBehavior))
            {
                bool readerContainsAnotherResult;
                do
                {
                    var recordSetDtoList = GetRecordSetDtoList(resultSetTypeProperties, recordSetIndex);
                    ReadRecordSetFromReader(reader, recordSetDtoList);

                    recordSetIndex += 1;
                    readerContainsAnotherResult = reader.NextResult();

                } while (readerContainsAnotherResult);
                reader.Close();
            }
        }

        private IList GetRecordSetDtoList(PropertyInfo[] resultSetTypePropertyInfos, int recordSetIndex)
        {
            var recordSetPropertyName = resultSetTypePropertyInfos[recordSetIndex].Name;
            var recordSetDtoList = GetRecordSetDtoList(recordSetPropertyName);
            EnsureRecorsetListIsInstantiated(recordSetDtoList, recordSetPropertyName);

            return recordSetDtoList;
        }

        private void ExecuteCommandForSingleRecordSet()
        {
            var recordSetDtoList = (IList)new TResultSetType();

            using (IDataReader reader = _command.ExecuteReader(_commandBehavior))
            {
                ReadRecordSetFromReader(reader, recordSetDtoList);
                reader.Close();
            }

            Results = (TResultSetType)recordSetDtoList;
        }

        private void ReadRecordSetFromReader(IDataReader reader, IList recordSetDtoList)
        {
            Type listItemType = recordSetDtoList.GetType().GetGenericArguments()[0];
            PropertyInfo[] listItemProperties = listItemType.GetMappedProperties();

            while (reader.Read())
            {
                AddRecordToResults(listItemType, recordSetDtoList, reader, listItemProperties);
            }
        }

        private IList GetRecordSetDtoList(string recordSetPropertyName)
        {
            PropertyInfo recordSetPropertyInfo = _resultSetType.GetProperty(recordSetPropertyName);
            IList recordSetDtoList = (IList)recordSetPropertyInfo.GetValue(Results);
            return recordSetDtoList;
        }

        private void EnsureRecorsetListIsInstantiated(
            IList dtoList,
            string listPropertyName)
        {
            if (dtoList != null) return;

            string errorMessage = string.Format(
                ExceptionMessages.RecordSetListNotInstatiated,
                _resultSetType.Name,
                listPropertyName);
            throw new NullReferenceException(errorMessage);
        }

        private void AddRecordToResults(
            Type outputType,
            IList results,
            IDataReader reader,
            PropertyInfo[] dtoListItemTypePropertyInfos)
        {
            var constructorInfo = (outputType).GetConstructor(Type.EmptyTypes);
            bool noConstructorDefined = (constructorInfo == null);
            if (noConstructorDefined) return;

            var item = Activator.CreateInstance(outputType);
            reader.ReadRecord(item, dtoListItemTypePropertyInfos);
            results.Add(item);
        }

        private void OpenClosedConnection()
        {
            if (!_connectionAlreadyOpen) _connection.Open();
        }

        private void RestoreOriginalConnectionState()
        {
            if (!_connectionAlreadyOpen) _connection.Close();
        }

        private bool HasSingleRecordSetOnly
        {
            get { return _resultSetType.ImplementsICollectionInterface(); }
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