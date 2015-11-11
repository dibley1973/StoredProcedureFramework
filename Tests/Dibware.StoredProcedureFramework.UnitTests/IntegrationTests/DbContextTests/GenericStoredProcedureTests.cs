using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.DbContextTests
{
    [TestClass]
    public class GenericStoredProcedureTests : BaseIntegrationTestWithDbContext
    {
        [TestMethod]
        public void ExecuteForWithGenericStoredProcedure_WhenPassedConstructedParameters_GetsExpectedResults()
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
            //resultSet = Context.NormalStoredProcedure2.Execute(parameters);
            var resultList = Context.NormalStoredProcedure2.ExecuteFor(parameters);
            //var results = resultSet.RecordSet1;
            //var result = results.First();
            var result = resultList.First();

            // ASSERT
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(expectedActive, result.Active);
        }
    }
}