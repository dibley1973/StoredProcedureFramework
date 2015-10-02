using Dibware.StoredProcedureFramework.DataInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.DataInfo
{
    [TestClass]
    public class DecimalInfoTests
    {
        [TestMethod]
        public void ConstructionWithPrecisionScaleAndTrailingZeros_ResultsInCorrectFieldValues()
        {
            // ARRANGE
            const int expectedPrecision = 6;
            const int expectedScale = 4;
            const int expectedTrailingZeros = 2;

            // ACT
            var decimalInfo = new DecimalInfo(
                expectedPrecision,
                expectedScale,
                expectedTrailingZeros
            );

            // ASSERT
            Assert.AreEqual(expectedPrecision, decimalInfo.Precision);
            Assert.AreEqual(expectedScale, decimalInfo.Scale);
            Assert.AreEqual(expectedTrailingZeros, decimalInfo.TrailingZeros);
        }

        [TestMethod]
        public void FromDecimalWithFractionAndTrailingZeros_ResultsInCorrectFieldValues()
        {
            // ARRANGE
            const decimal initialValue = 999.8800M;
            const int expectedPrecision = 7;
            const int expectedScale = 4;
            const int expectedTrailingZeros = 2;

            // ACT
            var decimalInfo = DecimalInfo.FromDecimal(initialValue);

            // ASSERT
            Assert.AreEqual(expectedPrecision, decimalInfo.Precision);
            Assert.AreEqual(expectedScale, decimalInfo.Scale);
            Assert.AreEqual(expectedTrailingZeros, decimalInfo.TrailingZeros);
        }

        [TestMethod]
        public void FromNegativeDecimalWithFractionAndTrailingZeros_ResultsInCorrectFieldValues()
        {
            // ARRANGE
            const decimal initialValue = -999.8800M;
            const int expectedPrecision = 7;
            const int expectedScale = 4;
            const int expectedTrailingZeros = 2;

            // ACT
            var decimalInfo = DecimalInfo.FromDecimal(initialValue);

            // ASSERT
            Assert.AreEqual(expectedPrecision, decimalInfo.Precision);
            Assert.AreEqual(expectedScale, decimalInfo.Scale);
            Assert.AreEqual(expectedTrailingZeros, decimalInfo.TrailingZeros);
        }

        [TestMethod]
        public void FromDecimalWithFractionAndNoTrailingZeros_ResultsInCorrectFieldValues()
        {
            // ARRANGE
            const decimal initialValue = 999.88M;
            const int expectedPrecision = 5;
            const int expectedScale = 2;
            const int expectedTrailingZeros = 0;

            // ACT
            var decimalInfo = DecimalInfo.FromDecimal(initialValue);

            // ASSERT
            Assert.AreEqual(expectedPrecision, decimalInfo.Precision);
            Assert.AreEqual(expectedScale, decimalInfo.Scale);
            Assert.AreEqual(expectedTrailingZeros, decimalInfo.TrailingZeros);
        }

        [TestMethod]
        public void FromNegativeDecimalWithFractionAndNoTrailingZeros_ResultsInCorrectFieldValues()
        {
            // ARRANGE
            const decimal initialValue = -999.88M;
            const int expectedPrecision = 5;
            const int expectedScale = 2;
            const int expectedTrailingZeros = 0;

            // ACT
            var decimalInfo = DecimalInfo.FromDecimal(initialValue);

            // ASSERT
            Assert.AreEqual(expectedPrecision, decimalInfo.Precision);
            Assert.AreEqual(expectedScale, decimalInfo.Scale);
            Assert.AreEqual(expectedTrailingZeros, decimalInfo.TrailingZeros);
        }

        [TestMethod]
        public void FromDecimalWithJustTrailingZeros_ResultsInCorrectFieldValues()
        {
            // ARRANGE
            const decimal initialValue = 999.000M;
            const int expectedPrecision = 6;
            const int expectedScale = 3;
            const int expectedTrailingZeros = 3;

            // ACT
            var decimalInfo = DecimalInfo.FromDecimal(initialValue);

            // ASSERT
            Assert.AreEqual(expectedPrecision, decimalInfo.Precision);
            Assert.AreEqual(expectedScale, decimalInfo.Scale);
            Assert.AreEqual(expectedTrailingZeros, decimalInfo.TrailingZeros);
        }

        [TestMethod]
        public void FromNegativeDecimalWithJustTrailingZeros_ResultsInCorrectFieldValues()
        {
            // ARRANGE
            const decimal initialValue = -999.000M;
            const int expectedPrecision = 6;
            const int expectedScale = 3;
            const int expectedTrailingZeros = 3;

            // ACT
            var decimalInfo = DecimalInfo.FromDecimal(initialValue);

            // ASSERT
            Assert.AreEqual(expectedPrecision, decimalInfo.Precision);
            Assert.AreEqual(expectedScale, decimalInfo.Scale);
            Assert.AreEqual(expectedTrailingZeros, decimalInfo.TrailingZeros);
        }

        [TestMethod]
        public void FromDecimalWithNoFractionAndNoTrailingZeros_ResultsInCorrectFieldValues()
        {
            // ARRANGE
            const decimal initialValue = 999888M;
            const int expectedPrecision = 6;
            const int expectedScale = 0;
            const int expectedTrailingZeros = 0;

            // ACT
            var decimalInfo = DecimalInfo.FromDecimal(initialValue);

            // ASSERT
            Assert.AreEqual(expectedPrecision, decimalInfo.Precision);
            Assert.AreEqual(expectedScale, decimalInfo.Scale);
            Assert.AreEqual(expectedTrailingZeros, decimalInfo.TrailingZeros);
        }


        [TestMethod]
        public void FromNegativeDecimalWithNoFractionAndNoTrailingZeros_ResultsInCorrectFieldValues()
        {
            // ARRANGE
            const decimal initialValue = -999888M;
            const int expectedPrecision = 6;
            const int expectedScale = 0;
            const int expectedTrailingZeros = 0;

            // ACT
            var decimalInfo = DecimalInfo.FromDecimal(initialValue);

            // ASSERT
            Assert.AreEqual(expectedPrecision, decimalInfo.Precision);
            Assert.AreEqual(expectedScale, decimalInfo.Scale);
            Assert.AreEqual(expectedTrailingZeros, decimalInfo.TrailingZeros);
        }

        [TestMethod]
        public void FromDecimalWithNoFractionWithTrailingZeros_ResultsInCorrectFieldValues()
        {
            // ARRANGE
            const decimal initialValue = 999000M;
            const int expectedPrecision = 6;
            const int expectedScale = 0;
            const int expectedTrailingZeros = 0;

            // ACT
            var decimalInfo = DecimalInfo.FromDecimal(initialValue);

            // ASSERT
            Assert.AreEqual(expectedPrecision, decimalInfo.Precision);
            Assert.AreEqual(expectedScale, decimalInfo.Scale);
            Assert.AreEqual(expectedTrailingZeros, decimalInfo.TrailingZeros);
        }

        [TestMethod]
        public void FromNegativeDecimalWithNoFractionWithTrailingZeros_ResultsInCorrectFieldValues()
        {
            // ARRANGE
            const decimal initialValue = -999000M;
            const int expectedPrecision = 6;
            const int expectedScale = 0;
            const int expectedTrailingZeros = 0;

            // ACT
            var decimalInfo = DecimalInfo.FromDecimal(initialValue);

            // ASSERT
            Assert.AreEqual(expectedPrecision, decimalInfo.Precision);
            Assert.AreEqual(expectedScale, decimalInfo.Scale);
            Assert.AreEqual(expectedTrailingZeros, decimalInfo.TrailingZeros);
        }

        [TestMethod]
        public void FromDecimalWithNoFractionAndLeadingZeros_ResultsInCorrectFieldValues()
        {
            // ARRANGE
            const decimal initialValue = 00999888M;
            const int expectedPrecision = 6;
            const int expectedScale = 0;
            const int expectedTrailingZeros = 0;

            // ACT
            var decimalInfo = DecimalInfo.FromDecimal(initialValue);

            // ASSERT
            Assert.AreEqual(expectedPrecision, decimalInfo.Precision);
            Assert.AreEqual(expectedScale, decimalInfo.Scale);
            Assert.AreEqual(expectedTrailingZeros, decimalInfo.TrailingZeros);
        }

        [TestMethod]
        public void FromNegativeDecimalWithNoFractionAndLeadingZeros_ResultsInCorrectFieldValues()
        {
            // ARRANGE
            const decimal initialValue = -00999888M;
            const int expectedPrecision = 6;
            const int expectedScale = 0;
            const int expectedTrailingZeros = 0;

            // ACT
            var decimalInfo = DecimalInfo.FromDecimal(initialValue);

            // ASSERT
            Assert.AreEqual(expectedPrecision, decimalInfo.Precision);
            Assert.AreEqual(expectedScale, decimalInfo.Scale);
            Assert.AreEqual(expectedTrailingZeros, decimalInfo.TrailingZeros);
        }

        #region Precsion

        [TestMethod]
        public void Preecision_WhenDecimalIsAllZeros_ReturnsCorrectCount()
        {
            // ARRANGE
            const decimal value = 0.0000M;
            var decimalnfo = DecimalInfo.FromDecimal(value);
            const int expectedResult = 5;

            // ACT
            var actualResult = decimalnfo.Precision;

            // ASSERT
            Assert.AreEqual(expectedResult, actualResult);
        }

        #endregion

        #region Scale

        [TestMethod]
        public void Scale_WhenDecimalIsAllZeros_ReturnscorrectCount()
        {
            // ARRANGE
            const decimal value = 0.0000M;
            var decimalnfo = DecimalInfo.FromDecimal(value);
            const int expectedResult = 4;

            // ACT
            var actualResult = decimalnfo.Scale;

            // ASSERT
            Assert.AreEqual(expectedResult, actualResult);
        }

        #endregion

        #region TrailingZeros

        [TestMethod]
        public void TrailingZeros_WhenDecimalHasNoTrailingZeros_ReturnsZeroCount()
        {
            // ARRANGE
            const decimal value = 1234.456M;
            var decimalnfo = DecimalInfo.FromDecimal(value);
            const int expectedResult = 0;

            // ACT
            var actualResult = decimalnfo.TrailingZeros;

            // ASSERT
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TrailingZeros_WhenDecimalHasTrailingZeros_ReturnsCorrectCount()
        {
            // ARRANGE

            const decimal value = 1234.400M;
            var decimalnfo = DecimalInfo.FromDecimal(value);
            const int expectedResult = 2;

            // ACT
            var actualResult = decimalnfo.TrailingZeros;

            // ASSERT
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TrailingZeros_WhenDecimalIsAllZeros_ReturnsZeroCount()
        {
            // ARRANGE
            const decimal value = 0.0000M;
            var decimalnfo = DecimalInfo.FromDecimal(value);
            const int expectedResult = 4;

            // ACT
            var actualResult = decimalnfo.TrailingZeros;

            // ASSERT
            Assert.AreEqual(expectedResult, actualResult);
        }

        #endregion
    }
}
