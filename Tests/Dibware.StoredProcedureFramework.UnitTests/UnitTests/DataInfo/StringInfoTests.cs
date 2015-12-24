using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringInfo = Dibware.StoredProcedureFramework.DataInfo.StringInfo;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.DataInfo
{
    [TestClass]
    public class StringInfoTests
    {
        [TestMethod]
        public void IsNull_WhenStringHasValue_ReturnsFalse()
        {
            // ARRANGE
            const string value = "TestString";
            var stringInfo = StringInfo.FromString(value);

            // ACT
            var actualResult = stringInfo.IsNull;

            // ASSERT
            Assert.IsFalse(actualResult);
        }

        [TestMethod]
        public void IsNull_WhenStringIsNull_ReturnsTrue()
        {
            // ARRANGE
            var stringInfo = StringInfo.FromString(null);

            // ACT
            var actualResult = stringInfo.IsNull;

            // ASSERT
            Assert.IsTrue(actualResult);
        }
    }
}
