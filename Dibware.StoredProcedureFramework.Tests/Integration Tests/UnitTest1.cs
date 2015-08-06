using Dibware.StoredProcedureFramework.Tests.Context;
using Dibware.StoredProcedureFramework.Tests.Fakes.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // ARRANGE
            var context = new IntegrationTestContext("IntegrationTestConnection");

            // ACT
            var tenants = context.Set<Tenant>();


            // ASSERT

        }
    }
}
