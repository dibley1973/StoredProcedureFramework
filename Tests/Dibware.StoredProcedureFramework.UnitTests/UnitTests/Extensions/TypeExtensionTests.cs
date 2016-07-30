using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Dynamic;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Extensions
{
    [TestClass]
    public class TypeExtensionTests
    {
        #region MyRegion

        [TestMethod]
        public void IsGenericType_WhenGivenNonGenericType_ReturnsFalse()
        {
            // ARRANGE
            const string item = "hello";
            var type = item.GetType();

            // ACT
            var actual = type.IsGenericType();

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsGenericType_WhenGivenGenericType_ReturnsTrue()
        {
            // ARRANGE
            var item = new List<string>();
            var type = item.GetType();

            // ACT
            var actual = type.IsGenericType();

            // ASSERT
            Assert.IsTrue(actual);
        }

        #endregion

        #region GetGeneGetGenricArgumentCount

        [TestMethod]
        public void GetGenericArgumentCount_WhenGivenNonGenericArgument_ReturnsZero()
        {
            // ARRANGE
            const string item = "hello";
            var type = item.GetType();

            // ACT
            var actual = type.GetGeneGetGenricArgumentCount();

            // ASSERT
            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void GetGenericArgumentCount_WhenGivenTypeWithOneGenericArgument_ReturnsOne()
        {
            // ARRANGE
            var item = new List<string>();
            var type = item.GetType();

            // ACT
            var actual = type.GetGeneGetGenricArgumentCount();

            // ASSERT
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void GetGenericArgumentCount_WhenGivenNonGenericArguments_ReturnsTwo()
        {
            // ARRANGE
            var item = new Dictionary<int, string>();
            var type = item.GetType();

            // ACT
            var actual = type.GetGeneGetGenricArgumentCount();

            // ASSERT
            Assert.AreEqual(2, actual);
        }

        #endregion

        #region IsDynamicType

        [TestMethod]
        public void IsDynamicType_WhenGivenNonDynamicType_ReturnsFalse()
        {
            // ARRANGE
            const string item = "hello";
            var type = item.GetType();

            // ACT
            var actual = type.IsDynamicType();

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsDynamicType_WhenGivenDynamicType_ReturnsTrue()
        {
            // ARRANGE
            var item = new ExpandoObject();
            var type = item.GetType();

            // ACT
            var actual = type.IsDynamicType();

            // ASSERT
            Assert.IsTrue(actual);
        }

        #endregion

        #region IsGenericTypeWithFirstDynamicTypeArgument

        [TestMethod]
        public void IsGenericTypeWithFirstDynamicTypeArgument_WhenGivenNonGenericType_ReturnsFalse()
        {
            // ARRANGE
            const string item = "hello";
            var type = item.GetType();

            // ACT
            var actual = type.IsGenericTypeWithFirstDynamicTypeArgument();

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsGenericTypeWithFirstDynamicTypeArgument_WhenGivenGenericTypeWithNonDynamicType_ReturnsFalse()
        {
            // ARRANGE
            var item = new List<string>();
            var type = item.GetType();

            // ACT
            var actual = type.IsGenericTypeWithFirstDynamicTypeArgument();

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void IsGenericTypeWithFirstDynamicTypeArgument_WhenGivenGenericTypeWithDynamicType_ReturnsTrue()
        {
            // ARRANGE
            var item = new List<ExpandoObject>();
            var type = item.GetType();

            // ACT
            var actual = type.IsGenericTypeWithFirstDynamicTypeArgument();

            // ASSERT
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void IsGenericTypeWithFirstDynamicTypeArgument_WhenGivenGenericTypeWithMoreThanOneGenericType_ReturnsFalse()
        {
            // ARRANGE
            var item = new Dictionary<int, string>();
            var type = item.GetType();

            // ACT
            var actual = type.IsGenericTypeWithFirstDynamicTypeArgument();

            // ASSERT
            Assert.IsFalse(actual);
        }

        #endregion
    }
}
