using System;
using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class SqlParameterHelperTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateSqlParametersFromPropertyInfoArray_ThrowsArgumentNullException_WhenCalledWithNullpropertyInforArray()
        {
            // ACT
            SqlParameterHelper.CreateSqlParametersFromPropertyInfoArray(null);
        }
    }
}
