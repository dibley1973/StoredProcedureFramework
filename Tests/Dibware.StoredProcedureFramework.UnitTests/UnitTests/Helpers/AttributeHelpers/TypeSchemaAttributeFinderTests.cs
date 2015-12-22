using System;
using Dibware.StoredProcedureFramework.Helpers.AttributeHelpers;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers.AttributeHelpers
{
    [TestClass]
    public class TypeSchemaAttributeFinderTests
    {
        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithNullProperty_ThrowsException()
        {
            // ARRANGE

            // ACT
            new TypeSchemaAttributeFinder(null);

            // ASSERT
        }

        [TestMethod]
        public void Constructor_WhenGivenValidValue_DoesNotThrowException()
        {
            // ARRANGE
            Type testType = typeof(TestObject1);

            // ACT
            new TypeSchemaAttributeFinder(testType);

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
            bool actual = new TypeSchemaAttributeFinder(testType)
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
            bool actual = new TypeSchemaAttributeFinder(testType)
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
            SchemaAttribute actual = new TypeSchemaAttributeFinder(testType)
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
            SchemaAttribute actual = new TypeSchemaAttributeFinder(testType)
                .DetectAttribute()
                .AttributeFound;

            // ASSERT
            Assert.IsNotNull(actual);
            Assert.AreEqual("log", actual.Value);
        }

        #endregion

        #region Mock object

        private class TestObject1
        {
        }

        [Schema("log")]
        private class TestObject2
        {
        }

        #endregion
    }
}