using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Helpers.Contracts;

namespace Dibware.StoredProcedureFramework.Helpers.Base
{
    internal abstract class DbCommandCreatorBase
        : IDbCommandCreator
    {
        #region Fields

        private const int DefaultCommandTimeout = 30;
        private IDbCommand _command;
        private readonly IDbConnection _connection;
        private IEnumerable<SqlParameter> _parameters;
        private string _commandText;
        private int _commandTimeout = DefaultCommandTimeout;
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
        protected DbCommandCreatorBase(IDbConnection connection)
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
        public IDbCommandCreator BuildCommand()
        {
            CreateCommand();
            LoadCommandParametersIfAnyExist();
            SetCommandTextForCommand();
            SetCommandTypeForCommand();
            SetCommandTimeoutIfExistsForCommand();
            SetTransactionIfExistsForCommand();

            return this;
        }

        /// <summary>
        /// Gets the command or null if it has not been built.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public IDbCommand Command
        {
            get { return _command; }
        }

        protected string CommandText
        {
            get { return _commandText; }
        }

        protected IEnumerable<SqlParameter> Parameters
        {
            get { return _parameters; }
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

        protected bool HasParameters
        {
            get { return _parameters != null; }
        }

        private void LoadCommandParametersIfAnyExist()
        {
            if (HasParameters)
            {
                ClearAnyExistingParameters();
                AddParametersToCommand();
            }
        }

        protected virtual void SetCommandTextForCommand()
        {
            _command.CommandText = _commandText;
        }

        protected void SetCommandTypeForCommand()
        {
            _command.CommandType = _commandType;
        }

        protected void SetCommandTimeoutIfExistsForCommand()
        {
            _command.CommandTimeout = _commandTimeout;
        }

        protected void SetTransactionIfExistsForCommand()
        {
            bool hasTransaction = _transaction != null;
            if (hasTransaction) _command.Transaction = _transaction;
        }

        protected void WithCommandText(string commandText)
        {
            _commandText = commandText;
        }

        public void WithCommandTimeout(int commandTimeout)
        {
            _commandTimeout = commandTimeout;
        }

        protected void WithCommandType(CommandType commandType)
        {
            _commandType = commandType;
        }

        public void WithParameters(IEnumerable<SqlParameter> parameters)
        {
            _parameters = parameters;
        }

        public void WithTransaction(SqlTransaction transaction)
        {
            _transaction = transaction;
        }

        #endregion
    }
}