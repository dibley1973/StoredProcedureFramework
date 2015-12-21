using System;
using System.Reflection;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class PropertyNameAttributeFinderTests
    {
        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithNullProperty_ThrowsException()
        {
            // ARRANGE

            // ACT
            new PropertyNameAttributeFinder(null);

            // ASSERT
        }

        [TestMethod]
        public void Constructor_WhenGivenValidValue_DoesNotThrowException()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name1");

            // ACT
            new PropertyNameAttributeFinder(property);

            // ASSERT
        }

        #endregion

        #region HastAttribute

        [TestMethod]
        public void HasAttribute_WhenCalledAfterCheckAttributeAndPropertyDoesNotHaveAtrribute_ReturnsFalse()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name1");
            
            // ACT
            bool actual = new PropertyNameAttributeFinder(property)
                .CheckForAttribute()
                .HasAttribute;

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasAttribute_WhenCalledAfterCheckAttributeAndPropertyDoesHaveAtrribute_ReturnsTrue()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name2");

            // ACT
            bool actual = new PropertyNameAttributeFinder(property)
                .CheckForAttribute()
                .HasAttribute;

            // ASSERT
            Assert.IsTrue(actual);
        }

        #endregion

        #region Attribute

        [TestMethod]
        public void Attribute_WhenCalledAfterCheckAttributeAndPropertyDoesNotHaveAtrribute_ReturnsNull()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name1");

            // ACT
            NameAttribute actual = new PropertyNameAttributeFinder(property)
                .CheckForAttribute()
                .Attribute;

            // ASSERT
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Attribute_WhenCalledAfterCheckAttributeAndPropertyDoesHaveAtrribute_ReturnsInstanceOfAttribute()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name2");

            // ACT
            NameAttribute actual = new PropertyNameAttributeFinder(property)
                .CheckForAttribute()
                .Attribute;

            // ASSERT
            Assert.IsNotNull(actual);
        }

        #endregion

        #region Mock object

        public class TestObject
        {
            public string Name1 { get; set; }
            [Name("Address")]
            public string Name2 { get; set; }
        }

        #endregion
    }
}