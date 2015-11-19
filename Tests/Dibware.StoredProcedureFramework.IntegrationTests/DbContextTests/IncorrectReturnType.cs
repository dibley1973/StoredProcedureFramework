using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests
{
    [TestClass]
    public class IncorrectReturnType : BaseDbContextIntegrationTest
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