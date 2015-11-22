using System;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests
{
    [TestClass]
    public class RecordSetNotInstantiatedTestsWithDbContext : BaseDbContextIntegrationTest
    {
        [TestMethod]
        [Ignore] // Need to find out if there is a way to set this test up!
        [ExpectedException(typeof (NullReferenceException))]
        public void CallStoredProcedure_WithRecordSetNotInstantiatedInResultSet_ThrowsNullReferenceException()
        {
            // ARRANGE
            var parameters = new NullStoredProcedureParameters();
            var procedure = new NotInstantiatedRecordSetStoredProcedure(parameters);

            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
        }
    }
}