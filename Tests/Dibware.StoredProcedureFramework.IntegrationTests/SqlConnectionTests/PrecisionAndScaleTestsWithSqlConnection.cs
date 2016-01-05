using System.Linq;
using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class PrecisionAndScaleTestsWithSqlConnection : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        public void CallDecimalProcedureWithValuesCorrectPrecisionAndScale_ResultsInNoLossOfData()
        {
            // ARRANGE
            const decimal initialValue1 = 1234567.123M;
            const decimal initialValue2 = 123456.7M;
            var parameters = new DecimalPrecisionAndScaleStoredProcedure.Parameter
            {
                Value1 = initialValue1,
                Value2 = initialValue2
            };
            var procedure = new DecimalPrecisionAndScaleStoredProcedure(parameters);

            // ACT
            var results = Connection.ExecuteStoredProcedure(procedure);
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
            var parameters = new DecimalPrecisionAndScaleStoredProcedure.Parameter
            {
                Value1 = initialValue,
                Value2 = initialValue
            };
            var procedure = new DecimalPrecisionAndScaleStoredProcedure(parameters);

            // ACT
            Connection.ExecuteStoredProcedure(procedure);

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
            var parameters = new DecimalPrecisionAndScaleStoredProcedure.Parameter
            {
                Value1 = initialValue1,
                Value2 = initialValue2
            };
            var procedure = new DecimalPrecisionAndScaleStoredProcedure(parameters);

            // ACT
            Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.Fail();
        }
    }
}
