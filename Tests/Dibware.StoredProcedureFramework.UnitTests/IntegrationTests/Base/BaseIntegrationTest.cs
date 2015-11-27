using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base
{
    //[TestClass]
    //public abstract class BaseIntegrationTest
    //{
    //    #region Fields

    //    private string _connectionString;
    //    private SqlConnection _connection;
    //    private TransactionScope _transaction;

    //    #endregion

    //    #region Properties

    //    protected SqlConnection Connection
    //    {
    //        get { return _connection; }
    //    }

    //    #endregion

    //    #region Test Pre and Clear down

    //    [TestInitialize]
    //    public void TestSetup()
    //    {
    //        _connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
    //        _connection = new SqlConnection(_connectionString);
    //        _transaction = new TransactionScope(TransactionScopeOption.RequiresNew);
    //    }

    //    [TestCleanup]
    //    public void TestCleanup()
    //    {
    //        if (_connection != null)
    //        {
    //            if (_connection.State != ConnectionState.Closed)
    //            {
    //                _connection.Close();
    //            }
    //            _connection.Dispose();
    //        }
    //        if (_transaction != null)
    //        {
    //            _transaction.Dispose();
    //        }
    //    }

    //    #endregion
    //}
}