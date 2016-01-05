using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class OutputParameterValueProcessorTests
    {
        private SqlParameter _sqlParameter1;
        private SqlParameter _sqlParameter2;

        [TestInitialize]
        public void TestSetup()
        {
            _sqlParameter1 = new SqlParameter("Value1", SqlDbType.NVarChar);
            _sqlParameter2 = new SqlParameter("Value2", SqlDbType.Int);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Construct_WithNullStoredProcedure_ThrowsException()
        {
            // ARRANGE
            _sqlParameter1.Direction = ParameterDirection.Output;
            var sqlParameters = new List<SqlParameter>
            {
                _sqlParameter1,
                _sqlParameter2
            };

            CountCharsInOutputParameterStoredProcedure procedure = null;
            var processor = new OutputParameterValueProcessor<NullStoredProcedureResult, CountCharsInOutputParameterStoredProcedure.Parameter>(sqlParameters, procedure);

            // ACT
            processor.Processs();

            // ASSERT
            // Exception should have already happenned
        }

        [TestMethod]
        public void Construct_WhenGivenOutputSqlParameter_PopulatesParameterValuecorrectly()
        {
            // ARRANGE
            const string expectedValue1 = "MonkeyTube";
            const int initialValue2 = 0;
            const int expectedvalue2 = 99;
            var parameters = new CountCharsInOutputParameterStoredProcedure.Parameter
            {
                Value1 = expectedValue1,
                Value2 = initialValue2
            };
            _sqlParameter2.Direction = ParameterDirection.Output;
            _sqlParameter2.Value = expectedvalue2;
            var sqlParameters = new List<SqlParameter>
            {
                _sqlParameter1,
                _sqlParameter2
            };

            var procedure = new CountCharsInOutputParameterStoredProcedure(parameters);
            var processor = new OutputParameterValueProcessor<NullStoredProcedureResult, CountCharsInOutputParameterStoredProcedure.Parameter>(sqlParameters, procedure);

            // ACT
            processor.Processs();

            // ASSERT
            Assert.AreEqual(expectedvalue2, parameters.Value2);
        }

        [TestMethod]
        public void Construct_WhenGivenInputOutputSqlParameter_PopulatesParameterValuecorrectly()
        {
            // ARRANGE
            const string expectedValue1 = "MonkeyTube";
            const int initialValue2 = 0;
            const int expectedvalue2 = 99;
            var parameters = new CountCharsInOutputParameterStoredProcedure.Parameter
            {
                Value1 = expectedValue1,
                Value2 = initialValue2
            };
            _sqlParameter2.Direction = ParameterDirection.InputOutput;
            _sqlParameter2.Value = expectedvalue2;
            var sqlParameters = new List<SqlParameter>
            {
                _sqlParameter1,
                _sqlParameter2
            };

            var procedure = new CountCharsInOutputParameterStoredProcedure(parameters);
            var processor = new OutputParameterValueProcessor<NullStoredProcedureResult, CountCharsInOutputParameterStoredProcedure.Parameter>(sqlParameters, procedure);

            // ACT
            processor.Processs();

            // ASSERT
            Assert.AreEqual(expectedvalue2, parameters.Value2);
        }

        [TestMethod]
        public void Construct_WhenGivenInputSqlParameter_PopulatesParameterValuecorrectly()
        {
            // ARRANGE
            const string expectedValue1 = "MonkeyTube";
            const int initialValue2 = 0;
            const int expectedvalue2 = 99;
            var parameters = new CountCharsInOutputParameterStoredProcedure.Parameter
            {
                Value1 = expectedValue1,
                Value2 = initialValue2
            };
            _sqlParameter2.Direction = ParameterDirection.Input;
            _sqlParameter2.Value = expectedvalue2;
            var sqlParameters = new List<SqlParameter>
            {
                _sqlParameter1,
                _sqlParameter2
            };

            var procedure = new CountCharsInOutputParameterStoredProcedure(parameters);
            var processor = new OutputParameterValueProcessor<NullStoredProcedureResult, CountCharsInOutputParameterStoredProcedure.Parameter>(sqlParameters, procedure);

            // ACT
            processor.Processs();

            // ASSERT
            Assert.AreNotEqual(expectedvalue2, parameters.Value2);
        }

        [TestMethod]
        public void Construct_WhenGivenReturnValueSqlParameter_PopulatesParameterValuecorrectly()
        {
            // ARRANGE
            const string expectedValue1 = "MonkeyTube";
            const int initialValue2 = 0;
            const int expectedvalue2 = 99;
            var parameters = new CountCharsInOutputParameterStoredProcedure.Parameter
            {
                Value1 = expectedValue1,
                Value2 = initialValue2
            };
            _sqlParameter2.Direction = ParameterDirection.ReturnValue;
            _sqlParameter2.Value = expectedvalue2;
            var sqlParameters = new List<SqlParameter>
            {
                _sqlParameter1,
                _sqlParameter2
            };

            var procedure = new CountCharsInOutputParameterStoredProcedure(parameters);
            var processor = new OutputParameterValueProcessor<NullStoredProcedureResult, CountCharsInOutputParameterStoredProcedure.Parameter>(sqlParameters, procedure);

            // ACT
            processor.Processs();

            // ASSERT
            Assert.AreNotEqual(expectedvalue2, parameters.Value2);
        }
    }
}