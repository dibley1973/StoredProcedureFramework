//using Dibware.StoredProcedureFramework.Generics;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Generics
//{
//    [TestClass]
//    public class MaybeOfT2TestsTests
//    {
//        #region ToMaybe

//        [TestMethod]
//        public void ToMaybe_WhenGivenNullValue_ReturnsNothingOfT()
//        {
//            // ARRANGE
//            const string value = null;

//            // ACT
//            var actual = value.ToMaybe();

//            // ASSERT
//            Assert.IsInstanceOfType(actual, typeof(Nothing<string>));
//        }

//        [TestMethod]
//        public void ToMaybe_WhenGivenNonNullValue_ReturnsJustOfT()
//        {
//            // ARRANGE
//            const string value = "Fred";

//            // ACT
//            var actual = value.ToMaybe();

//            // ASSERT
//            Assert.IsInstanceOfType(actual, typeof(Just<string>));
//        }

//        #endregion

//        #region Or

//        [TestMethod]
//        public void Or_WhenFirstAndSecondIsConstructedWithZeroElements_ReturnsNothing()
//        {
//            // ARRANGE
//            const string value1 = null;
//            const string value2 = null;
//            var maybe1 = value1.ToMaybe();
//            var maybe2 = value2.ToMaybe();

//            // ACT
//            IMaybe<string> actual = maybe1.Or(maybe2);

//            // ASSERT
//            Assert.IsInstanceOfType(actual, typeof(Nothing<string>));
//        }

//        [TestMethod]
//        public void Or_WhenFirstIsConstructedWithOneElementAndSecondIsConstructedWithZeroElements_ReturnsFirst()
//        {
//            // ARRANGE
//            const string value1 = "Bill";
//            const string value2 = null;
//            var maybe1 = value1.ToMaybe();
//            var maybe2 = value2.ToMaybe();

//            // ACT
//            IMaybe<string> actual = maybe1.Or(maybe2);

//            // ASSERT
//            Assert.IsInstanceOfType(actual, typeof(Just<string>));
//            Assert.AreEqual(((Just<string>)actual).Value, value1);
//        }

//        [TestMethod]
//        public void Or_WhenFirstIsConstructedWithZeroElementsAndSecondIsConstructedWithOneElement_ReturnsSecond()
//        {
//            // ARRANGE
//            const string value1 = null;
//            const string value2 = "Ted";
//            var maybe1 = value1.ToMaybe();
//            var maybe2 = value2.ToMaybe();

//            // ACT
//            var actual = maybe1.Or(maybe2);

//            // ASSERT
//            Assert.IsInstanceOfType(actual, typeof(Just<string>));
//            Assert.AreEqual(((Just<string>)actual).Value, value2);
//        }

//        [TestMethod]
//        public void Or_WhenFirstAndSecondIsConstructedWithOneElement_ReturnsFirst()
//        {
//            // ARRANGE
//            const string value1 = "Bill";
//            const string value2 = "Ted";
//            var maybe1 = value1.ToMaybe();
//            var maybe2 = value2.ToMaybe();

//            // ACT
//            var actual = maybe1.Or(maybe2);

//            // ASSERT
//            Assert.IsInstanceOfType(actual, typeof(Just<string>));
//            Assert.AreEqual(((Just<string>)actual).Value, value1);
//        }

//        #endregion
//    }
//}
