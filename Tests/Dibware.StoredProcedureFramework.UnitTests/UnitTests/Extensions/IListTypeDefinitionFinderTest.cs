using System;
using System.Collections;
using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Extensions
{
    [TestClass]
    public class IListTypeDefinitionFinderTest
    {
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

        [TestMethod]
        public void GetUnderlyingType_WhenInstanceIsValidList_ReturnsCorrectType()
        {
            // ARRANGE
            var list = (IList)new List<ListItem>
            {
                new ListItem {}
            };

            // ACT
            var actual = new IListTypeDefinitionFinder(list)
                .GenericListType;

            // ASSERT
            Assert.AreEqual(typeof(ListItem), actual);
        }

        private class ListItem
        {
        }
    }
}
