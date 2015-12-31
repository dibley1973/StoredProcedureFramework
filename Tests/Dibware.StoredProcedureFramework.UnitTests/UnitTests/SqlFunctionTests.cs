using System;
using Dibware.StoredProcedureFramework.Tests.UnitTests.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests
{
    [TestClass]
    public class SqlFunctionTests
    {
        #region GetTwoPartName

        //[TestMethod]
        //[Ignore] // Until we can get back to a way to fake no procedure name
        //[ExpectedException(typeof(StoredProcedureConstructionException))]
        //public void GetTwoPartNamed_WhenNotFullyConstructed_ThrowsException()
        //{
        //    // ARRANGE
        //    var procedure = new NotFullyConstructedScalarFunction();

        //    // ACT
        //    procedure.GetTwoPartName();

        //    // ASSERT
        //}

        [TestMethod]
        public void GetTwoPartName_WhenConstructedWithoutProcedureName_ReturnsCorrectly()
        {
            // ARRANGE
            var parameters = new ScalarValueFunctionWithParameterAndReturn.Parameter();
            var procedure = new ScalarValueFunctionWithParameterAndReturn(parameters);
            const string expectedSchemaName = StoredProcedureDefaults.DefaultSchemaName;
            string expectedProcedureName = typeof(ScalarValueFunctionWithParameterAndReturn).Name;
            string expectedTwoPartName = String.Concat(
                expectedSchemaName,
                StoredProcedureDefaults.DotIdentifier,
                expectedProcedureName);

            // ACT
            var actualTwoPartName = procedure.GetTwoPartName();

            // ASSERT
            Assert.AreEqual(expectedTwoPartName, actualTwoPartName);
        }

        [TestMethod]
        public void GetTwoPartName_WhenConstructedWithProcedureName_ReturnsCorrectly()
        {
            // ARRANGE
            const string expectedSchemaName = StoredProcedureDefaults.DefaultSchemaName;
            const string expectedProcedureName = "GetAllMonkeys";
            string expectedTwoPartName = String.Concat(
                expectedSchemaName,
                StoredProcedureDefaults.DotIdentifier,
                expectedProcedureName);
            var parameters = new ScalarValueFunctionWithParameterAndReturn.Parameter();
            var procedure = new ScalarValueFunctionWithParameterAndReturn(expectedProcedureName, parameters);

            // ACT
            var actualTwoPartName = procedure.GetTwoPartName();

            // ASSERT
            Assert.AreEqual(expectedTwoPartName, actualTwoPartName);
        }

        #endregion


        #region SetName

        [TestMethod]
        public void SetName_WithValidValue_CheckNameProperty_ReturnsCorrect()
        {
            // ARRANGE
            const string expectedValue = "TestName";
            var parameters = new ScalarValueFunctionWithParameterAndReturn.Parameter();
            var function = new ScalarValueFunctionWithParameterAndReturn(parameters);

            // ACT
            function.SetFunctionName(expectedValue);
            var actualValue = function.FunctionName;

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetName_WithInvalidValue_ThrowsArgumentOutOfRangeException()
        {
            // ARRANGE
            var parameters = new ScalarValueFunctionWithParameterAndReturn.Parameter();
            var function = new ScalarValueFunctionWithParameterAndReturn(parameters);

            // ACT
            function.SetFunctionName(string.Empty);

            // ASSERT
            Assert.Fail("ArgumentOutOfRangeException not encountered");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetName_WithNullValue_ThrowsArgumentNullException()
        {
            // ARRANGE
            var parameters = new ScalarValueFunctionWithParameterAndReturn.Parameter();
            var function = new ScalarValueFunctionWithParameterAndReturn(parameters);

            // ACT
            function.SetFunctionName(null);

            // ASSERT
            Assert.Fail("ArgumentOutOfRangeException not encountered");
        }

        #endregion

        #region SetSchema

        [TestMethod]
        public void SetSchema_WithValidValue_CheckSchemaProperty_ReturnsCorrect()
        {
            // ARRANGE
            const string expectedValue = "TestSchema";
            var parameters = new ScalarValueFunctionWithParameterAndReturn.Parameter();
            var function = new ScalarValueFunctionWithParameterAndReturn(parameters);

            // ACT
            function.SetSchemaName(expectedValue);
            var actualValue = function.SchemaName;

            // ASSERT
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetSchema_WithInvalidValue_ThrowsArgumentOutOfRangeException()
        {
            // ARRANGE
            var parameters = new ScalarValueFunctionWithParameterAndReturn.Parameter();
            var function = new ScalarValueFunctionWithParameterAndReturn(parameters);

            // ACT
            function.SetSchemaName(string.Empty);

            // ASSERT
            Assert.Fail("ArgumentOutOfRangeException not encountered");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetSchema_WithNullValue_ThrowsArgumentNullException()
        {
            // ARRANGE
            var parameters = new ScalarValueFunctionWithParameterAndReturn.Parameter();
            var function = new ScalarValueFunctionWithParameterAndReturn(parameters);

            // ACT
            function.SetSchemaName(null);

            // ASSERT
            Assert.Fail("ArgumentOutOfRangeException not encountered");
        }

        #endregion
    }
}
