using Dibware.StoredProcedureFramework.Examples.DbContextExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Examples.DbContextExampleTests
{
    [TestClass]
    public class StoredProcedureWithoutParameters : DbContextExampleTestBase
    {

        // ====== WORK IN PROGRESS ======
        // ====== WORK IN PROGRESS ======
        // ====== WORK IN PROGRESS ======
        // ====== WORK IN PROGRESS ======
        // ====== WORK IN PROGRESS ======
        // ====== WORK IN PROGRESS ======


        /// <summary>
        /// Tenants the get all.
        /// </summary>
        /// <remarks>Takes no parameters but returns results</remarks>
        [TestMethod]
        public void TenantGetAll()
        {
            //StoredProcedure<<TenantDto> TenantGetAll {get;set;}

            // ARRANGE
            const int expectedTenantCount = 1;

            // ACT
            var tenants = Context.TenantGetAll.Execute();
            TenantDto firstResult = tenants.FirstOrDefault();

            // ASSERT
            Assert.AreEqual(expectedTenantCount, tenants.Count);
            Assert.IsNotNull(firstResult);
        }

        /// <summary>
        /// Tenants for identifier.
        /// </summary>
        /// <remarks>Takes parameters and returns results</remarks>
        [TestMethod]
        public void TenantGetForId()
        {
            //StoredProcedure<TenantDto> TenantGetForId {get;set;}

            var tenant = Context.TenantGetForId.ExecuteFor(new { Id = "1" });
            //var tenant = Context.TenantGetForId.GetFirst(new {Id = "1"}); //Possibility

        }

        /// <summary>
        /// Tenants the delete identifier.
        /// </summary>
        /// <remarks>Takes parameters but returns no results</remarks>
        [TestMethod]
        public void TenantDeleteId()
        {
            //StoredProcedure TenantDeleteId {get;set;}

            Context.TenantDeleteId.ExecuteFor(new { Id = "1" });
        }

        /// <summary>
        /// Tenants the delete identifier.
        /// </summary>
        /// <remarks>Takes no parameters and returns no results</remarks>
        [TestMethod]
        public void TenantMarkAllinactive()
        {
            //StoredProcedure TenantMarkAllinactive {get;set;}

            Context.TenantMarkAllinactive.Execute();
        }
    }
}