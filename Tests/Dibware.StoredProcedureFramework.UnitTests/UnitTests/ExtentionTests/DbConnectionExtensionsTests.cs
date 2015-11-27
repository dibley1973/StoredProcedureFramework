using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.ExtentionTests
{
    [TestClass]
    public class DbConnectionExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteStoredProcedure_WithNullStoredProcedure_ThrowsArgumentNullException()
        {
            // ARRANGE
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
            IStoredProcedure<NullStoredProcedureResult, NullStoredProcedureParameters> procedure1 = null;
            //MostBasicStoredProcedure procedure = null;

            // ACT
            using (DbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ExecuteStoredProcedure(procedure1);
            }

            // ASSERT
            // Exception should have been thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteStoredProcedure_WithNullStoredProcedureName_ThrowsArgumentNullException()
        {
            // ARRANGE
            const string procedureName = null;
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

            // ACT
            using (DbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ExecuteStoredProcedure < MostBasicStoredProcedure>(procedureName);
            }

            // ASSERT
            // Exception should have been thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExecuteStoredProcedure_WithEmptyStoredProcedureName_ThrowsArgumentNullException()
        {
            // ARRANGE
            const string procedureName = "";
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

            // ACT
            using (DbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ExecuteStoredProcedure<MostBasicStoredProcedure>(procedureName);
            }

            // ASSERT
            // Exception should have been thrown
        }

        [TestMethod]
        public void CreateStoredProcedureCommand_WhenCommandTimeoutIsSpecified_CommandTimeoutOfResultIsSet()
        {
            // ARRANGE
            const int expectedCommandTimeout = 37;
            const string procedureName = "GetAll";
            DbCommand actualCommand;
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
            
            // ACT
            using (DbConnection connection = new SqlConnection(connectionString))
            {
                actualCommand = connection.CreateStoredProcedureCommand(
                    procedureName,
                    null,
                    expectedCommandTimeout
                );
            }

            // ASSERT
            Assert.AreEqual(expectedCommandTimeout, actualCommand.CommandTimeout);
        }
    }
}
