using System;
using System.Reflection;
using Dibware.StoredProcedureFramework.Helpers.AttributeHelpers;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers.AttributeHelpers
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

        #region HasFoundAttribute

        [TestMethod]
        public void HasAttribute_WhenCalledAfterCheckAttributeAndPropertyDoesNotHaveAtrribute_ReturnsFalse()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name1");
            
            // ACT
            bool actual = new PropertyNameAttributeFinder(property)
                .CheckForAttribute()
                .HasFoundAttribute;

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
                .HasFoundAttribute;

            // ASSERT
            Assert.IsTrue(actual);
        }

        #endregion

        #region AttributeFound

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Attribute_WhenCalledAfterCheckAttributeAndPropertyDoesNotHaveAtrribute_ThrowsException()
        {
            // ARRANGE
            Type testType = typeof(TestObject);
            PropertyInfo property = testType.GetProperty("Name1");

            // ACT
            NameAttribute actual = new PropertyNameAttributeFinder(property)
                .CheckForAttribute()
                .AttributeFound;

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
                .AttributeFound;

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