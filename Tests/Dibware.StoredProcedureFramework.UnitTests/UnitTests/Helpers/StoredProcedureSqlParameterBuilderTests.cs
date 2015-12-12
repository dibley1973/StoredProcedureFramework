using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class StoredProcedureSqlParameterBuilderTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNull_ThrowsException()
        {
            // ARRANGE
            MostBasicStoredProcedure procedure = null;

            // ACT
            new StoredProcedureSqlParameterBuilder<NullStoredProcedureResult, NullStoredProcedureParameters>(procedure);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parameters_WhenSuppliedProcedureHasNullStoredProcedureParameters_ParametersAreNull()
        {
            // ARRANGE
            MostBasicStoredProcedure procedure = null;

            // ACT
            var sqlParameterBuilder = new StoredProcedureSqlParameterBuilder<NullStoredProcedureResult, NullStoredProcedureParameters>(procedure);
            var parameters = sqlParameterBuilder.SqlParameters;

            //
            Assert.IsNull(parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Parameters_WhenSuppliedProcedureHasNullStoredProcedureParametersAndUsingFluidApi_ThrowsException()
        {
            // ARRANGE
            MostBasicStoredProcedure procedure = null;

            // ACT
            var parameters = new StoredProcedureSqlParameterBuilder<NullStoredProcedureResult, NullStoredProcedureParameters>(procedure)
                .BuildSqlParameters()
                .SqlParameters;

            // ASSERT
            // Exception should already be thrown
        }

        [TestMethod]
        public void Parameters_WhenSuppliedProcedureHasParametersAndUsingFluidApi_ParametersAreNull()
        {
            // ARRANGE
            const int expectedParameterCount = 1;
            const int expectedValue = 1;
            var parameters = new NormalStoredProcedureParameters
            {
                Id = 1
            };
            var procedure = new NormalStoredProcedure(parameters);

            // ACT
            var actualParameters = new StoredProcedureSqlParameterBuilder<List<NormalStoredProcedureRecordSetReturnType>, NormalStoredProcedureParameters>(procedure)
                .BuildSqlParameters()
                .SqlParameters;

            // ASSERT
            Assert.AreEqual(expectedParameterCount, actualParameters.Count);
            Assert.AreEqual(expectedValue, actualParameters.First().Value);
        }

        [TestMethod]
        public void BuildParameters_WhenSuppliedProcedureHasParameters_BuildsCorrectQuantity()
        {
            // ARRANGE
            const int expectedParameterCount = 28;
            const Int64 expectedBigint = Int64.MaxValue;
            Byte[] expectedBinary = { 0, 1, 2, 3, 4, 5, 6, 7 };
            const Boolean expectedBit = true;
            Char[] expectedChar = { 'a', 'b', 'c' };
            DateTime expectedDate = DateTime.Today;
            DateTime expectedDatetime = DateTime.Now.AddHours(1);
            DateTime expectedDatetime2 = DateTime.Now.AddMinutes(10);
            const Decimal expectedDecimal = 1234567890123456.02M;
            const Double expectedFloat = Double.MaxValue;
            Byte[] expectedImage = { 0x10, 0x20, 0x30, 0x10, 0x20, 0x30, 0x10, 0x20 };
            const Int32 expectedInt = Int32.MaxValue;
            const Decimal expectedMoney = 922337203685477.5807M;
            const String expectedNChar = @"NChar";
            const String expectedNText = @"NText";
            const Decimal expectedNumeric = 1234567890123456.02M;
            const String expectedNVarchar = @"NVarChar";
            const Single expectedReal = Single.MaxValue;
            DateTime expectedSmalldatetime = DateTime.Today.AddDays(-1);
            const Int16 expectedSmallint = Int16.MaxValue;
            const Decimal expectedSmallmoney = 214748.3647M;

            const String expectedText = @"Some boring text...";
            TimeSpan expectedTime = TimeSpan.FromMinutes(20);
            Byte[] expectedTimestamp = { 10, 20, 30, 0, 0, 0, 0, 0 };
            const Byte expectedTinyInt = Byte.MaxValue;
            Guid expectedUniqueIdentifier = Guid.NewGuid();
            Byte[] expectedVarBinary = { 110, 120, 130 };
            const String expectedVarChar = @"VarChar";
            const String expectedXml = @"<parent><child name=""Fred"">Angus</child></parent>";

            var parameters = new AllCommonDataTypesStoredProcedureWithParameterAttributes.Parameter
            {
                BigInt = expectedBigint,
                Binary = expectedBinary,
                Bit = expectedBit,
                Char = expectedChar,
                Date = expectedDate,
                DateTime = expectedDatetime,
                DateTime2 = expectedDatetime2,
                Decimal = expectedDecimal,
                Float = expectedFloat,
                Image = expectedImage,
                Int = expectedInt,
                Money = expectedMoney,
                NChar = expectedNChar,
                NText = expectedNText,
                Numeric = expectedNumeric,
                NVarchar = expectedNVarchar,
                Real = expectedReal,
                SmallDateTime = expectedSmalldatetime,
                Smallint = expectedSmallint,
                Smallmoney = expectedSmallmoney,
                Text = expectedText,
                Time = expectedTime,
                Timestamp = expectedTimestamp,
                TinyInt = expectedTinyInt,
                UniqueIdentifier = expectedUniqueIdentifier,
                VarBinary = expectedVarBinary,
                VarChar = expectedVarChar,
                Xml = expectedXml
            };
            var procedure = new AllCommonDataTypesStoredProcedureWithParameterAttributes(parameters);
            var sqlParameterBuilder = 
                new StoredProcedureSqlParameterBuilder<
                    List<AllCommonDataTypesStoredProcedureWithParameterAttributes.Return>,
                    AllCommonDataTypesStoredProcedureWithParameterAttributes.Parameter>(procedure);

            // ACT
            sqlParameterBuilder.BuildSqlParameters();
            var actual = sqlParameterBuilder.SqlParameters;

            // ASSERT
            Assert.AreEqual(expectedParameterCount, actual.Count);
        }


        [TestMethod]
        public void BuildParameters_WhenSuppliedProcedureHasParametersWithParameterAttributes_BuildsCorrectTypes()
        {
            // ARRANGE
            var expectedBigIntType = SqlDbType.BigInt;
            var expectedBinaryType = SqlDbType.Binary;
            var expectedBitType = SqlDbType.Bit;
            var expectedCharType = SqlDbType.Char;
            var expectedDateType = SqlDbType.Date;
            var expectedDateTimeType = SqlDbType.DateTime;
            var expectedDateTime2Type = SqlDbType.DateTime2;
            var expectedDecimalType = SqlDbType.Decimal;
            var expectedFloatType = SqlDbType.Float;
            var expectedImageType = SqlDbType.Image;
            var expectedintType = SqlDbType.Int;
            var expectedMoneyType = SqlDbType.Money;
            var expectedNCharType = SqlDbType.NChar;
            var expectedNTextType = SqlDbType.NText;
            var expectedNumericType = SqlDbType.Decimal;
            var expectedNVarcharType = SqlDbType.NVarChar;
            var expectedRealType = SqlDbType.Real;
            var expectedSmallDateTimeType = SqlDbType.SmallDateTime;
            var expectedSmallintType = SqlDbType.SmallInt;
            var expectedSmallmoneyType = SqlDbType.SmallMoney;
            var expectedTextType = SqlDbType.Text;
            var expectedTimeType = SqlDbType.Time;
            var expectedTimestampType = SqlDbType.Timestamp;
            var expectedTinyIntType = SqlDbType.TinyInt;
            var expectedUniqueIdentifierType = SqlDbType.UniqueIdentifier;
            var expectedVarBinaryType = SqlDbType.VarBinary;
            var expectedVarCharType = SqlDbType.VarChar;
            var expectedXmlType = SqlDbType.Xml;

            const Int64 expectedBigint = Int64.MaxValue;
            Byte[] expectedBinary = { 0, 1, 2, 3, 4, 5, 6, 7 };
            const Boolean expectedBit = true;
            Char[] expectedChar = { 'a', 'b', 'c' };
            DateTime expectedDate = DateTime.Today;
            DateTime expectedDatetime = DateTime.Now.AddHours(1);
            DateTime expectedDatetime2 = DateTime.Now.AddMinutes(10);
            const Decimal expectedDecimal = 1234567890123456.02M;
            const Double expectedFloat = Double.MaxValue;
            Byte[] expectedImage = { 0x10, 0x20, 0x30, 0x10, 0x20, 0x30, 0x10, 0x20 };
            const Int32 expectedInt = Int32.MaxValue;
            const Decimal expectedMoney = 922337203685477.5807M;
            const String expectedNChar = @"NChar";
            const String expectedNText = @"NText";
            const Decimal expectedNumeric = 1234567890123456.02M;
            const String expectedNVarchar = @"NVarChar";
            const Single expectedReal = Single.MaxValue;
            DateTime expectedSmalldatetime = DateTime.Today.AddDays(-1);
            const Int16 expectedSmallint = Int16.MaxValue;
            const Decimal expectedSmallmoney = 214748.3647M;
            const String expectedText = @"Some boring text...";
            TimeSpan expectedTime = TimeSpan.FromMinutes(20);
            Byte[] expectedTimestamp = { 10, 20, 30, 0, 0, 0, 0, 0 };
            const Byte expectedTinyInt = Byte.MaxValue;
            Guid expectedUniqueIdentifier = Guid.NewGuid();
            Byte[] expectedVarBinary = { 110, 120, 130 };
            const String expectedVarChar = @"VarChar";
            const String expectedXml = @"<parent><child name=""Fred"">Angus</child></parent>";
            var parameters = new AllCommonDataTypesStoredProcedureWithParameterAttributes.Parameter
            {
                BigInt = expectedBigint,
                Binary = expectedBinary,
                Bit = expectedBit,
                Char = expectedChar,
                Date = expectedDate,
                DateTime = expectedDatetime,
                DateTime2 = expectedDatetime2,
                Decimal = expectedDecimal,
                Float = expectedFloat,
                Image = expectedImage,
                Int = expectedInt,
                Money = expectedMoney,
                NChar = expectedNChar,
                NText = expectedNText,
                Numeric = expectedNumeric,
                NVarchar = expectedNVarchar,
                Real = expectedReal,
                SmallDateTime = expectedSmalldatetime,
                Smallint = expectedSmallint,
                Smallmoney = expectedSmallmoney,
                Text = expectedText,
                Time = expectedTime,
                Timestamp = expectedTimestamp,
                TinyInt = expectedTinyInt,
                UniqueIdentifier = expectedUniqueIdentifier,
                VarBinary = expectedVarBinary,
                VarChar = expectedVarChar,
                Xml = expectedXml
            };
            var procedure = new AllCommonDataTypesStoredProcedureWithParameterAttributes(parameters);
            var sqlParameterBuilder =
                new StoredProcedureSqlParameterBuilder<
                    List<AllCommonDataTypesStoredProcedureWithParameterAttributes.Return>,
                    AllCommonDataTypesStoredProcedureWithParameterAttributes.Parameter>(procedure);

            // ACT
            sqlParameterBuilder.BuildSqlParameters();
            var actual = sqlParameterBuilder.SqlParameters.ToList();

            // ASSERT
            Assert.AreEqual(expectedBigIntType, actual[0].SqlDbType);
            Assert.AreEqual(expectedBinaryType, actual[1].SqlDbType);
            Assert.AreEqual(expectedBitType, actual[2].SqlDbType);
            Assert.AreEqual(expectedCharType, actual[3].SqlDbType);
            Assert.AreEqual(expectedDateType, actual[4].SqlDbType);
            Assert.AreEqual(expectedDateTimeType, actual[5].SqlDbType);
            Assert.AreEqual(expectedDateTime2Type, actual[6].SqlDbType);
            Assert.AreEqual(expectedDecimalType, actual[7].SqlDbType);
            Assert.AreEqual(expectedFloatType, actual[8].SqlDbType);
            Assert.AreEqual(expectedImageType, actual[9].SqlDbType);
            Assert.AreEqual(expectedintType, actual[10].SqlDbType);
            Assert.AreEqual(expectedMoneyType, actual[11].SqlDbType);
            Assert.AreEqual(expectedNCharType, actual[12].SqlDbType);
            Assert.AreEqual(expectedNTextType, actual[13].SqlDbType);
            Assert.AreEqual(expectedNumericType, actual[14].SqlDbType);
            Assert.AreEqual(expectedNVarcharType, actual[15].SqlDbType);
            Assert.AreEqual(expectedRealType, actual[16].SqlDbType);
            Assert.AreEqual(expectedSmallDateTimeType, actual[17].SqlDbType);
            Assert.AreEqual(expectedSmallintType, actual[18].SqlDbType);
            Assert.AreEqual(expectedSmallmoneyType, actual[19].SqlDbType);
            Assert.AreEqual(expectedTextType, actual[20].SqlDbType);
            Assert.AreEqual(expectedTimeType, actual[21].SqlDbType);
            Assert.AreEqual(expectedTimestampType, actual[22].SqlDbType);
            Assert.AreEqual(expectedTinyIntType, actual[23].SqlDbType);
            Assert.AreEqual(expectedUniqueIdentifierType, actual[24].SqlDbType);
            Assert.AreEqual(expectedVarBinaryType, actual[25].SqlDbType);
            Assert.AreEqual(expectedVarCharType, actual[26].SqlDbType);
            Assert.AreEqual(expectedXmlType, actual[27].SqlDbType);
        }

        [TestMethod]
        public void BuildParameters_WhenSuppliedProcedureHasParametersWithoutParameterAttributes_BuildsCorrectTypes()
        {
            // ARRANGE
            var expectedBigIntType = SqlDbType.BigInt;
            var expectedBinaryType = SqlDbType.Binary;
            var expectedBitType = SqlDbType.Bit;
            var expectedCharType = SqlDbType.Char;
            //var expectedDateType = SqlDbType.Date;
            var expectedDateTimeType = SqlDbType.DateTime;
            //var expectedDateTime2Type = SqlDbType.DateTime2;
            var expectedDecimalType = SqlDbType.Decimal;
            var expectedFloatType = SqlDbType.Float;
            //var expectedImageType = SqlDbType.Image;
            var expectedintType = SqlDbType.Int;
            //var expectedMoneyType = SqlDbType.Money;
            //var expectedNCharType = SqlDbType.NChar;
            //var expectedNTextType = SqlDbType.NText;
            var expectedNumericType = SqlDbType.Decimal;
            var expectedNVarcharType = SqlDbType.NVarChar;
            var expectedRealType = SqlDbType.Real;
            //var expectedSmallDateTimeType = SqlDbType.SmallDateTime;
            var expectedSmallintType = SqlDbType.SmallInt;
            //var expectedSmallmoneyType = SqlDbType.SmallMoney;
            //var expectedTextType = SqlDbType.Text;
            var expectedTimeType = SqlDbType.Time;
            //var expectedTimestampType = SqlDbType.Timestamp;
            var expectedTinyIntType = SqlDbType.TinyInt;
            var expectedUniqueIdentifierType = SqlDbType.UniqueIdentifier;
            //var expectedVarBinaryType = SqlDbType.VarBinary;
            //var expectedVarCharType = SqlDbType.VarChar;
            //var expectedXmlType = SqlDbType.Xml;

            const Int64 expectedBigint = Int64.MaxValue;
            Byte[] expectedBinary = { 0, 1, 2, 3, 4, 5, 6, 7 };
            const Boolean expectedBit = true;
            Char[] expectedChar = { 'a', 'b', 'c' };
            DateTime expectedDate = DateTime.Today;
            DateTime expectedDatetime = DateTime.Now.AddHours(1);
            DateTime expectedDatetime2 = DateTime.Now.AddMinutes(10);
            const Decimal expectedDecimal = 1234567890123456.02M;
            const Double expectedFloat = Double.MaxValue;
            Byte[] expectedImage = { 0x10, 0x20, 0x30, 0x10, 0x20, 0x30, 0x10, 0x20 };
            const Int32 expectedInt = Int32.MaxValue;
            const Decimal expectedMoney = 922337203685477.5807M;
            const String expectedNChar = @"NChar";
            const String expectedNText = @"NText";
            const Decimal expectedNumeric = 1234567890123456.02M;
            const String expectedNVarchar = @"NVarChar";
            const Single expectedReal = Single.MaxValue;
            DateTime expectedSmalldatetime = DateTime.Today.AddDays(-1);
            const Int16 expectedSmallint = Int16.MaxValue;
            const Decimal expectedSmallmoney = 214748.3647M;
            const String expectedText = @"Some boring text...";
            TimeSpan expectedTime = TimeSpan.FromMinutes(20);
            Byte[] expectedTimestamp = { 10, 20, 30, 0, 0, 0, 0, 0 };
            const Byte expectedTinyInt = Byte.MaxValue;
            Guid expectedUniqueIdentifier = Guid.NewGuid();
            Byte[] expectedVarBinary = { 110, 120, 130 };
            const String expectedVarChar = @"VarChar";
            const String expectedXml = @"<parent><child name=""Fred"">Angus</child></parent>";
            var parameters = new AllCommonDataTypesStoredProcedureWithoutParameterAttributes.Parameter
            {
                BigInt = expectedBigint,
                Binary = expectedBinary,
                Bit = expectedBit,
                Char = expectedChar,
                Date = expectedDate,
                DateTime = expectedDatetime,
                DateTime2 = expectedDatetime2,
                Decimal = expectedDecimal,
                Float = expectedFloat,
                Image = expectedImage,
                Int = expectedInt,
                Money = expectedMoney,
                NChar = expectedNChar,
                NText = expectedNText,
                Numeric = expectedNumeric,
                NVarchar = expectedNVarchar,
                Real = expectedReal,
                SmallDateTime = expectedSmalldatetime,
                Smallint = expectedSmallint,
                Smallmoney = expectedSmallmoney,
                Text = expectedText,
                Time = expectedTime,
                Timestamp = expectedTimestamp,
                TinyInt = expectedTinyInt,
                UniqueIdentifier = expectedUniqueIdentifier,
                VarBinary = expectedVarBinary,
                VarChar = expectedVarChar,
                Xml = expectedXml
            };
            var procedure = new AllCommonDataTypesStoredProcedureWithoutParameterAttributes(parameters);
            var sqlParameterBuilder =
                new StoredProcedureSqlParameterBuilder<
                    List<AllCommonDataTypesStoredProcedureWithParameterAttributes.Return>,
                    AllCommonDataTypesStoredProcedureWithoutParameterAttributes.Parameter>(procedure);

            // ACT
            sqlParameterBuilder.BuildSqlParameters();
            var actual = sqlParameterBuilder.SqlParameters.ToList();

            // ASSERT
            Assert.AreEqual(expectedBigIntType, actual[0].SqlDbType);
            Assert.AreEqual(expectedBinaryType, actual[1].SqlDbType);
            Assert.AreEqual(expectedBitType, actual[2].SqlDbType);
            Assert.AreEqual(expectedCharType, actual[3].SqlDbType);
            Assert.AreEqual(expectedDateTimeType, actual[4].SqlDbType);
            Assert.AreEqual(expectedDateTimeType, actual[5].SqlDbType);
            Assert.AreEqual(expectedDateTimeType, actual[6].SqlDbType);
            Assert.AreEqual(expectedDecimalType, actual[7].SqlDbType);
            Assert.AreEqual(expectedFloatType, actual[8].SqlDbType);
            Assert.AreEqual(expectedBinaryType, actual[9].SqlDbType);
            //Assert.AreEqual(expectedImageType, actual[9].SqlDbType);
            Assert.AreEqual(expectedintType, actual[10].SqlDbType);
            Assert.AreEqual(expectedDecimalType, actual[11].SqlDbType);
            //Assert.AreEqual(expectedMoneyType, actual[11].SqlDbType);
            //Assert.AreEqual(expectedNCharType, actual[12].SqlDbType);
            Assert.AreEqual(expectedNVarcharType, actual[12].SqlDbType);
            //Assert.AreEqual(expectedNTextType, actual[13].SqlDbType);
            Assert.AreEqual(expectedNVarcharType, actual[13].SqlDbType);
            Assert.AreEqual(expectedNumericType, actual[14].SqlDbType);
            Assert.AreEqual(expectedNVarcharType, actual[15].SqlDbType);
            Assert.AreEqual(expectedRealType, actual[16].SqlDbType);
            //Assert.AreEqual(expectedSmallDateTimeType, actual[17].SqlDbType);
            Assert.AreEqual(expectedDateTimeType, actual[17].SqlDbType);
            Assert.AreEqual(expectedSmallintType, actual[18].SqlDbType);
            Assert.AreEqual(expectedDecimalType, actual[19].SqlDbType);
            //Assert.AreEqual(expectedSmallmoneyType, actual[19].SqlDbType);
            Assert.AreEqual(expectedNVarcharType, actual[20].SqlDbType);
            //Assert.AreEqual(expectedTextType, actual[20].SqlDbType);
            Assert.AreEqual(expectedTimeType, actual[21].SqlDbType);
            //Assert.AreEqual(expectedTimestampType, actual[22].SqlDbType);
            Assert.AreEqual(expectedBinaryType, actual[22].SqlDbType);
            Assert.AreEqual(expectedTinyIntType, actual[23].SqlDbType);
            Assert.AreEqual(expectedUniqueIdentifierType, actual[24].SqlDbType);
            //Assert.AreEqual(expectedVarBinaryType, actual[25].SqlDbType);
            Assert.AreEqual(expectedBinaryType, actual[25].SqlDbType);
            //Assert.AreEqual(expectedVarCharType, actual[26].SqlDbType);
            Assert.AreEqual(expectedNVarcharType, actual[26].SqlDbType);
            //Assert.AreEqual(expectedXmlType, actual[27].SqlDbType);
            Assert.AreEqual(expectedNVarcharType, actual[27].SqlDbType);
        }
    }
}
