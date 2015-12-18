using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Extensions
{
    [TestClass]
    public class IListExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetUnderlyingType_WhenInstanceIsNull_ThrowsException()
        {
            // ARRANGE
            IList list = null;

            // ACT
            list.GetUnderlyingType();

            // ASSERT
        }

        [TestMethod]
        public void GetUnderlyingType_WhenInstanceIsValidList_ReturnsCorrectType()
        {
            // ARRANGE
            var list = (IList)new List<ListItem>
            {
                new ListItem {Id = 1, Name = "Brucie"}
            };

            // ACT
            var actual = list.GetUnderlyingType();

            // ASSERT
            Assert.AreEqual(typeof(ListItem), actual);
        }

        private class ListItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
