using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests
{
    [TestClass]
    public class ExecuteTestWithDbContext : BaseDbContextIntegrationTest
    {
        [TestMethod]
        public void Execute_WhenCalledOnBasicStoredProcedure_doesNotFail()
        {
            // ARRANGE

            // ACT
            Context.MostBasicStoredProcedure.Execute();

            // ASSERT
        }
    }
}
