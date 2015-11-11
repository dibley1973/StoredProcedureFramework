using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.DecimalTests;
using Dibware.StoredProcedureFrameworkForEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Dibware.StoredProcedureFrameworkForEF.Extensions;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.DbContextTests
{
    [TestClass]
    public class LessColumnsInResultSetThanReturnObjectTestsWithDbContext : BaseIntegrationTestWithDbContext
    {
        [TestMethod]
        [ExpectedException(typeof(MissingFieldException))]
        public void LessColumnsInProcedureResultSetThanReturnObject_ThrowsMissingFieldException()
        {
            var procedure = new DecimalTestStoredProcedure();

            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.Fail();
        }
    }
}