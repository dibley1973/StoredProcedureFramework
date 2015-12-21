using System;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedureAttributes
{
    [TestClass]
    public class StoredProcedureAttributesTests
    {
        #region DirectionAttribute

        [TestMethod]
        public void Value_WhenDirectionAttributeConstructedWithValidValue_ReturnsConstructedValue()
        {
            // ARRANGE
            const ParameterDirection expectedValue = ParameterDirection.InputOutput;

            // ACT
            var directionAttribute = new DirectionAttribute(expectedValue);
            var actualValue = directionAttribute.Value;

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
        }

        #endregion

        #region NameAttribute

        [TestMethod]
        public void Value_WhenNameAttributeConstructedWithValidValue_ReturnsConstructedValue()
        {
            // ARRANGE
            const string expectedValue = "Test";

            // ACT
            var directionAttribute = new NameAttribute(expectedValue);
            var actualValue = directionAttribute.Value;

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
        }

        #endregion

        #region DbTypeAttribute

        [TestMethod]
        public void Value_WhenParameterDbTypeAttributeConstructedWithValidValue_ReturnsConstructedValue()
        {
            // ARRANGE
            const SqlDbType expectedValue = SqlDbType.DateTimeOffset;

            // ACT
            var directionAttribute = new DbTypeAttribute(expectedValue);
            var actualValue = directionAttribute.Value;

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
        }

        #endregion

        #region PrecisionAttribute

        [TestMethod]
        public void Value_WhenPrecisionAttributeConstructedWithValidValue_ReturnsConstructedValue()
        {
            // ARRANGE
            const Byte expectedValue = 36;

            // ACT
            var directionAttribute = new PrecisionAttribute(expectedValue);
            var actualValue = directionAttribute.Value;

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
        }

        #endregion

        #region ScaleAttribute

        [TestMethod]
        public void Value_WhenScaleAttributeConstructedWithValidValue_ReturnsConstructedValue()
        {
            // ARRANGE
            const Byte expectedValue = 42;

            // ACT
            var directionAttribute = new ScaleAttribute(expectedValue);
            var actualValue = directionAttribute.Value;

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
        }

        #endregion

        #region SchemaAttribute

        [TestMethod]
        public void Value_WhenSchemaAttributeConstructedWithValidValue_ReturnsConstructedValue()
        {
            // ARRANGE
            const string expectedValue = "agent";

            // ACT
            var directionAttribute = new SchemaAttribute(expectedValue);
            var actualValue = directionAttribute.Value;

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
        }

        #endregion

        #region SizeAttribute

        [TestMethod]
        public void Value_WhenSizeAttributeConstructedWithValidValue_ReturnsConstructedValue()
        {
            // ARRANGE
            const Byte expectedValue = 42;

            // ACT
            var directionAttribute = new SizeAttribute(expectedValue);
            var actualValue = directionAttribute.Value;

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
        }

        #endregion


    }
}