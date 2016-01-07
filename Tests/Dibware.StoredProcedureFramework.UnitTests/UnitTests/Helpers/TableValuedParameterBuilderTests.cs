using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.SqlServer.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class TableValuedParameterBuilderTests
    {
        #region Constructor Tests

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ConstructedWithNullItemList_ThrowsException()
        {
            // ARRANGE
            List<SimpleParameterTableType> itemList = null;

            // ACT
            // ReSharper disable once ExpressionIsAlwaysNull
            new TableValuedParameterBuilder(itemList);

            // ASSERT
        }

        [TestMethod]
        public void Constructor_ConstructedWithNonNullItemList_DoesNotThrowsException()
        {
            // ARRANGE
            var itemList = new List<SimpleParameterTableType>
            {
                new SimpleParameterTableType { Name = "Company 1", IsActive = true, Id = 2 }
            };

            // ACT
            new TableValuedParameterBuilder(itemList);

            // ASSERT
        }

        #endregion

        #region TableValueParameters

        [TestMethod]
        public void TableValueParameters_WhenConstructedWithValidListAndBuilt_ReturnsInstatiatedCollection()
        {
            // ARRANGE
            var itemList = new List<SimpleParameterTableType>
            {
                new SimpleParameterTableType { Name = "Company 1", IsActive = true, Id = 2 },
                new SimpleParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
                new SimpleParameterTableType { Name = "Company 3", IsActive = true, Id = 2 }
            };

            // ACT
            IEnumerable<SqlDataRecord> actualSqlDataRecords = new TableValuedParameterBuilder(itemList)
                .BuildParameters()
                .TableValueParameters;

            // ASSERT
            Assert.IsNotNull(actualSqlDataRecords);
        }

        [TestMethod]
        public void TableValueParameters_WhenConstructedWithValidListAndNotBuilt_ReturnsNull()
        {
            // ARRANGE
            var itemList = new List<SimpleParameterTableType>
            {
                new SimpleParameterTableType { Name = "Company 1", IsActive = true, Id = 2 },
                new SimpleParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
                new SimpleParameterTableType { Name = "Company 3", IsActive = true, Id = 2 }
            };

            // ACT
            IEnumerable<SqlDataRecord> actualSqlDataRecords = new TableValuedParameterBuilder(itemList)
                .TableValueParameters;

            // ASSERT
            Assert.IsNull(actualSqlDataRecords);
        }


        [TestMethod]
        public void TableValueParameters_WhenConstructedWithValidList_ReturnsCorrectRecordCount()
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
            var actualSqlDataRecords = new TableValuedParameterBuilder(itemList)
                .BuildParameters()
                .TableValueParameters;

            // ASSERT
            Assert.AreEqual(expectedCount, actualSqlDataRecords.Count());
        }

        [TestMethod]
        public void TableValueParameters_WhenConstructedWithValidList_ReturnsCorrectFieldCount()
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
            var actualSqlDataRecords = new TableValuedParameterBuilder(itemList)
                .BuildParameters()
                .TableValueParameters;

            // ASSERT
            Assert.AreEqual(expectedFieldCount, actualSqlDataRecords.First().FieldCount);
            Assert.AreEqual(expectedFieldCount, actualSqlDataRecords.Last().FieldCount);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TableValueParameters_WhenConstructedWithListOfNulls_ThrowsNullReferenceException()
        {
            // ARRANGE
            var itemList = new List<SimpleParameterTableType>
            {
                null,
                null,
                null
            };

            // ACT
            var actualSqlDataRecords = new TableValuedParameterBuilder(itemList)
                .BuildParameters()
                .TableValueParameters;

            // ASSERT
            // Should have thrown exception by here!
        }

        [TestMethod]
        public void TableValueParameters_WhenConstructedWithEmptyList_ReturnsNull()
        {
            // ARRANGE
            var itemList = new List<SimpleParameterTableType>();

            // ACT
            var actualSqlDataRecords = new TableValuedParameterBuilder(itemList)
                .BuildParameters()
                .TableValueParameters;

            // ASSERT
            Assert.IsNull(actualSqlDataRecords);
        }

        [TestMethod]
        public void TableValueParameters_WhenConstructedUsingListWithNameAttributes_ReturnsFieldsWithOriginalNames()
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
            IEnumerable<SqlDataRecord> actualSqlDataRecords = new TableValuedParameterBuilder(itemList)
                .BuildParameters()
                .TableValueParameters;
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
        public void TableValueParameters_WhenConstructedUsingListWithNameAttributes_ReturnsMetaDataFieldsWithAttributeNames()
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
            IEnumerable<SqlDataRecord> actualSqlDataRecords = new TableValuedParameterBuilder(itemList).BuildParameters().TableValueParameters;
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
        public void TableValueParameters_WhenConstructedUsingDecimalsWithoutPrecisionAttribute_ReturnsPrecisionOfOriginalValue()
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
            IEnumerable<SqlDataRecord> actualSqlDataRecords = new TableValuedParameterBuilder(itemList).BuildParameters().TableValueParameters;
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
        public void TableValueParameters_WhenConstructedUsingDecimalsWithPrecisionAttribute_ReturnsPrecisionOfAttributeForSqlMetatData()
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
            IEnumerable<SqlDataRecord> actualSqlDataRecords = new TableValuedParameterBuilder(itemList).BuildParameters().TableValueParameters;
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
        public void TableValueParameters_WhenConstructedUsingWithDecimalsWithoutScaleAttributes_ReturnsScaleOfOriginalValue()
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
            IEnumerable<SqlDataRecord> actualSqlDataRecords = new TableValuedParameterBuilder(itemList).BuildParameters().TableValueParameters;
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
        public void TableValueParameters_WhenConstructedUsingDecimalsWithScaleAttribute_ReturnsScaleOfAttributeForSqlMetatData()
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
            IEnumerable<SqlDataRecord> actualSqlDataRecords = new TableValuedParameterBuilder(itemList)
                .BuildParameters()
                .TableValueParameters;
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
        public void TableValueParameters_WhenConstructedUsingStringsWithoutSizeAttribute_ReturnsSizeOfOriginalValue()
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
            IEnumerable<SqlDataRecord> actualSqlDataRecords = new TableValuedParameterBuilder(itemList).BuildParameters().TableValueParameters;
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
        public void TableValueParameters_WhenConstructedUsingStringsWithSizeAttribute_ReturnsSizeOfAttributeForSqlMetatDataMaxLength()
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
            IEnumerable<SqlDataRecord> actualSqlDataRecords = new TableValuedParameterBuilder(itemList).BuildParameters().TableValueParameters;
            SqlDataRecord firstRecord = actualSqlDataRecords.First();
            var firstFieldOfFirstRecordMetaData = firstRecord.GetSqlMetaData(0);
            var secondFieldOfFirstRecordMetaData = firstRecord.GetSqlMetaData(1);
            var thirdFieldOfFirstRecordMetaData = firstRecord.GetSqlMetaData(2);

            // ASSERT
            Assert.AreEqual(expectedFirstFieldMaxLength, firstFieldOfFirstRecordMetaData.MaxLength);
            Assert.AreEqual(expectedSecondFieldSizeMaxLength, secondFieldOfFirstRecordMetaData.MaxLength);
            Assert.AreEqual(expectedThirdFieldSizeMaxLength, thirdFieldOfFirstRecordMetaData.MaxLength);
        }

        [TestMethod]
        public void TableValueParameters_WhenConstructedUsingListWithoutDbTypeAttribute_ReturnsFieldsWithOriginalDataType()
        {
            // ARRANGE
            const SqlDbType expectedField1SqlDbType = SqlDbType.Int;
            const SqlDbType expectedField2SqlDbType = SqlDbType.Bit;
            const SqlDbType expectedField3SqlDbType = SqlDbType.NVarChar;
            var itemList = new List<SimpleParameterTableType>
            {
                new SimpleParameterTableType { Name = "Company 1", IsActive = true, Id = 1 }
            };

            // ACT
            IEnumerable<SqlDataRecord> actualSqlDataRecords = new TableValuedParameterBuilder(itemList).BuildParameters().TableValueParameters;
            SqlDataRecord firstRecord = actualSqlDataRecords.First();
            var firstRecordFirstMetaData = firstRecord.GetSqlMetaData(0);
            var firstRecordSecondMetaData = firstRecord.GetSqlMetaData(1);
            var firstRecordThirdMetaData = firstRecord.GetSqlMetaData(2);

            // ASSERT
            Assert.AreEqual(expectedField1SqlDbType, firstRecordFirstMetaData.SqlDbType);
            Assert.AreEqual(expectedField2SqlDbType, firstRecordSecondMetaData.SqlDbType);
            Assert.AreEqual(expectedField3SqlDbType, firstRecordThirdMetaData.SqlDbType);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void TableValueParameters_WhenConstructedUsingListWithDbTypeAttributeAndIncorrectType_ThrowsException()
        {
            // ARRANGE
            const SqlDbType expectedField1SqlDbType = SqlDbType.BigInt;
            var itemList = new List<SimpleParameterWithDbTypeAttributeTableType>
            {
                new SimpleParameterWithDbTypeAttributeTableType { Id = 1}
            };

            // ACT
            IEnumerable<SqlDataRecord> actualSqlDataRecords = new TableValuedParameterBuilder(itemList)
                .BuildParameters()
                .TableValueParameters;
            SqlDataRecord firstRecord = actualSqlDataRecords.First();
            var firstRecordFirstMetaData = firstRecord.GetSqlMetaData(0);

            // ASSERT
            Assert.AreEqual(expectedField1SqlDbType, firstRecordFirstMetaData.SqlDbType);
        }

        [TestMethod]
        public void TableValueParameters_WhenBuildCalledTwice_AreNotSameInstance()
        {
            // ARRANGE
            var itemList = new List<SimpleParameterTableType>
            {
                new SimpleParameterTableType { Name = "Company 1", IsActive = true, Id = 1 },
                new SimpleParameterTableType { Name = "Company 2", IsActive = false, Id = 2 }
            };

            // ACT
            var builder = new TableValuedParameterBuilder(itemList);
            var actualSqlDataRecords1 = builder
                .BuildParameters()
                .TableValueParameters;

            var actualSqlDataRecords2 = builder
                .BuildParameters()
                .TableValueParameters;

            // ASSERT
            Assert.AreNotSame(actualSqlDataRecords1, actualSqlDataRecords2);
        }

        #endregion

        #region Test Classes

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
        /// Represents a simple table type with name AttributeFound
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
        /// Represents a simple table type with ParameterDbType AttributeFound 
        /// </summary>
        private class SimpleParameterWithDbTypeAttributeTableType
        {
            [DbType(SqlDbType.BigInt)]
            public int Id { get; set; }

            [DbType(SqlDbType.Decimal)]
            public int Count { get; set; }

            [DbType(SqlDbType.NChar)]
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

        #endregion
    }
}