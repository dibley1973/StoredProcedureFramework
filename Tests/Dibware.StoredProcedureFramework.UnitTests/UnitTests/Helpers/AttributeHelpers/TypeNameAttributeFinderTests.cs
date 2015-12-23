using Dibware.StoredProcedureFramework.Helpers.AttributeHelpers;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Dibware.StoredProcedureFramework.Generics;

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
        public void HasAttribute_WhenCalledAfterInstantiationAndPropertyDoesNotHaveAtrribute_ReturnsFalse()
        {
            // ARRANGE
            Type testType = typeof(TestObject1);

            // ACT
            bool actual = new TypeNameAttributeFinder(testType).HasFoundAttribute;

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasAttribute_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsTrue()
        {
            // ARRANGE
            Type testType = typeof(TestObject2);

            // ACT
            bool actual = new TypeNameAttributeFinder(testType).HasFoundAttribute;

            // ASSERT
            Assert.IsTrue(actual);
        }

        #endregion

        #region GetResult

        [TestMethod]
        public void GetResult_WhenCalledAfterInstantiationAndPropertyDoesNotHaveAtrribute_ReturnsEmptyMaybe()
        {
            // ARRANGE
            Type testType = typeof(TestObject1);

            // ACT
            Maybe<NameAttribute> actual = new TypeNameAttributeFinder(testType).GetResult();

            // ASSERT
            Assert.IsNull(actual.FirstOrDefault());
        }

        [TestMethod]
        public void GetResult_WhenCalledAfterInstantiationAndPropertyDoesHaveAtrribute_ReturnsMaybePopulatedWithInstanceOfAttribute()
        {
            // ARRANGE
            Type testType = typeof(TestObject2);

            // ACT
            Maybe<NameAttribute> actual = new TypeNameAttributeFinder(testType).GetResult();

            // ASSERT
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual.FirstOrDefault(), typeof(NameAttribute));
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