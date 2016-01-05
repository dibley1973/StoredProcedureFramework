using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Contracts;
using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
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

            // ACT
            using (DbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ExecuteStoredProcedure(procedure1);
            }

            // ASSERT
            // Exception should have been thrown
        }

        // TODO: This test has been commented out as it directly accesses
        // a member which has deemed to better be private
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void ExecuteStoredProcedure_WithNullStoredProcedureName_ThrowsArgumentNullException()
        //{
        //    // ARRANGE
        //    const string procedureName = null;
        //    var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

        //    // ACT
        //    using (DbConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        connection.ExecuteStoredProcedure<MostBasicStoredProcedure>(procedureName);
        //    }

        //    // ASSERT
        //    // Exception should have been thrown
        //}

        // TODO: This test has been commented out as it directly accesses
        // a member which has deemed to better be private
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        //public void ExecuteStoredProcedure_WithEmptyStoredProcedureName_ThrowsArgumentNullException()
        //{
        //    // ARRANGE
        //    const string procedureName = "";
        //    var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

        //    // ACT
        //    using (DbConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        connection.ExecuteStoredProcedure<MostBasicStoredProcedure>(procedureName);
        //    }

        //    // ASSERT
        //    // Exception should have been thrown
        //}

        //// superseeded by :
        //// CommandTimout_WhenWithCommandTimeoutIsCalled_ReturnsCorrectTimeout
        //[TestMethod]
        //public void CreateStoredProcedureCommand_WhenCommandTimeoutIsSpecified_CommandTimeoutOfResultIsSet()
        //{
        //    // ARRANGE
        //    const int expectedCommandTimeout = 37;
        //    const string procedureName = "GetAll";
        //    DbCommand actualCommand;
        //    var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

        //    // ACT
        //    using (DbConnection connection = new SqlConnection(connectionString))
        //    {
        //        actualCommand = connection.CreateStoredProcedureCommand(
        //            procedureName,
        //            null,
        //            expectedCommandTimeout
        //        );
        //    }

        //    // ASSERT
        //    Assert.AreEqual(expectedCommandTimeout, actualCommand.CommandTimeout);
        //}
    }
}