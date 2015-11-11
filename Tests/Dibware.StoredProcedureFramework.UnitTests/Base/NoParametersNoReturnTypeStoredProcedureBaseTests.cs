using Dibware.StoredProcedureFramework.Tests.Fakes.StoredProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.Base
{
    [TestClass]
    public class NoParametersNoReturnTypeStoredProcedureBaseTests
    {
        [TestMethod]
        public void SchemaName_WhenConstructedWithSchemaName_ReturnsCorrectValue()
        {
            // ARRANGE
            const string expectedSchemaName = "app";
            const string expectedProcedureName = "GetAllThis";
            var storedProc = new StoredProcedureWithNoParametersAndNoReturnType(
                expectedSchemaName, expectedProcedureName);

            // ACT
            var actualSchemaName = storedProc.SchemaName;

            // ASSERT
            Assert.AreEqual(expectedSchemaName, actualSchemaName);
        }

        [TestMethod]
        public void ProcedureName_WhenConstructedWithProcedureName_ReturnsCorrectValue()
        {
            // ARRANGE
            const string expectedProcedureName = "GetAllThis";
            var storedProc = new StoredProcedureWithNoParametersAndNoReturnType(
                expectedProcedureName);

            // ACT
            var actualProcedureName = storedProc.ProcedureName;

            // ASSERT
            Assert.AreEqual(expectedProcedureName, actualProcedureName);
        }
    }
}