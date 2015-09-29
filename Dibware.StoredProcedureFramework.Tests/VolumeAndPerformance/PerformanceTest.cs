using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.VolumnAndPerformance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Dibware.StoredProcedureFramework.Tests.VolumeAndPerformance
{
    [TestClass]
    public class PerformanceTest
    {
        [TestMethod]
        [Ignore]
        public void HowLongToSelect250KRows()
        {
            /* Note it takes 3 minutes to INSERT 250k of these rows in sql server */
            /* Note it takes 3 seconds to SELECT 250k of these rows in sql server */

            // ARRANGE
            const int expectedNumberOfRecords = 250000;
            const int commandTimeOut = 600;
            var truncateTableProcedure = new VolumeAndPerformanceTruncateStoredProcedure();
            truncateTableProcedure.InitializeFromAttributes();
            var insertTableProcedureParameters = new VolumeAndPerformanceInsertNRecordsStoredProcedureParameters
            {
                NumberOfRecords = expectedNumberOfRecords
            };
            var insertTableProcedure = new VolumeAndPerformanceInsertNRecordsStoredProcedure(insertTableProcedureParameters);
            insertTableProcedure.InitializeFromAttributes();
            var getAllStoredProcedure = new VolumeAndPerformanceGetAllStoredProcedure();
            getAllStoredProcedure.InitializeFromAttributes();
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
            VolumeAndPerformanceGetAllStoredProcedureResultSet resultSet;
            Stopwatch stopwatch = new Stopwatch();
            TimeSpan elapsed;

            // ACT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ExecuteStoredProcedure(truncateTableProcedure);
                connection.ExecuteStoredProcedure(insertTableProcedure, commandTimeout: commandTimeOut);

                stopwatch.Start();
                resultSet = connection.ExecuteStoredProcedure(getAllStoredProcedure);
                stopwatch.Stop();
            }
            elapsed = stopwatch.Elapsed;
            var results = resultSet.RecordSet1;

            // ASSERT
            Assert.AreEqual(expectedNumberOfRecords, results.Count);
            Assert.Inconclusive("Return Duration: " + elapsed);
        }

        [TestMethod]
        public void HowLongToSelect1MRows()
        {
            /* Note it takes ?? minutes to INSERT 1M of these rows in sql server */
            /* Note it takes ?? seconds to SELECT 1M of these rows in sql server */

            // ARRANGE
            const int expectedNumberOfRecords = 1000000;
            const int commandTimeOut = 800;
            var truncateTableProcedure = new VolumeAndPerformanceTruncateStoredProcedure();
            truncateTableProcedure.InitializeFromAttributes();
            var insertTableProcedureParameters = new VolumeAndPerformanceInsertNRecordsStoredProcedureParameters
            {
                NumberOfRecords = expectedNumberOfRecords
            };
            var insertTableProcedure = new VolumeAndPerformanceInsertNRecordsStoredProcedure(insertTableProcedureParameters);
            insertTableProcedure.InitializeFromAttributes();
            var getAllStoredProcedure = new VolumeAndPerformanceGetAllStoredProcedure();
            getAllStoredProcedure.InitializeFromAttributes();
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
            VolumeAndPerformanceGetAllStoredProcedureResultSet resultSet;
            Stopwatch stopwatch = new Stopwatch();
            TimeSpan elapsed;

            // ACT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ExecuteStoredProcedure(truncateTableProcedure);
                connection.ExecuteStoredProcedure(insertTableProcedure, commandTimeout: commandTimeOut);

                stopwatch.Start();
                resultSet = connection.ExecuteStoredProcedure(getAllStoredProcedure);
                stopwatch.Stop();
            }
            elapsed = stopwatch.Elapsed;
            var results = resultSet.RecordSet1;

            // ASSERT
            Assert.AreEqual(expectedNumberOfRecords, results.Count);
            Assert.Inconclusive("Return Duration: " + elapsed);
        }
    }
}
