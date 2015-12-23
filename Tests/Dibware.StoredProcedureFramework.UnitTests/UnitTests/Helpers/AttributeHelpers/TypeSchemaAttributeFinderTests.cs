using System;
using System.Linq;
using Dibware.StoredProcedureFramework.Generics;
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
        public void HasAttribute_WhenCalledAfterInstantiationAndPropertyDoesNotHaveAtrribute_ReturnsFalse()
        {
            // ARRANGE
            Type testType = typeof(TestObject1);

            // ACT
            bool actual = new TypeSchemaAttributeFinder(testType).HasFoundAttribute;

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasAttribute_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsTrue()
        {
            // ARRANGE
            Type testType = typeof(TestObject2);

            // ACT
            bool actual = new TypeSchemaAttributeFinder(testType).HasFoundAttribute;

            // ASSERT
            Assert.IsTrue(actual);
        }

        #endregion

        #region AttributeFound

        [TestMethod]
        public void Attribute_WhenCalledAfterInstantiationAndPropertyDoesNotHaveAtrribute_ReturnsEmptyMaybe()
        {
            // ARRANGE
            Type testType = typeof(TestObject1);

            // ACT
            Maybe<SchemaAttribute> actual = new TypeSchemaAttributeFinder(testType).GetResult();

            // ASSERT
            Assert.IsNull(actual.FirstOrDefault());
        }

        [TestMethod]
        public void Attribute_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsMaybePopulatedWithInstanceOfAttribute()
        {
            // ARRANGE
            Type testType = typeof(TestObject2);

            // ACT
            Maybe<SchemaAttribute> actual = new TypeSchemaAttributeFinder(testType).GetResult();

            // ASSERT
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual.FirstOrDefault(), typeof(SchemaAttribute));
            Assert.AreEqual("log", actual.Single().Value);
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