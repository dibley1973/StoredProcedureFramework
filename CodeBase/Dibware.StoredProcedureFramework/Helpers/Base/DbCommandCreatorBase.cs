using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Helpers.Base
{
    public abstract class DbCommandCreatorBase
    {
        #region Fields

        private DbCommand _command;
        private readonly DbConnection _connection;
        private IEnumerable<SqlParameter> _parameters;
        private string _commandText;
        private int? _commandTimeout;
        private SqlTransaction _transaction;
        private CommandType _commandType;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandCreatorBase"/> class.
        /// </summary>
        /// <param name="connection">
        /// The DbConnection to run the command against.
        /// </param>
        /// <exception cref="System.ArgumentNullException">connection</exception>
        protected DbCommandCreatorBase(DbConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            _connection = connection;
        }

        #endregion

        #region Public Members

        /// <summary>
        /// Builds and sets up the command based upon the settings that have 
        /// been previously passed to this builder.
        /// </summary>
        protected void BuildCommand()
        {
            CreateCommand();
            LoadCommandParametersIfAnyExist();
            SetCommandText();
            SetCommandType();
            SetCommandTimeoutIfExists();
            SetTransactionIfExists();
        }

        /// <summary>
        /// Gets the command or null if it has not been built.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public DbCommand Command
        {
            get { return _command; }
        }

        #endregion

        #region Private and Protected Members

        private void AddParametersToCommand()
        {
            foreach (SqlParameter parameter in _parameters)
            {
                _command.Parameters.Add(parameter);
            }
        }

        private void ClearAnyExistingParameters()
        {
            bool parametersRequireClearing = (_command.Parameters.Count > 0);
            if (parametersRequireClearing)
            {
                _command.Parameters.Clear();
            }
        }

        private void CreateCommand()
        {
            _command = _connection.CreateCommand();
        }

        private bool HasParameters
        {
            get { return _parameters != null; }
        }

        protected void LoadCommandParametersIfAnyExist()
        {
            if (HasParameters)
            {
                ClearAnyExistingParameters();
                AddParametersToCommand();
            }
        }

        private void SetCommandText()
        {
            _command.CommandText = _commandText;
        }

        private void SetCommandType()
        {
            _command.CommandType = _commandType;
        }

        private void SetCommandTimeoutIfExists()
        {
            bool hasCommandTimeout = _commandTimeout.HasValue;
            if (hasCommandTimeout)
            {
                _command.CommandTimeout = _commandTimeout.Value;
            }
        }

        private void SetTransactionIfExists()
        {
            bool hasTransaction = _transaction != null;
            if (hasTransaction) _command.Transaction = _transaction;
        }

        protected void WithCommandText(string commandText)
        {
            _commandText = commandText;
        }

        protected void WithCommandTimeout(int commandTimeout)
        {
            _commandTimeout = commandTimeout;
        }

        protected void WithCommandType(CommandType commandType)
        {
            _commandType = commandType;
        }

        protected void WithParameters(IEnumerable<SqlParameter> parameters)
        {
            _parameters = parameters;
        }

        protected void WithTransaction(SqlTransaction transaction)
        {
            _transaction = transaction;
        }

        #endregion
    }
}