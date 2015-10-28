using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.DecimalTests;
using Dibware.StoredProcedureFrameworkForEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.DbContextTests
{
    [TestClass]
    public class IncorectReturnTypeTestsWithDbContext : BaseIntegrationTestWithDbContext
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void DifferentDataTypeInReturnTypeThanProcedureResultSet_ThrowsInvalidCastException()
        {
            var procedure = new DecimalWrongReturnTestStoredProcedure();

            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.Fail();
        }
    }
}