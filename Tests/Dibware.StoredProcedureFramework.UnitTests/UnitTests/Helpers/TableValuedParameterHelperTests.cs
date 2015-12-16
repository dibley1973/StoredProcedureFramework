using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dibware.StoredProcedureFramework.Helpers;
using Microsoft.SqlServer.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            const int expectedFieldount = 3;
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
            Assert.AreEqual(expectedFieldount, actualSqlDataRecords.First().FieldCount);
            Assert.AreEqual(expectedFieldount, actualSqlDataRecords.Last().FieldCount);
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
        [ExpectedException(typeof(NullReferenceException))]
        public void GetTableValuedParameterFromList_FromEmptyList_ReturnsDefault()
        {
            // ARRANGE
            var itemList = new List<SimpleParameterTableType>();

            // ACT
            var actualSqlDataRecords = TableValuedParameterHelper.GetTableValuedParameterFromList(itemList);

            // ASSERT
            Assert.IsNotNull(actualSqlDataRecords);
            Assert.AreEqual(0, actualSqlDataRecords.Count());
        }

        /// <summary>
        /// Represenst a simple table type
        /// </summary>
        private class SimpleParameterTableType
        {
            public int Id { get; set; }
            public bool IsActive { get; set; }
            public string Name { get; set; }
        }
    }
}
