using Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Base;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Tests
{
    [TestClass]
    public class DynamicColumnStoredProcedure
        : SqlConnectionExampleTestBase
    {
        [TestMethod]
        public void GetDynamicColumnStoredProcedure()
        {
            // ARRANGE
            var procedure = new GetDynamicColumnStoredProcedure();

            // ACT
            var results = Connection.ExecuteStoredProcedure(procedure);
            var result = results.First();

            // ASSERT
            Assert.IsTrue(DynamicObjectHelper.HasProperty(result, "Firstname"));
            Assert.IsTrue(DynamicObjectHelper.HasProperty(result, "Surname"));
            Assert.IsTrue(DynamicObjectHelper.HasProperty(result, "Age"));
            Assert.IsTrue(DynamicObjectHelper.HasProperty(result, "DateOfBirth"));
            Assert.IsFalse(DynamicObjectHelper.HasProperty(result, "MiddleName"));

            var dynamicResult = (dynamic) result;
            Assert.AreEqual("Dave", dynamicResult.Firstname);
            Assert.AreEqual("Smith", dynamicResult.Surname);
            Assert.AreEqual(32, dynamicResult.Age);
        }
    }
}