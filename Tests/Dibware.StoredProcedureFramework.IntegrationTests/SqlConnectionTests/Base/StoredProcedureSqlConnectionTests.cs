using System;
using System.Data;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.IntegrationTests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests.Base
{
    [TestClass]
    public class StoredProcedureSqlConnectionTests
    {
        #region Constructors

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithNullConnectionString_ThrowsException()
        {
            // ARRANGE

            // ACT
            new TestConnection(null);

            // ASSERT
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithEmptyConnectionString_ThrowsException()
        {
            // ARRANGE
            const string connectionString = "";

            // ACT
            new TestConnection(connectionString);

            // ASSERT
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithWhiteSpaceonlyConnectionString_ThrowsException()
        {
            // ARRANGE
            const string connectionString = "   ";

            // ACT
            new TestConnection(connectionString);

            // ASSERT
        }

        [TestMethod]
        public void Constructor_WhenConstructedWithValidConnectionString_DoesNotThrow()
        {
            // ARRANGE
            string connectionString = Settings.Default.IntegrationTestConnection;

            // ACT
            new TestConnection(connectionString);

            // ASSERT
        }

        #endregion

        #region Dispose

        [TestMethod]
        public void Dispose_WhenCalled_DoesNotThrowException()
        {
            // ARRANGE
            string connectionString = Settings.Default.IntegrationTestConnection;
            var connection = new TestConnection(connectionString);
            
            // ACT
            connection.Dispose();

            // ASSERT
        }

        [TestMethod]
        public void Dispose_WhenCalledTwice_DoesNotThrowException()
        {
            // ARRANGE
            string connectionString = Settings.Default.IntegrationTestConnection;
            var connection = new TestConnection(connectionString);

            // ACT
            connection.Dispose();
            connection.Dispose();

            // ASSERT
        }

        #endregion

        #region Open

        [TestMethod]
        public void Open_AfterCalling_ConnectionStateIsOpen()
        {
            // ARRANGE
            string connectionString = Settings.Default.IntegrationTestConnection;
            var connection = new TestConnection(connectionString);
            ConnectionState actualConnectionState;

            // ACT
            try
            {
                connection.Open();
                actualConnectionState = connection.State;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }

            // ASSERT
            Assert.AreEqual(ConnectionState.Open, actualConnectionState);
        }

        #endregion

        #region Close

        [TestMethod]
        public void Close_AfterCalling_ConnectionStateIsClosed()
        {
            // ARRANGE
            string connectionString = Settings.Default.IntegrationTestConnection;
            var connection = new TestConnection(connectionString);
            ConnectionState actualConnectionState;

            // ACT
            try
            {
                connection.Open();
            }
            finally
            {
                connection.Close();
                actualConnectionState = connection.State;
                connection.Dispose();
            }

            // ASSERT
            Assert.AreEqual(ConnectionState.Closed, actualConnectionState);
        }

        #endregion

        #region ConnectionString

        [TestMethod]
        public void ConnectionString_WhenCalled_ReturnsOriginalConnectionString()
        {
            // ARRANGE
            string connectionString = Settings.Default.IntegrationTestConnection;
            var connection = new TestConnection(connectionString);

            // ACT
            var actualConnectionString = connection.ConnectionString;

            // ASSERT
            Assert.AreEqual(connectionString, actualConnectionString);
        }

        #endregion

        #region CreateCommand

        [TestMethod]
        public void CreateCommand_WhenCalled_ReturnsInstanceOfACommand()
        {
            // ARRANGE
            string connectionString = Settings.Default.IntegrationTestConnection;
            var connection = new TestConnection(connectionString);

            // ACT
            var actualCommand = connection.CreateCommand();

            // ASSERT
            Assert.IsNotNull(actualCommand);
        }

        #endregion

        #region Database

        [TestMethod]
        public void Database_WhenCalled_ReturnsDatabaseOfOriginalConnectionString()
        {
            // ARRANGE
            string connectionString = Settings.Default.IntegrationTestConnection;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            string expectedDatabase = builder.InitialCatalog;
            var connection = new TestConnection(connectionString);

            // ACT
            var actualDatabase = connection.Database;

            // ASSERT
            Assert.AreEqual(expectedDatabase, actualDatabase);
        }

        #endregion

        #region DataSource

        [TestMethod]
        public void DataSource_WhenCalled_ReturnsDataSourceOfOriginalConnectionString()
        {
            // ARRANGE
            string connectionString = Settings.Default.IntegrationTestConnection;
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            string expectedDataSource = builder.DataSource;
            var connection = new TestConnection(connectionString);

            // ACT
            var actualDataSource = connection.DataSource;

            // ASSERT
            Assert.AreEqual(expectedDataSource, actualDataSource);
        }

        #endregion

        #region ServerVersion

        [TestMethod]
        public void ServerVersion_WhenCalled_ReturnsAInstantiatedServerVersionString()
        {
            // ARRANGE
            string connectionString = Settings.Default.IntegrationTestConnection;
            var connection = new TestConnection(connectionString);
            string actualServerVersion;

            // ACT
            try
            {
                connection.Open();
                actualServerVersion = connection.ServerVersion;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }

            // ASSERT
            Assert.IsNotNull(actualServerVersion);
        }

        #endregion

        #region Test Objects

        private class TestConnection : StoredProcedureSqlConnection
        {
            public TestConnection(string connectionString)
                : base(connectionString)
            {
            }
        }


        #endregion
    }
}
