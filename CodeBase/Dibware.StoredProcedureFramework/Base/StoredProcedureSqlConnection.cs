using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Base
{
    /// <summary>
    /// Represents a base class that may be inherited for use with
    /// an application specific connection
    /// </summary>
    public abstract class StoredProcedureSqlConnection : DbConnection, IDisposable
    {
        #region Fields

        private readonly SqlConnection _connection;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        private SqlConnection Connection
        {
            get { return _connection; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        private bool IsDisposed { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is disposing.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is disposing; otherwise, <c>false</c>.
        /// </value>
        private bool IsDisposing { get; set; }

        #endregion

        #region construction and destruction

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureSqlConnection"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        protected StoredProcedureSqlConnection(string connectionString)
        {
            if (String.IsNullOrWhiteSpace(connectionString)) throw new ArgumentNullException("connectionString");

            _connection = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="StoredProcedureSqlConnection"/> class.
        /// </summary>
        ~StoredProcedureSqlConnection()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected new virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;

            if (disposing)
            {
                IsDisposing = true;

                // free other managed objects that implement
                // IDisposable only
                if (_connection != null) _connection.Dispose();
            }

            // release any unmanaged objects
            // set the object references to null

            IsDisposed = true;
            IsDisposing = false;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region DbConnection Methods

        /// <summary>
        /// Starts a database transaction.
        /// </summary>
        /// <param name="isolationLevel">Specifies the isolation level for the transaction.</param>
        /// <returns>
        /// An object representing the new transaction.
        /// </returns>
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            return _connection.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// Changes the current database for an open connection.
        /// </summary>
        /// <param name="databaseName">Specifies the name of the database for the connection to use.</param>
        public override void ChangeDatabase(string databaseName)
        {
            if (Connection.State != ConnectionState.Open) throw new InvalidOperationException("Cannot change databse if conenction is not open");

            _connection.ChangeDatabase(databaseName);
        }

        /// <summary>
        /// Closes the connection to the database. This is the preferred method of closing any open connection.
        /// </summary>
        public override void Close()
        {
            if (Connection.State != ConnectionState.Closed) _connection.Close();
        }

        /// <summary>
        /// Gets or sets the string used to open the connection.
        /// </summary>
        public override string ConnectionString
        {
            get { return Connection.ConnectionString; }
            set { Connection.ConnectionString = value; }
        }

        /// <summary>
        /// Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object associated with the current connection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Data.Common.DbCommand" /> object.
        /// </returns>
        protected override DbCommand CreateDbCommand()
        {
            return Connection.CreateCommand();
        }

        /// <summary>
        /// Gets the name of the database server to which to connect.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override string DataSource
        {
            get { return Connection.DataSource; }
        }

        /// <summary>
        /// Gets the name of the current database after a connection is opened, 
        /// or the database name specified in the connection string before the connection is opened.
        /// </summary>
        public override string Database
        {
            get { return Connection.Database; }
        }

        /// <summary>
        /// Opens a database connection with the settings specified by the <see cref="P:System.Data.Common.DbConnection.ConnectionString" />.
        /// </summary>
        public override void Open()
        {
            Connection.Open();
        }

        /// <summary>
        /// Gets a string that represents the version of the server to which the object is connected.
        /// </summary>
        public override string ServerVersion
        {
            get { return Connection.ServerVersion; }
        }

        /// <summary>
        /// Gets a string that describes the state of the connection.
        /// </summary>
        public override ConnectionState State
        {
            get { return Connection.State; }
        }

        #endregion
    }
}