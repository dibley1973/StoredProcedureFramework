using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Linq;
using Dibware.StoredProcedureFramework.Generics;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Generics
{
    [TestClass]
    public class MaybeOfTTests
    {
        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenConstructedWithNullElements_ThrowsException()
        {
            // ARRANGE
            new Maybe<string>(null);

            // ACT

            // ASSERT

        }

        [TestMethod]
        public void WhenConstructedWithNoElements_ReturnsZeroCount()
        {
            // ARRANGE
            const int expctedCount = 0;
            var maybe = new Maybe<string>();

            // ACT
            var actualCount = maybe.Count();

            // ASSERT
            Assert.AreEqual(expctedCount, actualCount);
        }

        [TestMethod]
        public void WhenConstructedWithOneElement_ReturnsCountOf1()
        {
            // ARRANGE
            const int expctedCount = 1;
            var maybe = new Maybe<string>("Bill");

            // ACT
            var actualCount = maybe.Count();

            // ASSERT
            Assert.AreEqual(expctedCount, actualCount);
            Assert.AreEqual("Bill", maybe.Single());
        }

        [TestMethod]
        public void WhenConstructedWithOneElement_ReturnsSameItem()
        {
            // ARRANGE
            const string expctedValue = "Bill";
            var maybe = new Maybe<string>(expctedValue);

            // ACT
            var actual = maybe.Single();

            // ASSERT
            Assert.AreSame(expctedValue, actual);
        }

        #endregion

        #region HasItem

        [TestMethod]
        public void HasItem_WhenConstructedWithZeroElements_ReturnsFalse()
        {
            // ARRANGE
            var maybe = new Maybe<string>();

            // ACT
            var actual = maybe.HasItem;

            // ASSERT
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void HasItem_WhenConstructedWithOneElement_ReturnsTrue()
        {
            // ARRANGE
            const string expctedValue = "Bill";
            var maybe = new Maybe<string>(expctedValue);

            // ACT
            var actual = maybe.HasItem;

            // ASSERT
            Assert.IsTrue(actual);
        }

        #endregion

        #region Or

        [TestMethod]
        public void Or_WhenFirstAndSecondIsConstructedWithZeroElements_ReturnsSecond()
        {
            // ARRANGE
            var maybe1 = new Maybe<string>();
            var maybe2 = new Maybe<string>();

            // ACT
            var actual = maybe1.Or(maybe2);

            // ASSERT
            Assert.AreNotSame(maybe1, actual);
            Assert.AreSame(maybe2, actual);
        }

        [TestMethod]
        public void Or_WhenFirstIsConstructedWithOneElementAndSecondIsConstructedWithZeroElements_ReturnsFirst()
        {
            // ARRANGE
            var maybe1 = new Maybe<string>("Bill");
            var maybe2 = new Maybe<string>();

            // ACT
            var actual = maybe1.Or(maybe2);

            // ASSERT
            Assert.AreSame(maybe1, actual);
            Assert.AreNotSame(maybe2, actual);
        }

        [TestMethod]
        public void Or_WhenFirstIsConstructedWithZeroElementsAndSecondIsConstructedWithOneElement_ReturnsSecond()
        {
            // ARRANGE
            var maybe1 = new Maybe<string>();
            var maybe2 = new Maybe<string>("Ted");

            // ACT
            var actual = maybe1.Or(maybe2);

            // ASSERT
            Assert.AreNotSame(maybe1, actual);
            Assert.AreSame(maybe2, actual);
        }

        [TestMethod]
        public void Or_WhenFirstAndSecondIsConstructedWithOneElement_ReturnsFirst()
        {
            // ARRANGE
            var maybe1 = new Maybe<string>("Bill");
            var maybe2 = new Maybe<string>("Ted");

            // ACT
            var actual = maybe1.Or(maybe2);

            // ASSERT
            Assert.AreSame(maybe1, actual);
            Assert.AreNotSame(maybe2, actual);
        }

        #endregion

        #region ToMaybe

        [TestMethod]
        public void ToMaybe_WhenSuppliedWithNull_ReturnsEmptyMaybe()
        {
            // ARRANGE
            const string value = null;
            const int expctedCount = 0;
            var maybe = Maybe<string>.ToMaybe(value);

            // ACT
            var actualCount = maybe.Count();

            // ASSERT
            Assert.AreEqual(expctedCount, actualCount);
        }

        [TestMethod]
        public void ToMaybe_WhenSuppliedWithNonNull_ReturnsMaybeWithOne()
        {
            // ARRANGE
            const string value = "Jane";
            const int expctedCount = 1;
            var maybe = Maybe<string>.ToMaybe(value);

            // ACT
            var actualCount = maybe.Count();

            // ASSERT
            Assert.AreEqual(expctedCount, actualCount);
        }

        #endregion

        #region IEnumerable.GetEnumerator

        [TestMethod]
        public void GetEnumerator_AfterContructionWithValidValue_CorrectlyIteratesThroughAllElements()
        {
            // ARRANGE
            const string expectedValue = "ValidValue";
            var maybe = Maybe<string>.ToMaybe(expectedValue);
            IEnumerator iterator = ((IEnumerable)maybe).GetEnumerator();

            // ACT
            iterator.MoveNext();
            var actual = iterator.Current;

            // ASSERT
            Assert.AreEqual(expectedValue, actual);
        }

        #endregion
    }
}
