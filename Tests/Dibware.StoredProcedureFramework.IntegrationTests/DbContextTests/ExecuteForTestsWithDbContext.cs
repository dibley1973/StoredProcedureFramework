using System.Linq;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests
{
    [TestClass]
    public class ExecuteForTestsWithDbContext : BaseDbContextIntegrationTest
    {
        [TestMethod]
        public void ExecuteFor_WhenPassedConstructedParameters_GetsExpectedResults()
        {
            // ARRANGE
            const int expectedId = 10;
            const string expectedName = @"Dave";
            const bool expectedActive = true;

            var parameters = new NormalStoredProcedureForEf.Parameter
            {
                Id = expectedId
            };

            // ACT
            var resultList = Context.NormalStoredProcedure.ExecuteFor(parameters);
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

            // ACT
            var resultList = Context.AnonymousParameterStoredProcedure.ExecuteFor(new { Id = expectedId });
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
