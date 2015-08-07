using System;
using Dibware.StoredProcedureFramework.Tests.Context;
using Dibware.StoredProcedureFramework.Tests.Fakes.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class IntegrationTest1
    {
        private IntegrationTestContext _context;

        [TestInitialize]
        public void SpinUpObjects()
        {
            _context = new IntegrationTestContext("IntegrationTestConnection");
            //if(!_context.Database.Exists()) throw new NullReferenceException("database");
        }

        [TestCleanup]
        public void KillObjects()
        {
            _context.Dispose();
        }


        [TestMethod]
        public void TestMethod1()
        {
            // ARRANGE
            //var context = new IntegrationTestContext("IntegrationTestConnection");

            // ACT
            var tenants = _context.Set<Tenant>();


            // ASSERT

        }
    }
}
