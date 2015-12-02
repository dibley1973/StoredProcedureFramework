using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Helpers.Base
{
    public abstract class DbCommandCreatorBase
    {
        #region Constructor

        protected DbCommandCreatorBase(DbConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");


            _connection = connection;
        }

        protected DbCommandCreatorBase(DbConnection connection,
            IEnumerable<SqlParameter> parameters)
            : this(connection)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");


            _parameters = parameters;
        }


        #endregion


        #region Public Members

        public abstract void BuildCommand();

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

        #region Private Members

        private DbCommand _command;
        private DbConnection _connection;
        private IEnumerable<SqlParameter> _parameters;
        protected int? _commandTimeout = null;
        private SqlTransaction _transaction = null;

        #endregion
    }
}