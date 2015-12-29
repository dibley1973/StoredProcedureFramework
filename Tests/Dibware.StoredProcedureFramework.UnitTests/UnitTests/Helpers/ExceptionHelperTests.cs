using System;
using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class ExceptionHelperTests
    {
        [TestMethod]
        public void CreateStoredProcedureConstructionException_ConstructedWithMessage_ReturnsCorrectMessage()
        {
            // ARRANGE
            const string expectedMessage = "Test Message";

            // ACT
            var exception = ExceptionHelper.CreateStoredProcedureConstructionException(expectedMessage);
            var actualMessage = exception.Message;

            // ASSERT
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod]
        public void CreateStoredProcedureConstructionException_ConstructedWithMessageAndInnerException_ReturnsBothCorrectly()
        {
            // ARRANGE
            const string expectedMessage = "Test Message";
            var expectedInnerException = new ArgumentNullException();
            //var expectedInnerExceptinType = innerException.GetType() typeof(ArgumentNullException);

            // ACT
            var exception = ExceptionHelper.CreateStoredProcedureConstructionException(expectedMessage, expectedInnerException);
            var actualMessage = exception.Message;

            // ASSERT
            Assert.AreEqual(expectedMessage, actualMessage);
            Assert.AreEqual(exception.InnerException, expectedInnerException);
        }

        [TestMethod]
        public void CreateSqlFunctionConstructionException_ConstructedWithMessage_ReturnsCorrectMessage()
        {
            // ARRANGE
            const string expectedMessage = "Test Message";

            // ACT
            var exception = ExceptionHelper.CreateSqlFunctionConstructionException(expectedMessage);
            var actualMessage = exception.Message;

            // ASSERT
            Assert.AreEqual(expectedMessage, actualMessage);
        }

    }
}