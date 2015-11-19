using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class IncorrectReturnType : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void DifferentDataTypeInReturnTypeThanProcedureResultSet_ThrowsInvalidCastException()
        {
            var procedure = new DecimalWrongReturnTestStoredProcedure();

            // ACT
            Connection.Open();
            Connection.ExecuteStoredProcedure(procedure);
            Connection.Close();

            // ASSERT
            Assert.Fail();
        }
    }
}