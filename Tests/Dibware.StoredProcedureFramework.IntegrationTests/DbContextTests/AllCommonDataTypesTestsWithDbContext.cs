using System;
using System.Linq;
using Dibware.StoredProcedureFramework.IntegrationTests.AssertExtensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests
{
    [TestClass]
    public class AllCommonDataTypesTestsWithDbContext : BaseDbContextIntegrationTest
    {
        [TestMethod]
        public void AllCommonDataTypes_ReturnsCorrectDataTypes()
        {
            // ARRANGE
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

            var parameters = new AllCommonDataTypesStoredProcedure.Parameter
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
            var procedure = new AllCommonDataTypesStoredProcedure(parameters);

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);
            var result = results.First();

            // ASSERT
            Assert.AreEqual(expectedBigint, result.BigInt);
            AssertExtension.ByteArrayEqual(expectedBinary, result.Binary);
            Assert.AreEqual(expectedBit, result.Bit);
            Assert.AreEqual(string.Join("", expectedChar), result.Char);
            Assert.AreEqual(expectedDate, result.Date);
            Assert.IsTrue((expectedDatetime - result.Datetime).Seconds == 0);
            Assert.IsTrue((expectedDatetime2 - result.Datetime2).Seconds == 0);
            Assert.AreEqual(expectedDecimal, result.Decimal);
            Assert.AreEqual(expectedFloat, result.Float);
            AssertExtension.ByteArrayEqual(expectedImage, result.Image);
            Assert.AreEqual(expectedInt, result.Int);
            Assert.AreEqual(expectedMoney, result.Money);
            Assert.AreEqual(expectedNChar, result.NChar);
            Assert.AreEqual(expectedNText, result.NText);
            Assert.AreEqual(expectedNumeric, result.Numeric);
            Assert.AreEqual(expectedNVarchar, result.NVarchar);
            Assert.AreEqual(expectedReal, result.Real);
            Assert.IsTrue((expectedSmalldatetime - result.SmallDateTime).Seconds == 0);
            Assert.AreEqual(expectedSmallint, result.SmallInt);
            Assert.AreEqual(expectedSmallmoney, result.Smallmoney);
            Assert.AreEqual(expectedText, result.Text);
            Assert.AreEqual(expectedTime, result.Time);
            AssertExtension.ByteArrayEqual(expectedTimestamp, result.Timestamp);
            Assert.AreEqual(expectedTinyInt, result.TinyInt);
            Assert.AreEqual(expectedUniqueIdentifier, result.UniqueIdentifier);
            AssertExtension.ByteArrayEqual(expectedVarBinary, result.Varbinary);
            Assert.AreEqual(expectedVarChar, result.Varchar);
            Assert.AreEqual(expectedXml, result.Xml);
        }
    }
}
