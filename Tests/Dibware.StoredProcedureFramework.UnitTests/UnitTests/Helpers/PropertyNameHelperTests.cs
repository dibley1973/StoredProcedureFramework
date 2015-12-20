using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class PropertyNameHelperTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithNullProperty_ThrowsException()
        {
            // ARRANGE

            // ACT
            new PropertyNameHelper(null);

            // ASSERT
        }

        [TestMethod]
        public void Constructor_WhenGivenValidValue_DoesNotThrowException()
        {
            // ARRANGE
            TestObject testObject = new TestObject();
            Type testType = typeof (TestObject);

            PropertyInfo property = testType.GetProperty("Name");

            // ACT
            new PropertyNameHelper(property);

            // ASSERT
        }

        [TestMethod]
        public void Name_ForPropertyWithoutAttribute_ReturnsPropertyName()
        {
            // ARRANGE
            TestObject testObject = new TestObject();
            Type testType = typeof(TestObject);

            PropertyInfo property = testType.GetProperty("Name");

            // ACT
            var name = new PropertyNameHelper(property).Build().Name;

            // ASSERT
            Assert.AreEqual("Name", name);
        }

    }

    public class TestObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
