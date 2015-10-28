using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests
{
    [TestClass]
    public class RecordSetNotInstantiatedTests : BaseIntegrationTest
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CallStoredProcedure_WithRecordSetNotInstantiatedInResultSet_ThrowsNullReferenceException()
        {
            // ARRANGE
            var parameters = new NullStoredProcedureParameters();
            var procedure = new NotInstantiatedRecordSetStoredProcedure(parameters);

            // ACT
            Connection.Open();
            Connection.ExecuteStoredProcedure(procedure);
            Connection.Close();

            // ASSERT
            Assert.Fail();
        }
    }
}
