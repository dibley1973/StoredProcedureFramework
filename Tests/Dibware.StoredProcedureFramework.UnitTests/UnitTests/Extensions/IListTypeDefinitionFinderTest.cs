using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Extensions
{
    [TestClass]
    public class IListTypeDefinitionFinderTest
    {
        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenInstanceIsNull_ThrowsException()
        {
            // ARRANGE
            IList list = null;

            // ACT
            new IListTypeDefinitionFinder(list);

            // ASSERT
        }

        #endregion

        #region GetUnderlyingType

        [TestMethod]
        public void GetUnderlyingType_WhenInstanceIsValidList_ReturnsCorrectType()
        {
            // ARRANGE
            var list = (IList)new TestItemList
            {
                new TestItem ()
            };

            // ACT
            var actual = new IListTypeDefinitionFinder(list)
                .GenericListTypeFound;

            // ASSERT
            Assert.AreEqual(typeof(TestItem), actual);
        }

        #endregion

        #region HasFoundGenericListTypeFound

        [TestMethod]
        public void HasFoundUnderlyingType_WhenCalledAfterConstructionWithValidListType_ReturnsTrue()
        {
            // ARRANGE
            var list = (IList)new TestItemList
            {
                new TestItem ()
            };

            // ACT
            bool actual = new IListTypeDefinitionFinder(list)
                .HasFoundGenericListTypeFound;

            // ASSERT
            Assert.IsTrue(actual);
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
