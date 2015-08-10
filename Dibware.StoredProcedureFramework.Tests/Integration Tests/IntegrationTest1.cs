using Dibware.StoredProcedureFramework.Tests.Fakes.Entities;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class IntegrationTest1 : BaseIntegrationTest
    {
        #region Test Pre and Clear down

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

        #endregion

        #region Tests
        

        [TestMethod]
        public void EntityFrameworkConnectionTest()
        {
            // ARRANGE
            const int expectedCount = 2;
            AddTenentsToContext(Context);

            // ACT
            var tenants = Context.Set<Tenant>().ToList();


            // ASSERT
            Assert.AreEqual(expectedCount, tenants.Count);
        }

        [TestMethod]
        public void GetAllTenents_ReturnsAllTenants()
        {
            // ARRANGE
            const int expectedCount = 2;
            var procedure = new StoredProcedure<GetTenantForAll>();
            AddTenentsToContext(Context);

            // ACT
            var tenantResults = Context.ExecuteStoredProcedure(
                procedure);

            // ASSERT
            Assert.AreEqual(expectedCount, tenantResults.Count);
        }

        #endregion

        #region Methods Data Setup



        private void AddTenentsToContext(Context.IntegrationTestContext context)
        {
            context.Tenants.Add(new Tenant()
            {
                IsActive = true,
                TenantId = 1,
                TenantName = "Me",
                RecordCreatedDateTime = DateTime.Now
            });
            context.Tenants.Add(new Tenant()
            {
                IsActive = true,
                TenantId = 2,
                TenantName = "You",
                RecordCreatedDateTime = DateTime.Now
            });
            context.SaveChanges();
        }

        #endregion
    }
}