using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Dibware.StoredProcedureFramework.IntegrationTests.UserDefinedTypes;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests
{
    [TestClass]
    public class TableValueParameterTests : BaseDbContextIntegrationTest
    {
        [TestMethod]
        public void TableValueParameterWithoutReturnTypeStoredProcedure_CallsCorrectly()
        {
            // ARRANGE
            var itemsToAdd = new List<SimpleParameterTableType>
            {
                new SimpleParameterTableType { Name = "Company 1", IsActive = true, Id = 2 },
                new SimpleParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
                new SimpleParameterTableType { Name = "Company 3", IsActive = true, Id = 2 }
            };
            var parameters = new TableValueParameterWithoutReturnTypeStoredProcedure.Parameter
            {
                TvpParameters = itemsToAdd
            };
            var procedure = new TableValueParameterWithoutReturnTypeStoredProcedure(parameters);

            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
        }

        [TestMethod]
        public void TableValueParameterStoredProcedureWithReturn_CallsCorrectlyAndReturnsValues()
        {
            // ARRANGE
            var itemsToAdd = new List<SimpleParameterTableType>
            {
                new SimpleParameterTableType { Name = "Company 1", IsActive = true, Id = 2 },
                new SimpleParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
                new SimpleParameterTableType { Name = "Company 3", IsActive = true, Id = 2 }
            };
            int expectedCount = itemsToAdd.Count;
            var parameters = new TableValueParameterWithReturnTypeStoredProcedure.Parameter
            {
                TvpParameters = itemsToAdd
            };
            var procedure = new TableValueParameterWithReturnTypeStoredProcedure(parameters);

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);
            var actualCount = results.Count;

            // ASSERT
            Assert.IsNotNull(results);
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}