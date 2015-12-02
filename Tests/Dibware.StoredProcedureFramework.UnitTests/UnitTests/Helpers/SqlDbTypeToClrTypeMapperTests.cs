using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class SqlDbTypeToClrTypeMapperTests
    {
        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenBigIntSqlDbType_ReturnsTypeOfInt64()
        {
            // ARRANGE
            Type expectedType = typeof(Int64);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.BigInt);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenBinarySqlDbType_ReturnsTypeOfByteArray()
        {
            // ARRANGE
            Type expectedType = typeof(Byte[]);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Binary);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenBitSqlDbType_ReturnsTypeOfBoolean()
        {
            // ARRANGE
            Type expectedType = typeof(Boolean);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Bit);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenCharSqlDbType_ReturnsTypeOf()
        {
            // ARRANGE
            Type expectedType = typeof(String);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Char);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenDateSqlDbType_ReturnsTypeOf()
        {
            // ARRANGE
            Type expectedType = typeof(DateTime);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Date);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenDateTimeSqlDbType_ReturnsTypeOfDateTime()
        {
            // ARRANGE
            Type expectedType = typeof(DateTime);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.DateTime);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenDateTime2SqlDbType_ReturnsTypeOfDateTime()
        {
            // ARRANGE
            Type expectedType = typeof(DateTime);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.DateTime2);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenDateTimeOffsetSqlDbType_ReturnsTypeOfDateTimeOffset()
        {
            // ARRANGE
            Type expectedType = typeof(DateTimeOffset);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.DateTimeOffset);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenDecimalSqlDbType_ReturnsTypeOfDecimal()
        {
            // ARRANGE
            Type expectedType = typeof(Decimal);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Decimal);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenFloatSqlDbType_ReturnsTypeOfDouble()
        {
            // ARRANGE
            Type expectedType = typeof(Double);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Float);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenImageSqlDbType_ReturnsTypeOfByteArray()
        {
            // ARRANGE
            Type expectedType = typeof(Byte[]);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Image);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenIntSqlDbType_ReturnsTypeOfInt32()
        {
            // ARRANGE
            Type expectedType = typeof(Int32);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Int);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenMoneySqlDbType_ReturnsTypeOfDecimal()
        {
            // ARRANGE
            Type expectedType = typeof(Decimal);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Money);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenNCharSqlDbType_ReturnsTypeOfString()
        {
            // ARRANGE
            Type expectedType = typeof(String);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.NChar);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenNTextSqlDbType_ReturnsTypeOfString()
        {
            // ARRANGE
            Type expectedType = typeof(String);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.NText);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenNVarCharSqlDbType_ReturnsTypeOfString()
        {
            // ARRANGE
            Type expectedType = typeof(String);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.NVarChar);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenRealSqlDbType_ReturnsTypeOfSingle()
        {
            // ARRANGE
            Type expectedType = typeof(Single);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Real);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenSmallDateTimeSqlDbType_ReturnsTypeOfDateTime()
        {
            // ARRANGE
            Type expectedType = typeof(DateTime);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.SmallDateTime);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenSmallIntSqlDbType_ReturnsTypeOfInt16()
        {
            // ARRANGE
            Type expectedType = typeof(Int16);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.SmallInt);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenSmallMoneySqlDbType_ReturnsTypeOfDecimal()
        {
            // ARRANGE
            Type expectedType = typeof(Decimal);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.SmallMoney);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenTextSqlDbType_ReturnsTypeOfString()
        {
            // ARRANGE
            Type expectedType = typeof(String);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Text);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenTimeSqlDbType_ReturnsTypeOfTimeSpan()
        {
            // ARRANGE
            Type expectedType = typeof(TimeSpan);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Time);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenTimestampSqlDbType_ReturnsTypeOfByteArray()
        {
            // ARRANGE
            Type expectedType = typeof(Byte[]);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Timestamp);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenTinyIntSqlDbType_ReturnsTypeOfByte()
        {
            // ARRANGE
            Type expectedType = typeof(byte);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.TinyInt);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenUniqueIdentifierSqlDbType_ReturnsTypeOfGuid()
        {
            // ARRANGE
            Type expectedType = typeof(Guid);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.UniqueIdentifier);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenVarBinarySqlDbType_ReturnsTypeOfByteArray()
        {
            // ARRANGE
            Type expectedType = typeof(Byte[]);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.VarBinary);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenVarCharSqlDbType_ReturnsTypeOfString()
        {
            // ARRANGE
            Type expectedType = typeof(String);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.VarChar);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }

        [TestMethod]
        public void GetClrTypeFromSqlDbType_WhenGivenXmlSqlDbType_ReturnsTypeOfString()
        {
            // ARRANGE
            Type expectedType = typeof(String);

            // ACT
            Type actual = SqlDbTypeToClrTypeMapper.GetClrTypeFromSqlDbType(SqlDbType.Xml);

            // ASSERT
            Assert.AreEqual(expectedType, actual);
        }
    }
}