using System;
using System.Dynamic;
using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class DynamicObjectHelperTests
    {
        [TestMethod]
        public void DynamicObjectHelper_WhenGivenExpandoObjectWithNoPropertiesAndPropertyName_ReturnsFalse()
        {
            // ARRANGE
            dynamic expando = new ExpandoObject();
            const string property = "FirstName";

            // ACT
            var actual = DynamicObjectHelper.HasProperty(expando, property);

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void DynamicObjectHelper_WhenGivenExpandoObjectWithNoneExistentPropertiesAndPropertyName_ReturnsFalse()
        {
            // ARRANGE
            dynamic expando = new ExpandoObject();
            expando.LastName = "Flintstone";

            const string property = "FirstName";

            // ACT
            var actual = DynamicObjectHelper.HasProperty(expando, property);

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void DynamicObjectHelper_WhenGivenExpandoObjectWithExistingPropertiesAndPropertyName_ReturnsTrue()
        {
            // ARRANGE
            dynamic expando = new ExpandoObject();
            const string property = "FirstName";
            expando.FirstName = "Fred";

            // ACT
            var actual = DynamicObjectHelper.HasProperty(expando, property);

            // ASSERT
            Assert.IsTrue(actual);
        }        
    }
}
