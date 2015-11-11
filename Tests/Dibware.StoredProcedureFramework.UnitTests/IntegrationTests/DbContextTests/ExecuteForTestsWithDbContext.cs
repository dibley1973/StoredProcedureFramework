using System;
using System.Collections.Generic;
using System.Linq;
using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.DbContextTests
{
    [TestClass]
    public class ExecuteForTestsWithDbContext : BaseIntegrationTestWithDbContext
    {
        [TestMethod]
        public void ExecuteFor_WhenPassedConstructedParameters_GetsExpectedResults()
        {
            // ARRANGE
            const int expectedId = 10;
            const string expectedName = @"Dave";
            const bool expectedActive = true;
            //NormalStoredProcedureResultSet resultSet;

            var parameters = new NormalStoredProcedureParameters
            {
                Id = expectedId
            };

            // ACT
            //resultSet = Context.NormalStoredProcedure.Execute(parameters);
            var resultList = Context.NormalStoredProcedure.ExecuteFor(parameters);
            //var results = resultSet.RecordSet1;
            //var result = results.First();
            var result = resultList.First();

            // ASSERT
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(expectedActive, result.Active);
        }

        [TestMethod]
        public void ExecuteFor_WhenPassedAnonymousParameterObject_GetsExpectedResults()
        {
            // ARRANGE
            const int expectedId = 10;
            const string expectedName = @"Dave";
            const bool expectedActive = true;
            //NormalStoredProcedureResultSet resultSet;

            // ACT
            //resultSet = Context.AnonymousParameterStoredProcedure.Execute(new { Id = expectedId });
            var resultList = Context.AnonymousParameterStoredProcedure.ExecuteFor(new { Id = expectedId });
            //var results = resultSet.RecordSet1;
            //var result = results.First();
            var result = resultList.First();

            // ASSERT
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(expectedActive, result.Active);
        }


        //[TestMethod]
        //[ExpectedException(typeof(ArgumentNullException))]
        //public void ExecuteFor_WhenPassedNullParameters_ThrowsArgumentNullException()
        //{
        //    // ARRANGE

        //    // ACT
        //    Context.NormalStoredProcedure.Execute(null);

        //    // ASSERT
        //    // Should have thrown exception by here!
        //}
    }
}
