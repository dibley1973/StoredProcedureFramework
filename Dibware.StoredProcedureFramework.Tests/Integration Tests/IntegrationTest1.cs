using Dibware.StoredProcedureFramework.Exceptions;
using Dibware.StoredProcedureFramework.Tests.AssertExtensions;
using Dibware.StoredProcedureFramework.Tests.Context;
using Dibware.StoredProcedureFramework.Tests.Fakes.Entities;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.Base;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.AllCommonDataTypes;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.PrecisionAndScale;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.VarCharTestProcedures;
using Dibware.StoredProcedureFrameworkForEF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests
{
    [TestClass]
    public class IntegrationTest1 : BaseIntegrationTest
    {
        #region Test Pre and Clear down

        [TestInitialize]
        public void TestSetup()
        {
            PrepareDatabase();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            CleanupDatabase();
        }

        #endregion

        #region Tests

        #region EntityFrameworkConnection Test

        /// <summary>
        /// Just to check context connects...
        /// </summary>
        [TestMethod]
        public void EntityFrameworkConnectionTest()
        {
            // ARRANGE
            const int expectedCount = 2;
            AddTenentsToContext(Context);

            // ACT
            var tenants = Context.Set<Tenant>().ToList();

            // ASSERT
            Assert.AreEqual(expectedCount, tenants.Count);
        }

        #endregion

        #region ReturnNoResult Tests

        [TestMethod]
        public void ReturnNoResultsProcedure_ReturnsNull()
        {
            // ARRANGE
            var parameters = new NullStoredProcedureParameters();
            var procedure = new ReturnNoResultStoredProcedure(parameters);
            procedure.InitializeFromAttributes();

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.IsNull(results);
        }

        #endregion

        #region AllCommonDataTypes Tests

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

            var parameters = new AllCommonDataTypesParameters
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
            procedure.InitializeFromAttributes();

            // ACT
            List<AllCommonDataTypesReturnType> results = Context.ExecuteStoredProcedure(procedure);
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

        #endregion

        #region GetAllTenants Tests

        [TestMethod]
        public void GetAllTenants_ReturnsCorrectResultCount()
        {
            // ARRANGE
            const int expectedCount = 2;
            const string expectedProcedureName = "Tenant_GetAll";
            const string expectedSchemaName = "app";
            var procedure = new TenantGetAllNoAttributes(expectedSchemaName,
                expectedProcedureName);
            AddTenentsToContext(Context);

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.AreEqual(expectedCount, results.Count);
        }

        [TestMethod]
        public void GetAllTenants_ReturnsResultsCastToCorrectType()
        {
            // ARRANGE
            Type expectedType = typeof(TenantResultRow);
            const string expectedProcedureName = "Tenant_GetAll";
            const string expectedSchemaName = "app";
            var procedure = new TenantGetAllNoAttributes(expectedSchemaName,
                expectedProcedureName);
            AddTenentsToContext(Context);

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.IsInstanceOfType(results.First(), expectedType);
        }

        #endregion

        #region GetTenantForName Tests

        [TestMethod]
        public void GetTenantForName_ReturnsOneRecord()
        {
            // ARRANGE
            const int expectedCount = 1;
            const string expectedName = "Me";
            var parameters = new GetTenantForTenantNameParameters
            {
                TenantName = expectedName
            };
            var procedure = new GetTenantForTenantNameProcedure(parameters);
            procedure.InitializeFromAttributes();
            AddTenentsToContext(Context);

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.AreEqual(expectedCount, results.Count);
        }

        [TestMethod]
        public void GetTenantForName_ReturnsCorrectName()
        {
            // ARRANGE
            const string expectedName = "Me";
            var parameters = new GetTenantForTenantNameParameters
            {
                TenantName = expectedName
            };
            var procedure = new GetTenantForTenantNameProcedure(parameters);
            procedure.InitializeFromAttributes();
            AddTenentsToContext(Context);

            // ACT
            var results = Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.AreEqual(expectedName, results.First().TenantName);
        }

        #endregion

        #region LessColumnsInResultSetThanReturnObject

        [TestMethod]
        public void LessColumnsInResultSetThanReturnObject_DoesWaht()
        {
            // TODO - create test




            // ASSERT
            Assert.Fail();
        }

        #endregion

        #region Precision and Scale Tests

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
            var results = Context.ExecuteStoredProcedure(procedure);
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

        [TestMethod]
        [ExpectedException(typeof(SqlParameterOutOfRangeException))]
        public void CallVarcharProcedureWithIncorrectLength_ThrowsSqlParameterOutOfRangeException()
        {
            // ARRANGE
            const string initialvalue = @"12345678901234567890";
            var parameters = new VarCharTestProcedureParameters
            {
                Parameter1 = initialvalue
            };
            var procedure = new VarCharTestProcedureStoredProcedure(parameters);

            // ACT
            Context.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.Fail();
        }

        #endregion

        //[TestMethod]
        //public void GetAllTenents2_ReturnsAllTenantsCast()
        //{
        //    // ARRANGE
        //    const int expectedCount = 2;
        //    var parameters = new NullStoredProcedureParameters();
        //    var procedure = new TenantGetAllNoAttributes(parameters);
        //    //procedure.SetProcedureName("Company_GetAll");
        //    //procedure.InitializeFromAttributes();

        //    AddTenentsToContext(Context);

        //    // ACT

        //    var results = Context.ExecuteStoredProcedure(procedure);

        //    //List<object> tenantResults = Context.ExecuteStoredProcedure<Tenant>(
        //    //    procedure);

        //    // next we need to be able to get an explicit list as the return rather than an list of objects.


        //    // ASSERT
        //    Assert.AreEqual(expectedCount, results.Count);
        //}



        //[TestMethod]
        //public void GetAllTenents_ReturnsAllTenantsCast()
        //{
        //    // ARRANGE
        //    const int expectedCount = 2;
        //    var procedure = new StoredProcedure<GetTenantForAll>();
        //    //procedure.SetProcedureName("Company_GetAll");
        //    procedure.InitializeFromAttributes();

        //    AddTenentsToContext(Context);

        //    // ACT
        //    List<object> tenantResults = Context.ExecuteStoredProcedure<Tenant>(
        //        procedure);

        //    // next we need to be able to get an explicit list as the return rather than an list of objects.


        //    // ASSERT
        //    Assert.AreEqual(expectedCount, tenantResults.Count);
        //}




        #endregion

        #region Methods Data Setup

        /// <summary>
        /// Adds the tenents to context.
        /// </summary>
        /// <param name="context">The context.</param>
        private void AddTenentsToContext(IntegrationTestContext context)
        {
            context.Tenants.Add(new Tenant()
            {
                IsActive = true,
                TenantId = 1,
                TenantName = "Me",
                RecordCreatedDateTime = DateTime.Now
            });
            context.Tenants.Add(new Tenant()
            {
                IsActive = true,
                TenantId = 2,
                TenantName = "You",
                RecordCreatedDateTime = DateTime.Now
            });
            context.SaveChanges();
        }

        #endregion
    }
}