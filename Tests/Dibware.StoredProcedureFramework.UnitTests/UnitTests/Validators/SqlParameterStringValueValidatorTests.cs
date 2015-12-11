using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Validators
{
    [TestClass]
    public class SqlParameterStringValueValidatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithNullSqlParameter_ThrowsException()
        {
            // ARRANGE
            const string testValue = "12345678";

            // ACT
            new SqlParameterStringValueValidator(null, testValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithNullValue_ThrowsException()
        {
            // ARRANGE
            const string testValue = "12345678";
            var sqlParameter = new SqlParameter("TestParameter", SqlDbType.NVarChar)
            {
                Size = 7
            };

            // ACT
            new SqlParameterStringValueValidator(sqlParameter, null);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlParameterOutOfRangeException))]
        public void Validate_WhenScaleIsTooLarge_ThrowsSqlParameterOutOfRangeException()
        {
            // ARRANGE
            const string testValue = "12345678";
            var sqlParameter = new SqlParameter("TestParameter", SqlDbType.NVarChar)
            {
                Size = 7
            };

            // ACT
            new SqlParameterStringValueValidator(sqlParameter, testValue).Validate();

            // ASSERT
        }

        [TestMethod]
        public void Validate_WhenPrecisionAndScaleIsWithinLimits_DoesNotThrowAnyExceptions()
        {
            // ARRANGE
            const string testValue = "123456";
            var sqlParameter = new SqlParameter("TestParameter", SqlDbType.NVarChar)
            {
                Size = 10
            };

            // ACT
            new SqlParameterStringValueValidator(sqlParameter, testValue).Validate();

            // ASSERT
        }
    }
}