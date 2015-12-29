using Dibware.StoredProcedureFramework.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

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
            string connectionString = Properties.Settings.Default.IntegrationTestConnection;

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
            string connectionString = Properties.Settings.Default.IntegrationTestConnection;
            var connection = new TestConnection(connectionString);
            
            // ACT
            connection.Dispose();

            // ASSERT
        }

        #endregion

        #region Open

        [TestMethod]
        public void Open_AfterCalling_ConnectionStateIsOpen()
        {
            // ARRANGE
            string connectionString = Properties.Settings.Default.IntegrationTestConnection;
            var connection = new TestConnection(connectionString);

            // ACT
            connection.Open();
            var actualConnectionState = connection.State;
            connection.Close();
            connection.Dispose();

            // ASSERT
            Assert.AreEqual(ConnectionState.Open, actualConnectionState);
        }

        #endregion

        #region Close

        [TestMethod]
        public void Close_AfterCalling_ConnectionStateIsClosed()
        {
            // ARRANGE
            string connectionString = Properties.Settings.Default.IntegrationTestConnection;
            var connection = new TestConnection(connectionString);
            connection.Open();
            
            // ACT
            connection.Close();
            var actualConnectionState = connection.State;
            connection.Dispose();

            // ASSERT
            Assert.AreEqual(ConnectionState.Closed, actualConnectionState);
        }

        #endregion

        #region Test Objects

        internal class TestConnection : StoredProcedureSqlConnection
        {
            public TestConnection(string connectionString)
                : base(connectionString)
            {
            }
        }


        #endregion
    }
}
