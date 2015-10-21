using Dibware.StoredProcedureFramework.Tests.Integration_Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
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
