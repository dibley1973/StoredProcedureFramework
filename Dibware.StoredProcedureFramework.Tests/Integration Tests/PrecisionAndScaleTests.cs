using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.Base;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.PrecisionAndScale;
using Dibware.StoredProcedureFrameworkForEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class PrecisionAndScaleTests : BaseIntegrationTest
    {
        [TestMethod]
        public void CallDecimalProcedureWithValuesCorrectPrecisionAndScale_ResultsInNoLossOfData()
        {
            // ARRANGE
            const decimal initialValue1 = 1234567.123M;
            const decimal initialValue2 = 123456.7M;
            var parameters = new DecimalPrecisionAndScaleParameters
            {
                Value1 = initialValue1,
                Value2 = initialValue2
            };
            var procedure = new DecimalPrecisionAndScaleStoredProcedure(parameters);
            procedure.InitializeFromAttributes();

            // ACT
            var resultSet = Context.ExecuteStoredProcedure(procedure);
            var results = resultSet.RecordSet1;
            var result = results.FirstOrDefault();

            // ASSERT
            Assert.IsNotNull(result);
            Assert.AreEqual(initialValue1, result.Value1);
            Assert.AreEqual(initialValue2, result.Value2);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlParameterOutOfRangeException))]
        public void CallDecimalProcedureWithValuesIncorrectPrecision_ThrowsSqlParameterOutOfRangeException()
        {
            // ARRANGE
            const decimal initialValue = 1234567.123M;
            var parameters = new DecimalPrecisionAndScaleParameters
            {
                Value1 = initialValue,
                Value2 = initialValue
            };
            var procedure = new DecimalPrecisionAndScaleStoredProcedure(parameters);
            procedure.InitializeFromAttributes();

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(SqlParameterOutOfRangeException))]
        public void CallDecimalProcedureWithIncorrectScale_ThrowsSqlParameterOutOfRangeException()
        {
            // ARRANGE
            const decimal initialValue1 = 1234567.891M;
            const decimal initialValue2 = 12345.67M;
            var parameters = new DecimalPrecisionAndScaleParameters
            {
                Value1 = initialValue1,
                Value2 = initialValue2
            };
            var procedure = new DecimalPrecisionAndScaleStoredProcedure(parameters);
            procedure.InitializeFromAttributes();

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.Fail();
        }
    }
}
