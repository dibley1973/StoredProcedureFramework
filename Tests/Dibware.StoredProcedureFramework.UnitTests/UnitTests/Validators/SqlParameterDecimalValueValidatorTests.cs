using System;
using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Validators
{
    [TestClass]
    public class SqlParameterDecimalValueValidatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithNullSqlParaemeter_ThrowsException()
        {
            // ARRANGE
            const decimal testValue = 10.12345678M;
            
            // ACT
            new SqlParameterDecimalValueValidator(null, testValue);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlParameterOutOfRangeException))]
        public void Validate_WhenPrecisionIsTooLarge_ThrowsSqlParameterOutOfRangeException()
        {
            // ARRANGE
            const decimal testValue = 10.12345678M;
            var sqlParameter = new SqlParameter("TestParameter", SqlDbType.Decimal)
            {
                Precision = 7
            };

            // ACT
            new SqlParameterDecimalValueValidator(sqlParameter, testValue).Validate();

            // ASSERT
        }

        [TestMethod]
        [ExpectedException(typeof(SqlParameterOutOfRangeException))]
        public void Validate_WhenScaleIsTooLarge_ThrowsSqlParameterOutOfRangeException()
        {
            // ARRANGE
            const decimal testValue = 12345678M;
            var sqlParameter = new SqlParameter("TestParameter", SqlDbType.Decimal)
            {
                Scale = 7
            };

            // ACT
            new SqlParameterDecimalValueValidator(sqlParameter, testValue).Validate();

            // ASSERT
        }

        [TestMethod]
        public void Validate_WhenPrecisionAndScaleIsWithinLimits_DoesNotThrowAnyExceptions()
        {
            // ARRANGE
            const decimal testValue = 123456.78M;
            var sqlParameter = new SqlParameter("TestParameter", SqlDbType.Decimal)
            {
                Precision = 10,
                Scale = 3
            };

            // ACT
            new SqlParameterDecimalValueValidator(sqlParameter, testValue).Validate();

            // ASSERT
        }
    }
}