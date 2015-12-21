using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class ListTypeUnderlyingTypeFinderTests
    {
        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithNullType_ThrowsException()
        {
            // ARRANGE

            // ACT
            new ListTypeUnderlyingTypeFinder(null);

            // ASSERT
        }

        [TestMethod]
        public void Constructor_WhenGivenNonListType_DoesWhat()
        {
            // ARRANGE
            Type testType = typeof(TestItem);

            // ACT
            new ListTypeUnderlyingTypeFinder(testType);

            // ASSERT
        }

        [TestMethod]
        public void Constructor_WhenGivenValidValue_DoesNotThrowException()
        {
            // ARRANGE
            Type testType = typeof(TestItemList);

            // ACT
            new ListTypeUnderlyingTypeFinder(testType);

            // ASSERT
        }

        #endregion

        #region HasFoundUnderlyingType

        [TestMethod]
        public void HasFoundUnderlyingType_WhenCalledAfterConstructionWithNonListType_ReturnsFalse()
        {
            // ARRANGE
            Type testType = typeof(TestItem);

            // ACT
            bool actual = new ListTypeUnderlyingTypeFinder(testType)
                .CheckForUnderlyingType()
                .HasFoundUnderlyingType;

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasFoundUnderlyingType_WhenCalledAfterConstructionWithValidListType_ReturnsTrue()
        {
            // ARRANGE
            Type testType = typeof(TestItemList);

            // ACT
            bool actual = new ListTypeUnderlyingTypeFinder(testType)
                .CheckForUnderlyingType()
                .HasFoundUnderlyingType;

            // ASSERT
            Assert.IsTrue(actual);
        }

        #endregion

        #region AttributeFound

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void UnderlyingTypeFound_WhenCalledAfterConstructionWithNonListType_ThrowsException()
        {
            // ARRANGE
            Type testType = typeof(TestItem);

            // ACT
            Type actual = new ListTypeUnderlyingTypeFinder(testType)
                .CheckForUnderlyingType()
                .UnderlyingTypeFound;

            // ASSERT
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void UnderlyingTypeFound_WhenCalledAfterConstructionWithValidListType_ReturnsCorrectType()
        {
            // ARRANGE
            Type testType = typeof(TestItemList);
            Type expectedUnderlyingType = typeof(TestItem);

            // ACT
            Type actual = new ListTypeUnderlyingTypeFinder(testType)
                .CheckForUnderlyingType()
                .UnderlyingTypeFound;

            // ASSERT
            Assert.AreEqual(expectedUnderlyingType, actual);
        }

        #endregion

        #region Mock Objects

        private class TestItem
        {
        }

        private class TestItemList : List<TestItem>
        {
        }

        #endregion
    }
}