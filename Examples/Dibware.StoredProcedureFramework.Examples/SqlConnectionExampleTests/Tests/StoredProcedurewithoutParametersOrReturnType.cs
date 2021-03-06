﻿using Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithoutParametersOrReturnType
        : SqlConnectionExampleTestBase
    {
        [TestMethod]
        public void AccountLastUpdatedDateTimeReset()
        {
            // ARRANGE
            var procedure = new AccountLastUpdatedDateTimeReset();

            // ACT
            Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            // Nothing to assert
        }
    }
}