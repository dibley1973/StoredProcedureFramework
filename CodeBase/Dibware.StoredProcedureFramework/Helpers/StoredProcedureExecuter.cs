using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public class StoredProcedureExecuter<TResultSetType>
            where TResultSetType : class, new()
    {
        #region Fields

        private DbConnection _connection;
        private string _procedureName;
        private IEnumerable<SqlParameter> _procedureParameters;
        private int? _commandTimeoutOverride;
        private CommandBehavior _commandBehavior;
        private SqlTransaction _transaction;

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

        //public StoredProcedureExecuter(
        //    DbConnection connection, 
        //    string procedureName, 
        //    IEnumerable<SqlParameter> procedureParameters, 
        //    int? commandTimeoutOverride, 
        //    CommandBehavior commandBehavior, 
        //    SqlTransaction transaction)
        //    : this(connection, pro)
        //{
        //    if(connection == null) throw new ArgumentNullException("connection");
        //    if(string.IsNullOrWhiteSpace(procedureName)) throw new ArgumentNullException("procedureName");


        //    _connection = connection;
        //    _procedureName = procedureName;
        //    _procedureParameters = procedureParameters;
        //    _commandTimeoutOverride = commandTimeoutOverride;
        //    _commandBehavior = commandBehavior;
        //    _transaction = transaction;
        //}

        #endregion

        #region Public Members

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

        #endregion
    }
}
