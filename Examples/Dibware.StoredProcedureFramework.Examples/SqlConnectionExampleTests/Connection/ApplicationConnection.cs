using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFrameworkForEF.Generic;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Connection
{
    /// <summary>
    /// 
    /// </summary>
    internal class ApplicationConnection : StoredProcedureSqlConnection
    {
        public ApplicationConnection(string connectionString) 
            : base(connectionString)
        {
        }

        #region Stored Procedures

        [Schema("app")]
        public StoredProcedure<List<TenantDto>> TenantGetAll { get; set; }
        [Schema("app")]
        public StoredProcedure<List<TenantDto>> TenantGetForId { get; set; }
        [Schema("app")]
        public StoredProcedure TenantDeleteForId { get; set; }
        [Schema("app")]
        public StoredProcedure TenantMarkAllInactive { get; set; }

        #endregion
    
    }


    ///// <summary>
    ///// 
    ///// </summary>
    //public class ApplicationConnection : DbConnection, IDisposable
    //{
    //    #region Fields

    //    private readonly SqlConnection _connection;

    //    #endregion

    //    #region Properties

    //    /// <summary>
    //    /// Gets the connection.
    //    /// </summary>
    //    /// <value>
    //    /// The connection.
    //    /// </value>
    //    protected SqlConnection Connection
    //    {
    //        get { return _connection; }
    //    }

    //    /// <summary>
    //    /// Gets a value indicating whether this instance is disposed.
    //    /// </summary>
    //    /// <value>
    //    /// <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
    //    /// </value>
    //    public bool IsDisposed { get; private set; }

    //    /// <summary>
    //    /// Gets a value indicating whether this instance is disposing.
    //    /// </summary>
    //    /// <value>
    //    /// <c>true</c> if this instance is disposing; otherwise, <c>false</c>.
    //    /// </value>
    //    public bool IsDisposing { get; private set; }

    //    #endregion

    //    #region construction and destruction

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="ApplicationConnection"/> class.
    //    /// </summary>
    //    /// <param name="connectionString">The connection string.</param>
    //    public ApplicationConnection(string connectionString)
    //    {
    //        _connection = new SqlConnection(connectionString);
    //    }

    //    /// <summary>
    //    /// Finalizes an instance of the <see cref="ApplicationConnection"/> class.
    //    /// </summary>
    //    ~ApplicationConnection()
    //    {
    //        Dispose(false);
    //    }

    //    /// <summary>
    //    /// Releases unmanaged and - optionally - managed resources.
    //    /// </summary>
    //    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    //    protected new virtual void Dispose(bool disposing)
    //    {
    //        if (IsDisposed) return;

    //        IsDisposing = true;
    //        if (disposing)
    //        {
    //            // free other managed objects that implement
    //            // IDisposable only
    //            if (_connection != null) _connection.Dispose();
    //        }

    //        // release any unmanaged objects
    //        // set the object references to null

    //        IsDisposed = true;
    //        IsDisposing = false;
    //    }

    //    #endregion

    //    #region IDisposable Members

    //    /// <summary>
    //    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    //    /// </summary>
    //    public new void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    #endregion

    //    #region DbConnection Methods

    //    protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    /// <summary>
    //    /// Changes the current database for an open connection.
    //    /// </summary>
    //    /// <param name="databaseName">Specifies the name of the database for the connection to use.</param>
    //    public override void ChangeDatabase(string databaseName)
    //    {
    //        if (Connection.State != ConnectionState.Open) throw new InvalidOperationException("Cannot change databse if conenction is not open");

    //        _connection.ChangeDatabase(databaseName);
    //    }

    //    /// <summary>
    //    /// Closes the connection to the database. This is the preferred method of closing any open connection.
    //    /// </summary>
    //    public override void Close()
    //    {
    //        if (Connection.State != ConnectionState.Closed) _connection.Close();
    //    }

    //    /// <summary>
    //    /// Gets or sets the string used to open the connection.
    //    /// </summary>
    //    public override string ConnectionString
    //    {
    //        get { return Connection.ConnectionString; }
    //        set { Connection.ConnectionString = value; }
    //    }

    //    /// <summary>
    //    /// Creates and returns a <see cref="T:System.Data.Common.DbCommand" /> object associated with the current connection.
    //    /// </summary>
    //    /// <returns>
    //    /// A <see cref="T:System.Data.Common.DbCommand" /> object.
    //    /// </returns>
    //    protected override DbCommand CreateDbCommand()
    //    {
    //        return Connection.CreateCommand();
    //    }

    //    /// <summary>
    //    /// Gets the name of the database server to which to connect.
    //    /// </summary>
    //    /// <exception cref="System.NotImplementedException"></exception>
    //    public override string DataSource
    //    {
    //        get { return Connection.DataSource; }
    //    }

    //    /// <summary>
    //    /// Gets the name of the current database after a connection is opened, 
    //    /// or the database name specified in the connection string before the connection is opened.
    //    /// </summary>
    //    public override string Database
    //    {
    //        get { return Connection.Database; }
    //    }

    //    /// <summary>
    //    /// Opens a database connection with the settings specified by the <see cref="P:System.Data.Common.DbConnection.ConnectionString" />.
    //    /// </summary>
    //    public override void Open()
    //    {
    //        Connection.Open();
    //    }

    //    /// <summary>
    //    /// Gets a string that represents the version of the server to which the object is connected.
    //    /// </summary>
    //    public override string ServerVersion
    //    {
    //        get { return Connection.ServerVersion; }
    //    }

    //    /// <summary>
    //    /// Gets a string that describes the state of the connection.
    //    /// </summary>
    //    public override ConnectionState State
    //    {
    //        get { return Connection.State; }
    //    }

    //    #endregion
    //}
}