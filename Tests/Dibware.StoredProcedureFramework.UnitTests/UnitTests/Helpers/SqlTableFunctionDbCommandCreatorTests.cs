using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class SqlTableFunctionDbCommandCreatorTests
    {
        #region Fields

        const string FunctionName = "DummyFunction";
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
            SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(null, FunctionName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenCalledWithNulllProcedureName_ThrowsException()
        {
            // ACT
            SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenCalledWithEmptyProcedureName_ThrowsException()
        {
            // ACT
            SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, string.Empty);
        }

        #endregion

        #region Command

        [TestMethod]
        public void CommandProperty_WhenBuildCommmandNotCalled_ReturnsNull()
        {
            // ARRANGE
            var builder = SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);

            // ACT
            var actualCommand = builder.Command;

            // ASSERT
            Assert.IsNull(actualCommand);
        }

        [TestMethod]
        public void CommandProperty_WhenBuildCommmandIsCalled_ReturnsInstance()
        {
            // ARRANGE
            var builder = SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);

            // ACT
            builder.BuildCommand();
            var actualCommand = builder.Command;

            // ASSERT
            Assert.IsNotNull(actualCommand);
        }

        [TestMethod]
        public void CommandProperty_WhenBuildCommmandTwice_ReturnsDistinctInstances()
        {
            // ARRANGE
            var builder = SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);

            // ACT
            builder.BuildCommand();
            var actualCommand1 = builder.Command;
            builder.BuildCommand();
            var actualCommand2 = builder.Command;

            // ASSERT
            Assert.AreNotSame(actualCommand1, actualCommand2);
        }

        #endregion

        #region CommandText

        [TestMethod]
        public void CommandText_WhenBuildCommmandIsCalled_ReturnsProcedureName()
        {
            // ARRANGE
            string expectedCommandText = String.Format("SELECT * FROM {0} ()", FunctionName);
            var builder = SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);

            // ACT
            builder.BuildCommand();
            var actualCommand = builder.Command;
            var actualCommandText = actualCommand.CommandText;

            // ASSERT
            Assert.AreEqual(expectedCommandText, actualCommandText);
        }

        #endregion

        #region CommandTimeout

        [TestMethod]
        public void CommandTimout_WhenWithCommandTimeoutNotCalled_ReturnsDefaultTimeout()
        {
            // ARRANGE
            const int defaultCommandTimeout = 30;
            var builder = SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);

            // ACT
            builder.BuildCommand();
            var actualCommand = builder.Command;
            var actualCommandTimeout = actualCommand.CommandTimeout;

            // ASSERT
            Assert.AreEqual(defaultCommandTimeout, actualCommandTimeout);
        }

        [TestMethod]
        public void CommandTimout_WhenWithCommandTimeoutIsCalled_ReturnsCorrectTimeout()
        {
            // ARRANGE
            const int expectedCommandTimeout = 120;
            var builder = SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);

            // ACT
            builder
                .WithCommandTimeout(expectedCommandTimeout)
                .BuildCommand();
            var actualCommand = builder.Command;
            var actualCommandText = actualCommand.CommandTimeout;

            // ASSERT
            Assert.AreEqual(expectedCommandTimeout, actualCommandText);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CommandTimout_WhenWithCommandTimeoutIsCalledWithNullValue_ExceptionIsThrown()
        {
            // ARRANGE
            var builder = SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);

            // ACT
            builder
                .WithCommandTimeout(null)
                .BuildCommand();

            // ASSERT
        }

        #endregion

        #region CommandType

        [TestMethod]
        public void CommandType_WhenBuildCommmandIsCalled_ReturnsStoredProcedureCommandType()
        {
            // ARRANGE
            const CommandType expectedCommandType = CommandType.Text;
            var builder = SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);

            // ACT
            builder.BuildCommand();
            var actualCommand = builder.Command;
            var actualCommandType = actualCommand.CommandType;

            // ASSERT
            Assert.AreEqual(expectedCommandType, actualCommandType);
        }

        #endregion

        #region Parameters

        [TestMethod]
        public void Parameters_WhenBuildCommmandIsNotCalled_ReturnsEmptParameterCollection()
        {
            // ARRANGE
            var builder = SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);

            // ACT
            builder.BuildCommand();
            var actualCommand = builder.Command;
            var actualParameters = actualCommand.Parameters;

            // ASSERT
            Assert.AreEqual(0, actualParameters.Count);
        }

        [TestMethod]
        public void Parameters_WhenBuildCommmandIsCalledAndParametersWasSupplied_ReturnsSameInstance()
        {
            // ARRANGE
            var expectedParameters = new List<SqlParameter>
                    {
                        new SqlParameter("Id", SqlDbType.Int),
                        new SqlParameter("Name", SqlDbType.NVarChar),
                    };
            var builder = SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);

            // ACT
            builder
                .WithParameters(expectedParameters)
                .BuildCommand();
            var actualCommand = builder.Command;
            var actualParameters = actualCommand.Parameters;

            // ASSERT
            Assert.AreSame(expectedParameters[0], actualParameters[0]);
            Assert.AreSame(expectedParameters[1], actualParameters[1]);
        }

        #endregion

        #region Transaction

        [TestMethod]
        public void Transaction_WhenBuildCommmandIsCalledAndNoTransactionSet_ReturnsNull()
        {
            // ARRANGE
            var builder = SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);

            // ACT
            builder.BuildCommand();
            var actualCommand = builder.Command;
            var actualCommandTransaction = actualCommand.Transaction;

            // ASSERT
            Assert.IsNull(actualCommandTransaction);
        }

        [TestMethod]
        [Ignore] // Requires a valid connection first!
        public void Transaction_WhenBuildCommmandIsCalled_ContainsSameInstanceAsSupplied()
        {
            // ARRANGE
            SqlTransaction expectedTransaction = Connection.BeginTransaction();
            var builder = SqlTableFunctionDbCommandCreator.CreateSqlTableFunctionDbCommandCreator(Connection, FunctionName);

            // ACT
            builder.BuildCommand();
            var actualCommand = builder.Command;
            var actualCommandTransaction = actualCommand.Transaction;

            // ASSERT
            Assert.AreSame(expectedTransaction, actualCommandTransaction);
        }

        #endregion

        #endregion
    }
}
