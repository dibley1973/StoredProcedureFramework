using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

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

            PropertyInfo property = testType.GetProperty("Name1");

            // ACT
            new PropertyNameHelper(property);

            // ASSERT
        }

        [TestMethod]
        public void Name_ForPropertyWithoutAttribute_ReturnsPropertyName()
        {
            // ARRANGE
            const string expectedPropertyName = "Name1";
            TestObject testObject = new TestObject();
            Type testType = typeof(TestObject);

            PropertyInfo property = testType.GetProperty("Name1");

            // ACT
            var name = new PropertyNameHelper(property).Build().Name;

            // ASSERT
            Assert.AreEqual(expectedPropertyName, name);
        }

        [TestMethod]
        public void Name_ForPropertyWithAttribute_ReturnsAttributeValue()
        {
            // ARRANGE
            const string expectedPropertyName = "Address";
            TestObject testObject = new TestObject();
            Type testType = typeof(TestObject);

            PropertyInfo property = testType.GetProperty("Name2");

            // ACT
            var name = new PropertyNameHelper(property).Build().Name;

            // ASSERT
            Assert.AreEqual(expectedPropertyName, name);
        }

    }

    public class TestObject
    {
        public string Name1 { get; set; }
        [Name("Address")]
        public string Name2 { get; set; }
    }
}
