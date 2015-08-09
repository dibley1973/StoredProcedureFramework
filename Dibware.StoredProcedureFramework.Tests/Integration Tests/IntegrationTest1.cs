using Dibware.StoredProcedureFramework.Tests.Fakes.Entities;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class IntegrationTest1 : BaseIntegrationTest
    {
        [TestInitialize]
        public void TestSetup()
        {
            PrepareDatabase();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            CleanupDatabase();
        }

        [TestMethod]
        public void GetAllTenents_ReturnsAllTenants()
        {
            // ARRANGE
            Context.Tenants.Add(new Tenant() { Active = true, TenantId = 1, TenantName = "Me", RecordCreatedDateTime = DateTime.Now });
            Context.Tenants.Add(new Tenant() { Active = true, TenantId = 2, TenantName = "You", RecordCreatedDateTime = DateTime.Now });
            Context.SaveChanges();

            // ACT
            var tenants = Context.Set<Tenant>().ToList();


            // ASSERT

        }
    }
}