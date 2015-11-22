using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace Dibware.StoredProcedureFramework.IntegrationTests.VolumeAndPerformanceTests
{
    [TestClass]
    public class PerformanceTest : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        //[Ignore] /* long running so switch in  / out as required */
        public void HowLongToSelect250KRows()
        {
            /* To clear records use:
                    EXECUTE [dbo].[VolumeAndPerformanceTruncate]  
              
               To insert records use:
                    DECLARE @NumberOfRecords int;
                    EXECUTE [dbo].[VolumeAndPerformanceInsertNRecords] @NumberOfRecords;
             */
            /* Note it takes 3 minutes to INSERT 250k of these rows in sql server */
            /* Note it takes 2 seconds to SELECT 250k of these rows in sql server */
            /* It should take around 2 seconds for this framework to SELECT and process records */

            // ARRANGE
            const int expectedNumberOfRecords = 250000;
            //TruncateRecords();                        // Uncomment to clear records
            //InsertNRecords(expectedNumberOfRecords);  // Uncomment to insert records

            var getAllStoredProcedure = new VolumeAndPerformanceGetAllStoredProcedure();
            Stopwatch stopwatch = new Stopwatch();
            TimeSpan elapsed;

            // ACT
            stopwatch.Start();
            var results = Connection.ExecuteStoredProcedure(getAllStoredProcedure);
            stopwatch.Stop();
            elapsed = stopwatch.Elapsed;

            // ASSERT
            Assert.AreEqual(expectedNumberOfRecords, results.Count);
            Assert.Inconclusive("Return Duration: " + elapsed);
        }

        //[TestMethod]
        //[Ignore] /* long running so switch in  / out as required */
        //public void HowLongToSelect1MRows()
        //{
        //    /* Note it takes 9:55 minutes to INSERT 1M of these rows in sql server */
        //    /* Note it takes 10 seconds to SELECT 1M of these rows in sql server */
        //    /* Approx time to select and process in framework is 00:00:09.6394072 seconds */

        //    // ARRANGE
        //    const int expectedNumberOfRecords = 1000000;
        //    var insertTableProcedureParameters = new VolumeAndPerformanceInsertNRecordsStoredProcedureParameters
        //    {
        //        NumberOfRecords = expectedNumberOfRecords
        //    };
        //    var insertTableProcedure = new VolumeAndPerformanceInsertNRecordsStoredProcedure(insertTableProcedureParameters);
        //    var getAllStoredProcedure = new VolumeAndPerformanceGetAllStoredProcedure();
        //    var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
        //    //VolumeAndPerformanceGetAllStoredProcedureResultSet resultSet;
        //    List<VolumeAndPerformanceGetAllStoredProcedureReturnType> results;
        //    Stopwatch stopwatch = new Stopwatch();

        //    // ACT
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        stopwatch.Start();
        //        results = connection.ExecuteStoredProcedure(getAllStoredProcedure);
        //        stopwatch.Stop();
        //        connection.Close();
        //    }
        //    var elapsed = stopwatch.Elapsed;

        //    // ASSERT
        //    Assert.AreEqual(expectedNumberOfRecords, results.Count);
        //    Assert.Inconclusive("Return Duration: " + elapsed);
        //}

        //[TestMethod]
        ///*[Ignore] /* long running so switch in  / out as required */
        //public void HowLongToSelect2MRows()
        //{
        //    /* Note it takes 20 minutes to INSERT 2M of these rows in sql server */
        //    /* Note it takes 20 seconds to SELECT 2M of these rows in sql server */
        //    /* Approx time to select and process in framework is 00:00:19.6106512 seconds With Activator */
        //    /* Approx time to select and process in framework is 00:00:18.9935831 seconds With FastActivator */
        //    /* Approx time to select and process in framework is 00:00:19.8269147 seconds With FastActivator */
        //    // Even at 2M records there is still neglidgable difference between
        //    // standard Activator and FastActivator

        //    // ARRANGE
        //    const int expectedNumberOfRecords = 2000000;
        //    var insertTableProcedureParameters = new VolumeAndPerformanceInsertNRecordsStoredProcedureParameters
        //    {
        //        NumberOfRecords = expectedNumberOfRecords
        //    };
        //    var insertTableProcedure = new VolumeAndPerformanceInsertNRecordsStoredProcedure(insertTableProcedureParameters);
        //    var getAllStoredProcedure = new VolumeAndPerformanceGetAllStoredProcedure();
        //    var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
        //    List<VolumeAndPerformanceGetAllStoredProcedureReturnType> results;
        //    Stopwatch stopwatch = new Stopwatch();

        //    // ACT
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        stopwatch.Start();
        //        results = connection.ExecuteStoredProcedure(getAllStoredProcedure);
        //        stopwatch.Stop();
        //        connection.Close();
        //    }
        //    var elapsed = stopwatch.Elapsed;
        //    //var results = resultSet.RecordSet1;

        //    // ASSERT
        //    Assert.AreEqual(expectedNumberOfRecords, results.Count);
        //    Assert.Inconclusive("Return Duration: " + elapsed);
        //}

        //[TestMethod]
        //[Ignore] /* long running so switch in  / out as required */
        //public void HowLongToSelect3MRows()
        //{
        //    /* Note it takes ?? minutes to INSERT 3M of these rows in sql server */
        //    /* Note it takes ?? seconds to SELECT 3M of these rows in sql server */

        //    // ARRANGE
        //    const int expectedNumberOfRecords = 3000000;
        //    var insertTableProcedureParameters = new VolumeAndPerformanceInsertNRecordsStoredProcedureParameters
        //    {
        //        NumberOfRecords = expectedNumberOfRecords
        //    };
        //    var getAllStoredProcedure = new VolumeAndPerformanceGetAllStoredProcedure();
        //    var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
        //    //VolumeAndPerformanceGetAllStoredProcedureResultSet resultSet;
        //    List<VolumeAndPerformanceGetAllStoredProcedureReturnType> results;
        //    Stopwatch stopwatch = new Stopwatch();

        //    // ACT
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        stopwatch.Start();
        //        results = connection.ExecuteStoredProcedure(getAllStoredProcedure);
        //        stopwatch.Stop();
        //        connection.Close();
        //    }
        //    var elapsed = stopwatch.Elapsed;

        //    // ASSERT
        //    Assert.AreEqual(expectedNumberOfRecords, results.Count);
        //    Assert.Inconclusive("Return Duration: " + elapsed);
        //}

        private void InsertNRecords(int expectedNumberOfRecords)
        {
            var insertTableProcedureParameters = new VolumeAndPerformanceInsertNRecordsStoredProcedure.Parameter
            {
                NumberOfRecords = expectedNumberOfRecords
            };
            var insertTableProcedure = new VolumeAndPerformanceInsertNRecordsStoredProcedure(insertTableProcedureParameters);
            Connection.ExecuteStoredProcedure(insertTableProcedure);
        }

        private void TruncateRecords()
        {
            var truncateTableProcedure = new VolumeAndPerformanceTruncateStoredProcedure();
            Connection.ExecuteStoredProcedure(truncateTableProcedure);
        }
    }
}
