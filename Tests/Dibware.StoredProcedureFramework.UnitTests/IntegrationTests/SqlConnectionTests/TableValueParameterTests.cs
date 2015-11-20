
namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.SqlConnectionTests
{
    // Moved to:
    // Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests.TableValueParameterTests

    //[TestClass]
    //public class TableValueParameterTests : BaseIntegrationTest
    //{
    //    [TestMethod]
    //    public void SimpleTableValueParameterStoredProcedure_CallsCorrectly()
    //    {
    //        // ARRANGE
    //        var itemsToAdd = new List<SimpleTableValueParameterTableType>
    //        {
    //            new SimpleTableValueParameterTableType { Name = "Company 1", IsActive = true, Id = 2 },
    //            new SimpleTableValueParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
    //            new SimpleTableValueParameterTableType { Name = "Company 3", IsActive = true, Id = 2 }
    //        };
    //        var parameters = new SimpleTableValueParameterTableTypeParameters
    //        {
    //            TvpParameters = itemsToAdd
    //        };
    //        var procedure = new SimpleTableValueParameterStoredProcedure(parameters);

    //        // ACT
    //        Connection.Open();
    //        Connection.ExecuteStoredProcedure(procedure);
    //        Connection.Close();

    //        // ASSERT
    //    }

    //    [TestMethod]
    //    public void TableValueParameterStoredProcedureWithReturn_CallsCorrectlyAndReturnsValues()
    //    {
    //        // ARRANGE
    //        var itemsToAdd = new List<SimpleTableValueParameterTableType>
    //        {
    //            new SimpleTableValueParameterTableType { Name = "Company 1", IsActive = true, Id = 2 },
    //            new SimpleTableValueParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
    //            new SimpleTableValueParameterTableType { Name = "Company 3", IsActive = true, Id = 2 }
    //        };
    //        int expectedCount = itemsToAdd.Count;
    //        var parameters = new TableValueParameterStoredProcedureWithReturnParameters
    //        {
    //            TvpParameters = itemsToAdd
    //        };
    //        var procedure = new TableValueParameterStoredProcedureWithReturn(parameters);

    //        // ACT
    //        Connection.Open();
    //        var results = Connection.ExecuteStoredProcedure(procedure);
    //        Connection.Close();
    //        var actualCount = results.Count;

    //        // ASSERT
    //        Assert.IsNotNull(results);
    //        Assert.AreEqual(expectedCount, actualCount);
    //    }
    //}
}