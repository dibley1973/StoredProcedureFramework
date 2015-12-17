using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.SqlServer.Server;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class TableValuedParameterHelperTests
    {
        [TestMethod]
        public void GetTableValuedParameterFromList_FromValidList_ReturnsInstatiatedCollection()
        {
            // ARRANGE
            var itemList = new List<SimpleParameterTableType>
            {
                new SimpleParameterTableType { Name = "Company 1", IsActive = true, Id = 2 },
                new SimpleParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
                new SimpleParameterTableType { Name = "Company 3", IsActive = true, Id = 2 }
            };

            // ACT
            var actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList);

            // ASSERT
            Assert.IsNotNull(actualSqlDataRecords);
        }

        [TestMethod]
        public void GetTableValuedParameterFromList_FromValidList_ReturnsCorrectRecordCount()
        {
            // ARRANGE
            const int expectedCount = 3;
            var itemList = new List<SimpleParameterTableType>
            {
                new SimpleParameterTableType { Name = "Company 1", IsActive = true, Id = 2 },
                new SimpleParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
                new SimpleParameterTableType { Name = "Company 3", IsActive = true, Id = 2 }
            };

            // ACT
            var actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList);

            // ASSERT
            Assert.AreEqual(expectedCount, actualSqlDataRecords.Count());
        }

        [TestMethod]
        public void GetTableValuedParameterFromList_FromValidList_ReturnsCorrectFieldCount()
        {
            // ARRANGE
            const int expectedFieldCount = 3;
            var itemList = new List<SimpleParameterTableType>
            {
                new SimpleParameterTableType { Name = "Company 1", IsActive = true, Id = 1 },
                new SimpleParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
                new SimpleParameterTableType { Name = "Company 3", IsActive = true, Id = 3 },
                new SimpleParameterTableType { Name = "Company 4", IsActive = true, Id = 4 }
            };

            // ACT
            var actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList).ToList();

            // ASSERT
            Assert.AreEqual(expectedFieldCount, actualSqlDataRecords.First().FieldCount);
            Assert.AreEqual(expectedFieldCount, actualSqlDataRecords.Last().FieldCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetTableValuedParameterFromList_FromNullList_ThrowsArgumentNullException()
        {
            // ARRANGE

            // ACT
            var actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(null);

            // ASSERT
            Assert.IsNotNull(actualSqlDataRecords);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTableValuedParameterFromList_FromListOfNulls_ThrowsNullReferenceException()
        {
            // ARRANGE
            var itemList = new List<SimpleParameterTableType>
            {
                null,
                null,
                null
            };

            // ACT
            var actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList);

            // ASSERT
            Assert.IsNotNull(actualSqlDataRecords);
        }

        [TestMethod]
        public void GetTableValuedParameterFromList_FromEmptyList_ReturnsNull()
        {
            // ARRANGE
            var itemList = new List<SimpleParameterTableType>();

            // ACT
            var actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList);

            // ASSERT
            Assert.IsNull(actualSqlDataRecords);
        }

        [TestMethod]
        public void GetTableValuedParameterFromList_UsingListWithNameAttributes_ReturnsFieldsWithOriginalNames()
        {
            // ARRANGE
            const string expectedField1Name = "Id";
            const string expectedField2Name = "IsActive";
            const string expectedField3Name = "Name";
            var itemList = new List<SimpleParameterTableType>
            {
                new SimpleParameterTableType { Name = "Company 1", IsActive = true, Id = 1 }
            };

            // ACT
            IEnumerable<SqlDataRecord> actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList).ToList();
            SqlDataRecord firstRecord = actualSqlDataRecords.First();
            var firstRecordFirstFieldName = firstRecord.GetName(0);
            var firstRecordSecondFieldName = firstRecord.GetName(1);
            var firstRecordThirdFieldName = firstRecord.GetName(2);

            // ASSERT
            Assert.AreEqual(expectedField1Name, firstRecordFirstFieldName);
            Assert.AreEqual(expectedField2Name, firstRecordSecondFieldName);
            Assert.AreEqual(expectedField3Name, firstRecordThirdFieldName);
        }

        [TestMethod]
        public void GetTableValuedParameterFromList_UsingListWithNameAttributes_ReturnsMetaDataFieldsWithAttributeNames()
        {
            // ARRANGE
            const string expectedField1Name = "RecordId";
            const string expectedField2Name = "Active";
            const string expectedField3Name = "Fullname";
            var itemList = new List<SimpleParameterWithNameAttributeTableType>
            {
                new SimpleParameterWithNameAttributeTableType { Name = "Company 1", IsActive = true, Id = 1 }
            };

            // ACT
            IEnumerable<SqlDataRecord> actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList).ToList();
            SqlDataRecord firstRecord = actualSqlDataRecords.First();
            var firstRecordFirstMetaData = firstRecord.GetSqlMetaData(0);
            var firstRecordSecondMetaData = firstRecord.GetSqlMetaData(1);
            var firstRecordThirdMetaData = firstRecord.GetSqlMetaData(2);

            // ASSERT
            Assert.AreEqual(expectedField1Name, firstRecordFirstMetaData.Name);
            Assert.AreEqual(expectedField2Name, firstRecordSecondMetaData.Name);
            Assert.AreEqual(expectedField3Name, firstRecordThirdMetaData.Name);
        }


        [TestMethod]
        public void GetTableValuedParameterFromListWithDecimalsWithoutPrecisionAttribute_ReturnsPrecisionOfOriginalValue()
        {
            // ARRANGE
            const int expectedFirstFieldPrecision = 3;
            const int expectedSecondFieldPrecision = 4;
            const int expectedThirdFieldPrecision = 5;
            var itemList = new List<DecimalParameterTableType>
            {
                new DecimalParameterTableType { Value1 = 30.1M, Value2 = 30.02M, Value3 = 30.003M }
            };

            // ACT
            IEnumerable<SqlDataRecord> actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList).ToList();
            SqlDataRecord firstRecord = actualSqlDataRecords.First();
            var firstFieldOfFirstRecord = firstRecord.GetSqlDecimal(0);
            var secondFieldOfFirstRecord = firstRecord.GetSqlDecimal(1);
            var thirdFieldOfFirstRecord = firstRecord.GetSqlDecimal(2);
            
            // ASSERT
            Assert.AreEqual(expectedFirstFieldPrecision, firstFieldOfFirstRecord.Precision);
            Assert.AreEqual(expectedSecondFieldPrecision, secondFieldOfFirstRecord.Precision);
            Assert.AreEqual(expectedThirdFieldPrecision, thirdFieldOfFirstRecord.Precision);
        }

        [TestMethod]
        public void GetTableValuedParameterFromListWithDecimalsWithPrecisionAttribute_ReturnsPrecisionOfAttributeForSqlMetatData()
        {
            // ARRANGE
            const int expectedFirstFieldPrecision = 7;
            const int expectedSecondFieldPrecision = 8;
            const int expectedThirdFieldPrecision = 9;
            var itemList = new List<DecimalParameterWithAttributeTableType>
            {
                new DecimalParameterWithAttributeTableType { Value1 = 30.11111111M, Value2 = 30.022222M, Value3 = 30.003333M }
            };

            // ACT
            IEnumerable<SqlDataRecord> actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList).ToList();
            SqlDataRecord firstRecord = actualSqlDataRecords.First();
            var firstFieldOfFirstRecordMetaData = firstRecord.GetSqlMetaData(0);
            var secondFieldOfFirstRecordMetaData = firstRecord.GetSqlMetaData(1);
            var thirdFieldOfFirstRecordMetaData = firstRecord.GetSqlMetaData(2);
            
            // ASSERT
            Assert.AreEqual(expectedFirstFieldPrecision, firstFieldOfFirstRecordMetaData.Precision);
            Assert.AreEqual(expectedSecondFieldPrecision, secondFieldOfFirstRecordMetaData.Precision);
            Assert.AreEqual(expectedThirdFieldPrecision, thirdFieldOfFirstRecordMetaData.Precision);
        }

        [TestMethod]
        public void GetTableValuedParameterFromListWithDecimalsWithoutScaleAttributes_ReturnsScaleOfOriginalValue()
        {
            // ARRANGE
            const int expectedFirstFieldScale = 1;
            const int expectedSecondFieldScale = 2;
            const int expectedThirdFieldScale = 3;
            var itemList = new List<DecimalParameterTableType>
            {
                new DecimalParameterTableType { Value1 = 30.1M, Value2 = 30.02M, Value3 = 30.003M }
            };

            // ACT
            IEnumerable<SqlDataRecord> actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList).ToList();
            SqlDataRecord firstRecord = actualSqlDataRecords.First();
            var firstFieldOfFirstRecord = firstRecord.GetSqlDecimal(0);
            var secondFieldOfFirstRecord = firstRecord.GetSqlDecimal(1);
            var thirdFieldOfFirstRecord = firstRecord.GetSqlDecimal(2);

            // ASSERT
            Assert.AreEqual(expectedFirstFieldScale, firstFieldOfFirstRecord.Scale);
            Assert.AreEqual(expectedSecondFieldScale, secondFieldOfFirstRecord.Scale);
            Assert.AreEqual(expectedThirdFieldScale, thirdFieldOfFirstRecord.Scale);
        }

        [TestMethod]
        public void GetTableValuedParameterFromListWithDecimalsWithScaleAttribute_ReturnsScaleOfAttributeForSqlMetatData()
        {
            // ARRANGE
            const int expectedFirstFieldScale = 4;
            const int expectedSecondFieldScale = 5;
            const int expectedThirdFieldScale = 6;
            var itemList = new List<DecimalParameterWithAttributeTableType>
            {
                new DecimalParameterWithAttributeTableType { Value1 = 30.11111111M, Value2 = 30.022222M, Value3 = 30.003333M }
            };

            // ACT
            IEnumerable<SqlDataRecord> actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList).ToList();
            SqlDataRecord firstRecord = actualSqlDataRecords.First();
            var firstFieldOfFirstRecordMetaData = firstRecord.GetSqlMetaData(0);
            var secondFieldOfFirstRecordMetaData = firstRecord.GetSqlMetaData(1);
            var thirdFieldOfFirstRecordMetaData = firstRecord.GetSqlMetaData(2);

            // ASSERT
            Assert.AreEqual(expectedFirstFieldScale, firstFieldOfFirstRecordMetaData.Scale);
            Assert.AreEqual(expectedSecondFieldScale, secondFieldOfFirstRecordMetaData.Scale);
            Assert.AreEqual(expectedThirdFieldScale, thirdFieldOfFirstRecordMetaData.Scale);
        }


        [TestMethod]
        public void GetTableValuedParameterFromListWithStringsWithoutSizeAttribute_ReturnsSizeOfOriginalValue()
        {
            // ARRANGE
            const int expectedFirstFieldSize = 3;
            const int expectedSecondFieldSize = 4;
            const int expectedThirdFieldSize = 5;
            var itemList = new List<StringParameterTableType>
            {
                new StringParameterTableType { Value1 = "123", Value2 = "1234", Value3 = "12345" }
            };

            // ACT
            IEnumerable<SqlDataRecord> actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList).ToList();
            SqlDataRecord firstRecord = actualSqlDataRecords.First();
            var firstFieldOfFirstRecord = firstRecord.GetSqlString(0);
            var secondFieldOfFirstRecord = firstRecord.GetSqlString(1);
            var thirdFieldOfFirstRecord = firstRecord.GetSqlString(2);

            // ASSERT
            Assert.AreEqual(expectedFirstFieldSize, firstFieldOfFirstRecord.Value.Length);
            Assert.AreEqual(expectedSecondFieldSize, secondFieldOfFirstRecord.Value.Length);
            Assert.AreEqual(expectedThirdFieldSize, thirdFieldOfFirstRecord.Value.Length);
        }

        [TestMethod]
        public void GetTableValuedParameterFromListWithStringsWithSizeAttribute_ReturnsSizeOfAttributeForSqlMetatDataMaxLength()
        {
            // ARRANGE
            const int expectedFirstFieldMaxLength = 5;
            const int expectedSecondFieldSizeMaxLength = 6;
            const int expectedThirdFieldSizeMaxLength = 7;
            var itemList = new List<StringParameterWithAttributeTableType>
            {
                new StringParameterWithAttributeTableType { Value1 = "123456", Value2 = "123456", Value3 = "123456" }
            };

            // ACT
            IEnumerable<SqlDataRecord> actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList).ToList();
            SqlDataRecord firstRecord = actualSqlDataRecords.First();
            var firstFieldOfFirstRecordMetaData = firstRecord.GetSqlMetaData(0);
            var secondFieldOfFirstRecordMetaData = firstRecord.GetSqlMetaData(1);
            var thirdFieldOfFirstRecordMetaData = firstRecord.GetSqlMetaData(2);

            // ASSERT
            Assert.AreEqual(expectedFirstFieldMaxLength, firstFieldOfFirstRecordMetaData.MaxLength);
            Assert.AreEqual(expectedSecondFieldSizeMaxLength, secondFieldOfFirstRecordMetaData.MaxLength);
            Assert.AreEqual(expectedThirdFieldSizeMaxLength, thirdFieldOfFirstRecordMetaData.MaxLength);
        }

        /// <summary>
        /// Represents a simple table type
        /// </summary>
        private class SimpleParameterTableType
        {
            public int Id { get; set; }
            public bool IsActive { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// Represents a simple table type with name Attribute
        /// </summary>
        private class SimpleParameterWithNameAttributeTableType
        {
            [Name("RecordId")]
            public int Id { get; set; }
            [Name("Active")]
            public bool IsActive { get; set; }
            [Name("Fullname")]
            public string Name { get; set; }
        }

        /// <summary>
        /// Represents a table type with decimal fields
        /// </summary>
        private class DecimalParameterTableType
        {
            public decimal Value1 { get; set; }
            public decimal Value2 { get; set; }
            public decimal Value3 { get; set; }
        }

        /// <summary>
        /// Represents a table type with decimal fields and Precision and Scale attributes
        /// </summary>
        private class DecimalParameterWithAttributeTableType
        {
            [Precision(7)]
            [Scale(4)]
            public decimal Value1 { get; set; }
            [
            Precision(8)]
            [Scale(5)]
            public decimal Value2 { get; set; }
            
            [Precision(9)]
            [Scale(6)]
            public decimal Value3 { get; set; }
        }

        private class StringParameterTableType
        {
            public string Value1 { get; set; }
            public string Value2 { get; set; }
            public string Value3 { get; set; }
        }

        private class StringParameterWithAttributeTableType
        {
            [Size(5)]
            public string Value1 { get; set; }

            [Size(6)]
            public string Value2 { get; set; }

            [Size(7)]
            public string Value3 { get; set; }
        }
    }
}
