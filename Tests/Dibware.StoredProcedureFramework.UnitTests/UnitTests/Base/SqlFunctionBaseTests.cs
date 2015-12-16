using System;
using Dibware.StoredProcedureFramework.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Base
{
    [TestClass]
    public class SqlFunctionBaseTests
    {
        #region Construtor

        [TestMethod]
        public void Constructor_WhenCalledWithNullParameters_ContructsWithoutIssue()
        {
            // ARRANGE

            // ACT
            var actual = new SqlFunctionBaseTestFunction(null);

            // ASSERT
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenCalledWithNullFunctionName_ThrowsException()
        {
            // ARRANGE

            // ACT
            new SqlFunctionBaseTestFunction(null, null);

            // ASSERT

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WhenCalledWithEmptyFunctionName_ThrowsException()
        {
            // ARRANGE

            // ACT
            new SqlFunctionBaseTestFunction("", null);

            // ASSERT

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenCalledWithNullSchemaName_ThrowsException()
        {
            // ARRANGE

            // ACT
            new SqlFunctionBaseTestFunction(null, "FunctionName", null);

            // ASSERT

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_WhenCalledWithEmptySchemaName_ThrowsException()
        {
            // ARRANGE

            // ACT
            new SqlFunctionBaseTestFunction("", "FunctionName", null);

            // ASSERT

        }

        #endregion

        #region ParametersType

        [TestMethod]
        public void ParametersType_AfteCconstruction_ReturnsCorrectType()
        {
            // ARRANGE
            Type expectedType = typeof(SqlFunctionBaseTestFunction.Parameter);

            // ACT
            var function = new SqlFunctionBaseTestFunction(null);
            var actual = function.ParametersType;

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        #endregion

        #region ReturnType

        [TestMethod]
        public void ReturnType_AfteCconstruction_ReturnsCorrectType()
        {
            // ARRANGE
            Type expectedType = typeof(int?);

            // ACT
            var function = new SqlFunctionBaseTestFunction(null);
            var actual = function.ReturnType;

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        #endregion

    }

    internal class SqlFunctionBaseTestFunction
        : SqlFunctionBase<
            int?,
            SqlFunctionBaseTestFunction.Parameter>
    {
        public SqlFunctionBaseTestFunction(Parameter parameters)
            : base(parameters)
        { }

        public SqlFunctionBaseTestFunction(string sqlFunctionName, Parameter parameters)
            : base(sqlFunctionName, parameters)
        { }

        public SqlFunctionBaseTestFunction(string schemaName, string sqlFunctionName, Parameter parameters)
            : base(schemaName, sqlFunctionName, parameters)
        { }


        internal class Parameter
        {
            public int Value1 { get; set; }
        }
    }
}
