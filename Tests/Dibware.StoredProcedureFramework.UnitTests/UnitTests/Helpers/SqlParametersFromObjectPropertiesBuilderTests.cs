using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures;
using Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures.Parameters;
using Dibware.StoredProcedureFramework.Tests.UnitTests.UserDefinedTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class SqlParametersFromObjectPropertiesBuilderTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructWithNull_ThrowsException()
        {
            // ACT
            new SqlParametersFromObjectPropertiesBuilder<NullStoredProcedureParameters>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SqlParameters_WhenSuppliedProcedureHasNullStoredProcedureParameters_ParametersAreNull()
        {
            // ARRANGE
            StoredProcedureWithParameters.BasicParameters parameters = null;

            // ACT
            var sqlParameterBuilder = new SqlParametersFromObjectPropertiesBuilder<StoredProcedureWithParameters.BasicParameters>(parameters);
            var actual = sqlParameterBuilder.SqlParameters;

            //
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void SqlParameters_WhenSuppliedProcedureHasNullableParameterswithNullValues_ParametersHaveDbNullSqlValues()
        {
            // ARRANGE
            const int expectedCount = 2;
            DBNull expectedValue = DBNull.Value;
            var parameters = new StoredProcedureWithNullableParameters.BasicParameters
            {
                Id = null,
                Name = null
            };

            // ACT
            var sqlParameterBuilder = new SqlParametersFromObjectPropertiesBuilder<StoredProcedureWithNullableParameters.BasicParameters>(parameters);
            sqlParameterBuilder.BuildSqlParameters();
            var actual = sqlParameterBuilder.SqlParameters.ToList();

            // ARRANGE
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCount, actual.Count);
            Assert.AreEqual(expectedValue, actual[0].Value);
            Assert.AreEqual(expectedValue, actual[1].Value);
        }

        [TestMethod]
        public void SqlParameters_WhenSuppliedWithParameterWhichHasoutputDirectionAttribute_SetsDirectionCorrectly()
        {
            // ARRANGE
            var parameters = new OutputParameterStoredProcedure.Parameter
            {
                Value1 = "Ted",
                Value2 = 0
            };

            // ACT
            var sqlParameterBuilder = new SqlParametersFromObjectPropertiesBuilder<OutputParameterStoredProcedure.Parameter>(parameters);
            sqlParameterBuilder.BuildSqlParameters();
            var sqlParameters = sqlParameterBuilder.SqlParameters.ToList();
            var actual1 = sqlParameters[0];
            var actual2 = sqlParameters[1];

            // ASSERT
            Assert.AreEqual(ParameterDirection.Input, actual1.Direction);
            Assert.AreEqual(ParameterDirection.Output, actual2.Direction);
        }

        //[TestMethod]
        //public void SqlParameters_WhenSuppliedDecimalPrecisionAndScaleParameterAndObject



        [TestMethod]
        public void SqlParameters_WhenSuppliedTableValueParameters_HasCorrect()
        {
            // ARRANGE
            const SqlDbType expectedSqlDbType = SqlDbType.Structured;
            var itemsToAdd = new List<SimpleParameterTableType>
            {
                new SimpleParameterTableType { Name = "Company 1", IsActive = true, Id = 2 },
                new SimpleParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
                new SimpleParameterTableType { Name = "Company 3", IsActive = true, Id = 2 }
            };
            var parameters = new TableValueParameterWithoutReturnTypeStoredProcedure.Parameter
            {
                TvpParameters = itemsToAdd
            };

            // ACT
            var sqlParameterBuilder = new SqlParametersFromObjectPropertiesBuilder<TableValueParameterWithoutReturnTypeStoredProcedure.Parameter>(parameters);
            sqlParameterBuilder.BuildSqlParameters();
            var sqlParameters = sqlParameterBuilder.SqlParameters.ToList();
            var actual = sqlParameters[0];

            // ASSERT
            Assert.AreEqual(expectedSqlDbType, actual.SqlDbType);
        }

        [TestMethod]
        [ExpectedException(typeof(SqlParameterInvalidDataTypeException))]
        public void BuildSqlParameters_WhenSuppliedWithWrongDataTypeDecimalParameter_ThrowsSqlParameterInvalidDataTypeException()
        {
            // ARRANGE
            var parameters = new WrongDataTypeDecimalParameter
            {
                Value1 = 99999,
                Value2 = "Bert"
            };

            // ACT
            var sqlParameterBuilder = new SqlParametersFromObjectPropertiesBuilder<WrongDataTypeDecimalParameter>(parameters);
            sqlParameterBuilder.BuildSqlParameters();

            // ASSERT
            // Exception should already be thrown
        }

        [TestMethod]
        [ExpectedException(typeof(SqlParameterInvalidDataTypeException))]
        public void BuildSqlParameters_WhenSuppliedWithWrongDataTypeStringParameter_ThrowsSqlParameterInvalidDataTypeException()
        {
            // ARRANGE
            var parameters = new WrongDataTypeStringParameter
            {
                Value1 = 99999,
                Value2 = true
            };

            // ACT
            var sqlParameterBuilder = new SqlParametersFromObjectPropertiesBuilder<WrongDataTypeStringParameter>(parameters);
            sqlParameterBuilder.BuildSqlParameters();

            // ASSERT
            // Exception should already be thrown
        }

        [TestMethod]
        public void SqlParameter_WhenSuppliedWithStringTypeParametersWithNullValues_ThenValueIsNull()
        {
            // ARRANGE
            DBNull expectedValue = DBNull.Value;
            var parameters = new StringTypeParameters
            {
                Value1 = null,
                Value2 = null
            };

            var sqlParameterBuilder = new SqlParametersFromObjectPropertiesBuilder<StringTypeParameters>(parameters);
            sqlParameterBuilder.BuildSqlParameters();
            var sqlParameters = sqlParameterBuilder.SqlParameters.ToList();
            var actual1 = sqlParameters[0];
            var actual2 = sqlParameters[1];

            // ASSERT
            Assert.AreEqual(expectedValue, actual1.Value);
            Assert.AreEqual(expectedValue, actual2.Value);
        }


        // NullableIntegerParameters
        //[TestMethod]
        //[ExpectedException(typeof(SqlNullValueException))]
        //public void SqlParameter_WhenSuppliedWithNullableIntegerParametersWithNullValues_ThrowsException()
        //{
        //    // ARRANGE
        //    DBNull expectedValue = DBNull.Value;
        //    var parameters = new NullableIntegerParameters
        //    {
        //        Value1 = null,
        //        Value2 = null
        //    };

        //    var sqlParameterBuilder = new SqlParametersFromObjectPropertiesBuilder<NullableIntegerParameters>(parameters);
        //    sqlParameterBuilder.BuildSqlParameters();
        //    var sqlParameters = sqlParameterBuilder.SqlParameters.ToList();
        //    var actual1 = sqlParameters[0];
        //    var actual2 = sqlParameters[1];

        //    // ASSERT
        //    Assert.AreEqual(expectedValue, actual1.Value);
        //    Assert.AreEqual(expectedValue, actual2.Value);
        //}


        [TestMethod]
        public void BuildParameters_WhenSuppliedProcedureHasParameters_BuildsCorrectQuantityOfParameters()
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
            var sqlParameterBuilder =
                new SqlParametersFromObjectPropertiesBuilder<
                    AllCommonDataTypesStoredProcedureWithParameterAttributes.Parameter>(parameters);

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
            const SqlDbType expectedBigIntType = SqlDbType.BigInt;
            const SqlDbType expectedBinaryType = SqlDbType.Binary;
            const SqlDbType expectedBitType = SqlDbType.Bit;
            const SqlDbType expectedCharType = SqlDbType.Char;
            const SqlDbType expectedDateType = SqlDbType.Date;
            const SqlDbType expectedDateTimeType = SqlDbType.DateTime;
            const SqlDbType expectedDateTime2Type = SqlDbType.DateTime2;
            const SqlDbType expectedDecimalType = SqlDbType.Decimal;
            const SqlDbType expectedFloatType = SqlDbType.Float;
            const SqlDbType expectedImageType = SqlDbType.Image;
            const SqlDbType expectedintType = SqlDbType.Int;
            const SqlDbType expectedMoneyType = SqlDbType.Money;
            const SqlDbType expectedNCharType = SqlDbType.NChar;
            const SqlDbType expectedNTextType = SqlDbType.NText;
            const SqlDbType expectedNumericType = SqlDbType.Decimal;
            const SqlDbType expectedNVarcharType = SqlDbType.NVarChar;
            const SqlDbType expectedRealType = SqlDbType.Real;
            const SqlDbType expectedSmallDateTimeType = SqlDbType.SmallDateTime;
            const SqlDbType expectedSmallintType = SqlDbType.SmallInt;
            const SqlDbType expectedSmallmoneyType = SqlDbType.SmallMoney;
            const SqlDbType expectedTextType = SqlDbType.Text;
            const SqlDbType expectedTimeType = SqlDbType.Time;
            const SqlDbType expectedTimestampType = SqlDbType.Timestamp;
            const SqlDbType expectedTinyIntType = SqlDbType.TinyInt;
            const SqlDbType expectedUniqueIdentifierType = SqlDbType.UniqueIdentifier;
            const SqlDbType expectedVarBinaryType = SqlDbType.VarBinary;
            const SqlDbType expectedVarCharType = SqlDbType.VarChar;
            const SqlDbType expectedXmlType = SqlDbType.Xml;

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
            var sqlParameterBuilder =
                new SqlParametersFromObjectPropertiesBuilder<
                    AllCommonDataTypesStoredProcedureWithParameterAttributes.Parameter>(parameters);

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
            const SqlDbType expectedBigIntType = SqlDbType.BigInt;
            const SqlDbType expectedBinaryType = SqlDbType.Binary;
            const SqlDbType expectedBitType = SqlDbType.Bit;
            const SqlDbType expectedCharType = SqlDbType.Char;
            const SqlDbType expectedDateTimeType = SqlDbType.DateTime;
            const SqlDbType expectedDecimalType = SqlDbType.Decimal;
            const SqlDbType expectedFloatType = SqlDbType.Float;
            //const SqlDbType expectedImageType = SqlDbType.Image;
            const SqlDbType expectedintType = SqlDbType.Int;
            //const SqlDbType expectedMoneyType = SqlDbType.Money;
            //const SqlDbType expectedNCharType = SqlDbType.NChar;
            //const SqlDbType expectedNTextType = SqlDbType.NText;
            const SqlDbType expectedNumericType = SqlDbType.Decimal;
            const SqlDbType expectedNVarcharType = SqlDbType.NVarChar;
            const SqlDbType expectedRealType = SqlDbType.Real;
            //const SqlDbType expectedSmallDateTimeType = SqlDbType.SmallDateTime;
            const SqlDbType expectedSmallintType = SqlDbType.SmallInt;
            //const SqlDbType expectedSmallmoneyType = SqlDbType.SmallMoney;
            //const SqlDbType expectedTextType = SqlDbType.Text;
            const SqlDbType expectedTimeType = SqlDbType.Time;
            //const SqlDbType expectedTimestampType = SqlDbType.Timestamp;
            const SqlDbType expectedTinyIntType = SqlDbType.TinyInt;
            const SqlDbType expectedUniqueIdentifierType = SqlDbType.UniqueIdentifier;
            //const SqlDbType expectedVarBinaryType = SqlDbType.VarBinary;
            //const SqlDbType expectedVarCharType = SqlDbType.VarChar;
            //const SqlDbType expectedXmlType = SqlDbType.Xml;

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
            var sqlParameterBuilder =
                new SqlParametersFromObjectPropertiesBuilder<
                    AllCommonDataTypesStoredProcedureWithoutParameterAttributes.Parameter>(parameters);

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
            Assert.AreEqual(expectedintType, actual[10].SqlDbType);
            Assert.AreEqual(expectedDecimalType, actual[11].SqlDbType);
            Assert.AreEqual(expectedNVarcharType, actual[12].SqlDbType);
            Assert.AreEqual(expectedNVarcharType, actual[13].SqlDbType);
            Assert.AreEqual(expectedNumericType, actual[14].SqlDbType);
            Assert.AreEqual(expectedNVarcharType, actual[15].SqlDbType);
            Assert.AreEqual(expectedRealType, actual[16].SqlDbType);
            Assert.AreEqual(expectedDateTimeType, actual[17].SqlDbType);
            Assert.AreEqual(expectedSmallintType, actual[18].SqlDbType);
            Assert.AreEqual(expectedDecimalType, actual[19].SqlDbType);
            Assert.AreEqual(expectedNVarcharType, actual[20].SqlDbType);
            Assert.AreEqual(expectedTimeType, actual[21].SqlDbType);
            Assert.AreEqual(expectedBinaryType, actual[22].SqlDbType);
            Assert.AreEqual(expectedTinyIntType, actual[23].SqlDbType);
            Assert.AreEqual(expectedUniqueIdentifierType, actual[24].SqlDbType);
            Assert.AreEqual(expectedBinaryType, actual[25].SqlDbType);
            Assert.AreEqual(expectedNVarcharType, actual[26].SqlDbType);
            Assert.AreEqual(expectedNVarcharType, actual[27].SqlDbType);
        }
    }
}
