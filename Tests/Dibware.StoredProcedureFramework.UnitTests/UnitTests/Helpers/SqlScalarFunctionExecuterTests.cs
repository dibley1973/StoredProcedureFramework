﻿using System;
using System.Data;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class SqlScalarFunctionExecuterTests
    {
        #region Fields

        const string SqlFunctionName = "DummyFunction";
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
            SqlScalarFunctionExecuter<TestResultSet>.CreateSqlScalarFunctionExecuter(null, SqlFunctionName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenCalledWithNulllProcedureName_ThrowsException()
        {
            // ACT
            SqlScalarFunctionExecuter<TestResultSet>.CreateSqlScalarFunctionExecuter(Connection, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenCalledWithEmptyProcedureName_ThrowsException()
        {
            // ACT
            SqlScalarFunctionExecuter<TestResultSet>.CreateSqlScalarFunctionExecuter(Connection, string.Empty);
        }

        [TestMethod]
        public void Constructor_WhenCalledWithValidconnectionAndProcedureName_ReturnsConstructedInstance()
        {
            // ACT
            var actualSqlFunctionExecuter = SqlScalarFunctionExecuter<TestResultSet>.CreateSqlScalarFunctionExecuter(Connection, SqlFunctionName);

            // ASSERT
            Assert.IsNotNull(actualSqlFunctionExecuter);
        }

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