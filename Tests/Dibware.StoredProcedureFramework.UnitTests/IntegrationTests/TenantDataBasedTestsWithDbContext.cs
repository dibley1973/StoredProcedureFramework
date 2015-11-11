using System;
using System.Linq;
using Dibware.StoredProcedureFramework.Tests.Context;
using Dibware.StoredProcedureFramework.Tests.Fakes.Entities;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.ResultSets.TenantResultSets;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.TenantProcedures;
using Dibware.StoredProcedureFrameworkForEF;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests
{
    [TestClass]
    public class TenantDataBasedTestsWithDbContext : BaseIntegrationTestWithDbContext
    {
        #region Tests

        #region EntityFrameworkConnection Test

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

        #endregion

        #region GetAllTenants Tests

        [TestMethod]
        public void GetAllTenants_ReturnsCorrectResultCount()
        {
            // ARRANGE
            const int expectedCount = 2;
            const string expectedProcedureName = "Tenant_GetAll";
            const string expectedSchemaName = "app";
            //TenantGetAllNoAttributesResultSet resultSet;
            var procedure = new TenantGetAllNoAttributes(expectedSchemaName,
                expectedProcedureName);
            AddTenentsToContext(Context);

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);
            //var results = resultSet.RecordSet1;

            // ASSERT
            Assert.AreEqual(expectedCount, results.Count);
        }

        [TestMethod]
        public void GetAllTenants_ReturnsResultsCastToCorrectType()
        {
            // ARRANGE
            Type expectedType = typeof(TenantResultRow);
            const string expectedProcedureName = "Tenant_GetAll";
            const string expectedSchemaName = "app";
           // TenantGetAllNoAttributesResultSet resultSet;
            var procedure = new TenantGetAllNoAttributes(expectedSchemaName,
                expectedProcedureName);
            AddTenentsToContext(Context);

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);
            //var results = resultSet.RecordSet1;

            // ASSERT
            Assert.IsInstanceOfType(results.First(), expectedType);
        }

        #endregion

        #region GetTenantForName Tests

        [TestMethod]
        public void GetTenantForName_ReturnsOneRecord()
        {
            // ARRANGE
            const int expectedCount = 1;
            const string expectedName = "Me";
            var parameters = new GetTenantForTenantNameParameters
            {
                TenantName = expectedName
            };
            var procedure = new GetTenantForTenantNameProcedure(parameters);
            AddTenentsToContext(Context);

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);
            //var results = resultSet.RecordSet1;

            // ASSERT
            Assert.AreEqual(expectedCount, results.Count);
        }

        [TestMethod]
        public void GetTenantForName_ReturnsCorrectName()
        {
            // ARRANGE
            const string expectedName = "Me";
            var parameters = new GetTenantForTenantNameParameters
            {
                TenantName = expectedName
            };
            var procedure = new GetTenantForTenantNameProcedure(parameters);
            AddTenentsToContext(Context);

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);
            //var results = resultSet.RecordSet1;

            // ASSERT
            Assert.AreEqual(expectedName, results.First().TenantName);
        }

        #endregion

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