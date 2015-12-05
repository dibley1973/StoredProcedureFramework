using System;
using System.Data;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class StoredProcedureExecuterTests
    {
        #region Fields

        const string StoredProcedureName = "DummyProcedure";
        const string ConnectionString = "Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";
        SqlConnection _connection;

        #endregion

        #region Properties

        private SqlConnection Connection
        {
            get { return _connection; }
        }

        #endregion

        #region Test Pre and Clear down

        [TestInitialize]
        public void TestSetup()
        {
            _connection = new SqlConnection(ConnectionString);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Closed)
                {
                    _connection.Close();
                }
                _connection.Dispose();
            }
        }

        #endregion

        #region Tests

        #region Construction

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenCalledWithNulllConnection_ThrowsException()
        {
            // ACT
            StoredProcedureExecuter<TestResultSet>.CreateStoredProcedureExecuter(null, StoredProcedureName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenCalledWithNulllProcedureName_ThrowsException()
        {
            // ACT
            StoredProcedureExecuter<TestResultSet>.CreateStoredProcedureExecuter(Connection, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenCalledWithEmptyProcedureName_ThrowsException()
        {
            // ACT
            StoredProcedureExecuter<TestResultSet>.CreateStoredProcedureExecuter(Connection, string.Empty);
        }

        [TestMethod]
        public void Constructor_WhenCalledWithValidconnectionAndProcedureName_ReturnsConstructedInstance()
        {
            // ACT
            var actualStoredProcedureExecuter = StoredProcedureExecuter<TestResultSet>.CreateStoredProcedureExecuter(Connection, StoredProcedureName);

            // ASSERT
            Assert.IsNotNull(actualStoredProcedureExecuter);
        }

        #endregion

        #region Execute
        
        #endregion

        #endregion

        #region Private Classes

        private class TestResultSet
        {
            public int Value1 { get; set; }
        }

        #endregion
    }
}
