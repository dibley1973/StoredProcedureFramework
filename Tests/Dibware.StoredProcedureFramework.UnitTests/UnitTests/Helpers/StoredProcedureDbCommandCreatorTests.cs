using System.Data;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class StoredProcedureDbCommandCreatorTests
    {
        #region Fields

        const string StoredProcedureName = "DummyProcedure";
        const string ConnectionString = "Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";
        SqlConnection _connection;

        #endregion

        #region Properties

        protected SqlConnection Connection
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
        public void CommandProperty_WhenBuildCommmandNotCalled_ReturnsNull()
        {
            // ARRANGE
            var builder = StoredProcedureDbCommandCreator.CreateStoredProcedureDbCommandCreator(Connection, StoredProcedureName);
            
            // ACT
            var actualCommand = builder.Command;

            // ASSERT
            Assert.IsNull(actualCommand);
        }

        [TestMethod]
        public void CommandProperty_WhenBuildCommmandIsCalled_ReturnsInstance()
        {
            // ARRANGE
            var builder = StoredProcedureDbCommandCreator.CreateStoredProcedureDbCommandCreator(Connection, StoredProcedureName);

            // ACT
            builder.BuildCommand();
            var actualCommand = builder.Command;

            // ASSERT
            Assert.IsNotNull(actualCommand);
        }

        #endregion

        #region CommandText

        [TestMethod]
        public void CommandText_WhenBuildCommmandIsCalled_ReturnsProcedureName()
        {
            // ARRANGE
            var builder = StoredProcedureDbCommandCreator.CreateStoredProcedureDbCommandCreator(Connection, StoredProcedureName);

            // ACT
            builder.BuildCommand();
            var command = builder.Command;
            var actualCommandText = command.CommandText;

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
            var builder = StoredProcedureDbCommandCreator.CreateStoredProcedureDbCommandCreator(Connection, StoredProcedureName);

            // ACT
            builder.BuildCommand();
            var command = builder.Command;
            var actualCommandTimeout = command.CommandTimeout;

            // ASSERT
            Assert.AreEqual(defaultCommandTimeout, actualCommandTimeout);
        }

        [TestMethod]
        public void CommandTimout_WhenWithCommandTimeoutIsCalled_ReturnsCorrectTimeout()
        {
            // ARRANGE
            const int expectedCommandTimeout = 120;
            var builder = StoredProcedureDbCommandCreator.CreateStoredProcedureDbCommandCreator(Connection, StoredProcedureName);

            // ACT
            builder
                .WithCommandTimeout(expectedCommandTimeout)
                .BuildCommand();
            var command = builder.Command;
            var actualCommandText = command.CommandTimeout;

            // ASSERT
            Assert.AreEqual(expectedCommandTimeout, actualCommandText);
        }

        #endregion

        #endregion
    }
}