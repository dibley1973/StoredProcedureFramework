using System;
using System.Data;
using System.Text;
using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class ClrTypeToSqlTypeMapperTests
    {
        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenBooleanType_ReturnsBitSqlDbType()
        {
            // ARRANGE
            Type value = typeof (Boolean);
            const SqlDbType expectedSqlDbType = SqlDbType.Bit;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);
            
            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenNullableBooleanType_ReturnsBitSqlDbType()
        {
            // ARRANGE
            Type value = typeof(Boolean?);
            const SqlDbType expectedSqlDbType = SqlDbType.Bit;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenByteType_ReturnsTinyIntSqlDbType()
        {
            // ARRANGE
            Type value = typeof (Byte);
            const SqlDbType expectedSqlDbType = SqlDbType.TinyInt;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);
            
            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenNullableByteType_ReturnsTinyIntSqlDbType()
        {
            // ARRANGE
            Type value = typeof(Byte?);
            const SqlDbType expectedSqlDbType = SqlDbType.TinyInt;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenStringType_ReturnsNVarCharSqlDbType()
        {
            // ARRANGE
            Type value = typeof (String);
            const SqlDbType expectedSqlDbType = SqlDbType.NVarChar;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenDateTimeType_ReturnsDateTimeSqlDbType()
        {
            // ARRANGE
            Type value = typeof (DateTime);
            const SqlDbType expectedSqlDbType = SqlDbType.DateTime;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenNullableDateTimeType_ReturnsDateTimeSqlDbType()
        {
            // ARRANGE
            Type value = typeof(DateTime?);
            const SqlDbType expectedSqlDbType = SqlDbType.DateTime;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenInt16Type_ReturnsSmallIntSqlDbType()
        {
            // ARRANGE
            Type value = typeof (Int16);
            const SqlDbType expectedSqlDbType = SqlDbType.SmallInt;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenNullableInt16Type_ReturnsSmallIntSqlDbType()
        {
            // ARRANGE
            Type value = typeof(Int16?);
            const SqlDbType expectedSqlDbType = SqlDbType.SmallInt;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenInt32Type_ReturnsIntSqlDbType()
        {
            // ARRANGE
            Type value = typeof (Int32);
            const SqlDbType expectedSqlDbType = SqlDbType.Int;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenNullableInt32Type_ReturnsIntSqlDbType()
        {
            // ARRANGE
            Type value = typeof(Int32?);
            const SqlDbType expectedSqlDbType = SqlDbType.Int;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenInt64Type_ReturnsBigIntSqlDbType()
        {
            // ARRANGE
            Type value = typeof (Int64);
            const SqlDbType expectedSqlDbType = SqlDbType.BigInt;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenNullableInt64Type_ReturnsBigIntSqlDbType()
        {
            // ARRANGE
            Type value = typeof(Int64?);
            const SqlDbType expectedSqlDbType = SqlDbType.BigInt;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenDecimalType_ReturnsDecimalSqlDbType()
        {
            // ARRANGE
            Type value = typeof(Decimal);
            const SqlDbType expectedSqlDbType = SqlDbType.Decimal;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenNullableDecimalType_ReturnsDecimalSqlDbType()
        {
            // ARRANGE
            Type value = typeof(Decimal?);
            const SqlDbType expectedSqlDbType = SqlDbType.Decimal;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenDoubleType_ReturnsFloatSqlDbType()
        {
            // ARRANGE
            Type value = typeof(Double);
            const SqlDbType expectedSqlDbType = SqlDbType.Float;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenNullableDoubleType_ReturnsFloatSqlDbType()
        {
            // ARRANGE
            Type value = typeof(Double?);
            const SqlDbType expectedSqlDbType = SqlDbType.Float;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenSingleType_ReturnsRealSqlDbType()
        {
            // ARRANGE
            Type value = typeof(Single);
            const SqlDbType expectedSqlDbType = SqlDbType.Real;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenNullableSingleType_ReturnsRealSqlDbType()
        {
            // ARRANGE
            Type value = typeof(Single?);
            const SqlDbType expectedSqlDbType = SqlDbType.Real;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenTimeSpanType_ReturnsTimeSqlDbType()
        {
            // ARRANGE
            Type value = typeof (TimeSpan);
            const SqlDbType expectedSqlDbType = SqlDbType.Time;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenGuidType_ReturnsUniqueIdentifierSqlDbType()
        {
            // ARRANGE
            Type value = typeof (Guid);
            const SqlDbType expectedSqlDbType = SqlDbType.UniqueIdentifier;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenNullableGuidType_ReturnsUniqueIdentifierSqlDbType()
        {
            // ARRANGE
            Type value = typeof(Guid?);
            const SqlDbType expectedSqlDbType = SqlDbType.UniqueIdentifier;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenByteArrayType_ReturnsBinarySqlDbType()
        {
            // ARRANGE
            Type value = typeof (Byte[]);
            const SqlDbType expectedSqlDbType = SqlDbType.Binary;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenNullableByteArrayType_ReturnsBinarySqlDbType()
        {
            // ARRANGE
            Type value = typeof(Byte?[]);
            const SqlDbType expectedSqlDbType = SqlDbType.Binary;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenCharArrayType_ReturnsCharSqlDbType()
        {
            // ARRANGE
            Type value = typeof (Char[]);
            const SqlDbType expectedSqlDbType = SqlDbType.Char;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        public void GetSqlDbTypeFromClrType_WhenGivenNullableCharArrayType_ReturnsCharSqlDbType()
        {
            // ARRANGE
            Type value = typeof(Char?[]);
            const SqlDbType expectedSqlDbType = SqlDbType.Char;

            // ACT
            SqlDbType actual = ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetSqlDbTypeFromClrType_WhenGivenUnexpectedType_THEN()
        {
            // ARRANGE
            Type value = typeof(StringBuilder);

            // ACT
            ClrTypeToSqlDbTypeMapper.GetSqlDbTypeFromClrType(value);

            // ASSERT
            // Exception should have been thrown by here
        }
    }
}