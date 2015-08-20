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
    }
}
