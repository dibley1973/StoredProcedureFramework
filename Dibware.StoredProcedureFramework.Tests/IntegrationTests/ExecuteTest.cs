using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests
{
    [TestClass]
    public class ExecuteTest : BaseIntegrationTest
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
