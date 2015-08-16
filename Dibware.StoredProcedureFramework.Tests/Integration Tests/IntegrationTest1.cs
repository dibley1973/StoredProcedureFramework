using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.Context;
using Dibware.StoredProcedureFramework.Tests.Fakes.Entities;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.Base;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Just to check context connects...
        /// </summary>
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
            //procedure.SetProcedureName("Company_GetAll");
            procedure.InitializeFromAttributes();

            AddTenentsToContext(Context);

            // ACT
            List<object> tenantResults = Context.ExecuteStoredProcedure(
                procedure);

            // next we need to be able to get an explicit list as the return rather than an list of objects.


            // ASSERT
            Assert.AreEqual(expectedCount, tenantResults.Count);
        }


        [TestMethod]
        public void GetAllTenents_ReturnsAllTenantsCast()
        {
            // ARRANGE
            const int expectedCount = 2;
            const string expectedProcedureName = "Tenant_GetAll";
            const string expectedSchemaName = "app";
            var parameters = new NullStoredProcedureParameters();
            var procedure = new TenantGetAllNoAttributes(expectedSchemaName,
                expectedProcedureName, parameters);
            AddTenentsToContext(Context);

            // ACT
            var results = Context.ExecSproc(procedure);

            // ASSERT
            Assert.AreEqual(expectedCount, results.Count);
        }


        [TestMethod]
        public void GetAllTenents2_ReturnsAllTenantsCast()
        {
            // ARRANGE
            const int expectedCount = 2;
            var parameters = new NullStoredProcedureParameters();
            var procedure = new TenantGetAllNoAttributes(parameters);
            //procedure.SetProcedureName("Company_GetAll");
            //procedure.InitializeFromAttributes();

            AddTenentsToContext(Context);

            // ACT

            var results = Context.ExecSproc(procedure);

            //List<object> tenantResults = Context.ExecSproc<Tenant>(
            //    procedure);

            // next we need to be able to get an explicit list as the return rather than an list of objects.


            // ASSERT
            Assert.AreEqual(expectedCount, results.Count);
        }



        //[TestMethod]
        //public void GetAllTenents_ReturnsAllTenantsCast()
        //{
        //    // ARRANGE
        //    const int expectedCount = 2;
        //    var procedure = new StoredProcedure<GetTenantForAll>();
        //    //procedure.SetProcedureName("Company_GetAll");
        //    procedure.InitializeFromAttributes();

        //    AddTenentsToContext(Context);

        //    // ACT
        //    List<object> tenantResults = Context.ExecuteStoredProcedure<Tenant>(
        //        procedure);

        //    // next we need to be able to get an explicit list as the return rather than an list of objects.


        //    // ASSERT
        //    Assert.AreEqual(expectedCount, tenantResults.Count);
        //}




        #endregion

        #region Methods Data Setup

        /// <summary>
        /// Adds the tenents to context.
        /// </summary>
        /// <param name="context">The context.</param>
        private void AddTenentsToContext(IntegrationTestContext context)
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