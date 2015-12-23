using System;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Generics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Extensions
{
    [TestClass]
    public class MaybeExtensionsTests
    {
        [TestMethod]
        public void SingleOrDefault_WhenGivenEmptyMaybe_ReturnsDefault()
        {
            // ARRANGE
            const string defaultValue = "Monkey";
            var maybe = new Maybe<string>();
            
            // ACT
            var actual = maybe.SingleOrDefault(defaultValue);

            // ASSERT
            Assert.AreEqual(defaultValue, actual);
        }

        [TestMethod]
        public void SingleOrDefault_WhenGivenPoulatedMaybe_ReturnsMaybeValue()
        {
            // ARRANGE
            const string defaultValue = "Monkey";
            const string maybeValue = "Cat";
            var maybe = new Maybe<string>(maybeValue);

            // ACT
            var actual = maybe.SingleOrDefault(defaultValue);

            // ASSERT
            Assert.AreEqual(maybeValue, actual);
        }
    }
}