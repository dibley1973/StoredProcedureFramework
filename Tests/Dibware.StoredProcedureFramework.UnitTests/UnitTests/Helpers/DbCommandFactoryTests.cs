using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class DbCommandFactoryTests
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

        #region Command

        [TestMethod]
        public void CommandProperty_WhenCreated_ReturnsInstance()
        {
            // ACT
            var actualCommand = DbCommandFactory.CreateStoredProcedureCommand(Connection, StoredProcedureName);

            // ASSERT
            Assert.IsNotNull(actualCommand);
        }

        #endregion

        #region CommandText

        [TestMethod]
        public void CommandText_WhenCreated_ReturnsProcedureName()
        {
            // ACT
            var actualCommand = DbCommandFactory.CreateStoredProcedureCommand(Connection, StoredProcedureName);
            var actualCommandText = actualCommand.CommandText;

            // ASSERT
            Assert.AreEqual(StoredProcedureName, actualCommandText);
        }

        #endregion

        #region CommandTimeout

        [TestMethod]
        public void CommandTimout_WhenWithCommandTimeoutNotCalled_ReturnsDefaultTimeout()
        {
            // ARRANGE
            const int defaultCommandTimeout = 30;
            
            // ACT
            var actualCommand = DbCommandFactory.CreateStoredProcedureCommand(Connection, StoredProcedureName);
            var actualCommandTimeout = actualCommand.CommandTimeout;

            // ASSERT
            Assert.AreEqual(defaultCommandTimeout, actualCommandTimeout);
        }

        [TestMethod]
        public void CommandTimout_WhenCreatedWithCommandTimeout_ReturnsCorrectTimeout()
        {
            // ARRANGE
            const int expectedCommandTimeout = 120;

            // ACT
            var actualCommand = DbCommandFactory.CreateStoredProcedureCommandWithCommandTimeout(Connection, StoredProcedureName, expectedCommandTimeout);
            var actualCommandText = actualCommand.CommandTimeout;

            // ASSERT
            Assert.AreEqual(expectedCommandTimeout, actualCommandText);
        }

        #endregion

        #region CommandType

        [TestMethod]
        public void CommandType_WhenBuildCommmandIsCalled_ReturnsStoredProcedureCommandType()
        {
            // ARRANGE
            const CommandType expectedCommandType = CommandType.StoredProcedure;
            
            // ACT
            var actualCommand = DbCommandFactory.CreateStoredProcedureCommand(Connection, StoredProcedureName);
            var actualCommandType = actualCommand.CommandType;

            // ASSERT
            Assert.AreEqual(expectedCommandType, actualCommandType);
        }

        #endregion

        #region Parameters

        [TestMethod]
        public void Parameters_WhenBuildCommmandIsNotCalled_ReturnsEmptParameterCollection()
        {
            // ACT
            var actualCommand = DbCommandFactory.CreateStoredProcedureCommand(Connection, StoredProcedureName);
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
            
            // ACT
            var actualCommand = DbCommandFactory.CreateStoredProcedureCommandWithParameters(Connection, StoredProcedureName, expectedParameters);
            var actualParameters = actualCommand.Parameters;

            // ASSERT
            Assert.AreSame(expectedParameters[0], actualParameters[0]);
            Assert.AreSame(expectedParameters[1], actualParameters[1]);
        }

        #endregion

        #region Transaction

        [TestMethod]
        public void Transaction_WhenBuildCommmandIsNotCalled_ReturnsNull()
        {
            // ACT
            var actualCommand = DbCommandFactory.CreateStoredProcedureCommand(Connection, StoredProcedureName);
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
            
            // ACT
            var actualCommand = DbCommandFactory.CreateStoredProcedureCommandWithTransaction(Connection, StoredProcedureName, expectedTransaction);
            var actualCommandTransaction = actualCommand.Transaction;

            // ASSERT
            Assert.AreSame(expectedTransaction, actualCommandTransaction);
        }

        #endregion

        #endregion
    }
}
