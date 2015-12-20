using System;
using System.Data;
using System.Reflection;
using Dibware.StoredProcedureFramework.Helpers;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Helpers
{
    [TestClass]
    public class ColumnSqlDbTypeHelperTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenConstructedWithNullProperty_ThrowsException()
        {
            // ARRANGE

            // ACT
            new ColumnSqlDbTypeHelper(null);

            // ASSERT
        }

        [TestMethod]
        public void Constructor_WhenGivenValidValue_DoesNotThrowException()
        {
            // ARRANGE
            Type testType = typeof(TestObject);

            PropertyInfo property = testType.GetProperty("ParameterDbTypeAttribute1");

            // ACT
            new ColumnSqlDbTypeHelper(property);

            // ASSERT
        }

        [TestMethod]
        public void ParameterDbTypeAttribute_ForPropertyWithoutAttribute_ReturnsColumnParameterDbTypeAttribute()
        {
            // ARRANGE
            const SqlDbType expectedColumnParameterDbTypeAttribute = SqlDbType.NVarChar;
            Type testType = typeof(TestObject);

            PropertyInfo property = testType.GetProperty("ParameterDbTypeAttribute1");

            // ACT
            var name = new ColumnSqlDbTypeHelper(property).Build().SqlDbType;

            // ASSERT
            Assert.AreEqual(expectedColumnParameterDbTypeAttribute, name);
        }

        [TestMethod]
        public void ParameterDbTypeAttribute_ForPropertyWithAttribute_ReturnsAttributeValue()
        {
            // ARRANGE
            const SqlDbType expectedColumnParameterDbTypeAttribute = SqlDbType.Decimal;
            Type testType = typeof(TestObject);

            PropertyInfo property = testType.GetProperty("ParameterDbTypeAttribute2");

            // ACT
            var name = new ColumnSqlDbTypeHelper(property).Build().SqlDbType;

            // ASSERT
            Assert.AreEqual(expectedColumnParameterDbTypeAttribute, name);
        }

        public class TestObject
        {
            public string ParameterDbTypeAttribute1 { get; set; }
            [ParameterDbType(SqlDbType.Decimal)]
            public string ParameterDbTypeAttribute2 { get; set; }
        }

    }
}