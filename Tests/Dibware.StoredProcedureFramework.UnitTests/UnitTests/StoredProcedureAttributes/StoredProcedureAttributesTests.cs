using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedureAttributes
{
    [TestClass]
    public class StoredProcedureAttributesTests
    {
        #region ParameterDirection

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
    }
}