using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class I010_IssueNumberTen_TestsDynamicColumnsRequest
        : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        public void IssueNumberTenTests_ReturnsResults_AfterCallingStoreProcedure()
        {
            // ARRANGE
            var procedure = new DynamicColumnStoredProcedure();

            // ACT
            var results = Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.IsTrue(results.Any());
        }

        [TestMethod]
        public void IssueNumberTenTests_DynamicObjectHasAllCorrectFields_AfterCallingStoreProcedure()
        {
            // ARRANGE
            var procedure = new DynamicColumnStoredProcedure();

            // ACT
            var results = Connection.ExecuteStoredProcedure(procedure);
            var result = results.First();

            // ASSERT
            Assert.IsTrue(DynamicObjectHelper.HasProperty(result, "Firstname"));
            Assert.IsTrue(DynamicObjectHelper.HasProperty(result, "Surname"));
            Assert.IsTrue(DynamicObjectHelper.HasProperty(result, "Age"));
            Assert.IsTrue(DynamicObjectHelper.HasProperty(result, "DateOfBirth"));

            var dynamicResult = (dynamic)result;
            Assert.AreEqual("Dave", dynamicResult.Firstname);
            Assert.AreEqual("Smith", dynamicResult.Surname);
            Assert.AreEqual(32, dynamicResult.Age);
        }

        [TestMethod]
        public void IssueNumberTenTests_DynamicObjectHasNoFicticiousCorrectFields_AfterCallingStoreProcedure()
        {
            // ARRANGE
            var procedure = new DynamicColumnStoredProcedure();

            // ACT
            var results = Connection.ExecuteStoredProcedure(procedure);
            var result = results.First();

            // ASSERT
            var hasProperty = DynamicObjectHelper.HasProperty(result, "MiddleName");
            Assert.IsFalse(hasProperty);
        }
    }
}