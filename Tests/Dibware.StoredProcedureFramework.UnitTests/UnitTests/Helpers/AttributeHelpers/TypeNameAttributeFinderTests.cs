using Dibware.StoredProcedureFramework.Helpers.AttributeHelpers;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers.AttributeHelpers
{
    [TestClass]
    public class TypeNameAttributeFinderTests
    {
        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithNullProperty_ThrowsException()
        {
            // ARRANGE

            // ACT
            new TypeNameAttributeFinder(null);

            // ASSERT
        }

        [TestMethod]
        public void Constructor_WhenGivenValidValue_DoesNotThrowException()
        {
            // ARRANGE
            Type testType = typeof(TestObject1);

            // ACT
            new TypeNameAttributeFinder(testType);

            // ASSERT
        }

        #endregion

        #region HasFoundAttribute

        [TestMethod]
        public void HasAttribute_WhenCalledAfterCheckAttributeAndPropertyDoesNotHaveAtrribute_ReturnsFalse()
        {
            // ARRANGE
            Type testType = typeof(TestObject1);

            // ACT
            bool actual = new TypeNameAttributeFinder(testType)
                .DetectAttribute()
                .HasFoundAttribute;

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasAttribute_WhenCalledAfterCheckAttributeAndPropertyDoesHaveAtrribute_ReturnsTrue()
        {
            // ARRANGE
            Type testType = typeof(TestObject2);

            // ACT
            bool actual = new TypeNameAttributeFinder(testType)
                .DetectAttribute()
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
            Type testType = typeof(TestObject1);

            // ACT
            NameAttribute actual = new TypeNameAttributeFinder(testType)
                .DetectAttribute()
                .AttributeFound;

            // ASSERT
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Attribute_WhenCalledAfterCheckAttributeAndPropertyDoesHaveAtrribute_ReturnsInstanceOfAttribute()
        {
            // ARRANGE
            Type testType = typeof(TestObject2);

            // ACT
            NameAttribute actual = new TypeNameAttributeFinder(testType)
                .DetectAttribute()
                .AttributeFound;

            // ASSERT
            Assert.IsNotNull(actual);
        }

        #endregion

        #region Mock object

        private class TestObject1
        {
        }

        [Name("Address")]
        private class TestObject2
        {
        }

        #endregion
    }
}