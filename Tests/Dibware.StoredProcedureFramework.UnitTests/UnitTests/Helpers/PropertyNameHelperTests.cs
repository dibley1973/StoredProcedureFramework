using System;
using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class PropertyNameHelperTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithNullProperty_ThrowsException()
        {
            // ARRANGE

            // ACT
            new PropertyNameHelper(null);

            // ASSERT
        }
    }
}
