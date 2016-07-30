using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class I011_IssueNumberEleven_TestsDynamicColumnsInMUltipleRecordSets
        : BaseSqlConnectionIntegrationTest
    {
        // TODO: Add tests
        [TestMethod]
        public void MultipleRecordSetStoredDynamiccolumProcedure_WithThreeSelects_ReturnsThreeRecordSets()
        {
            // ARRANGE
            var procedure = new MultipleRecordSetDynamicColumnStoredProcedure();

            // ACT
            var resultSet = Connection.ExecuteStoredProcedure(procedure);
            var results1 = resultSet.RecordSet1;
            var results2 = resultSet.RecordSet2;
            var results3 = resultSet.RecordSet3;

            // ASSERT
            Assert.IsNotNull(results1);
            Assert.IsNotNull(results2);
            Assert.IsNotNull(results3);
        }

        [TestMethod]
        public void MultipleRecordSetStoredDynamiccolumProcedure_WithThreeSelects_ReturnsCorrectDataTypes()
        {
            // ARRANGE
            var procedure = new MultipleRecordSetDynamicColumnStoredProcedure();

            // ACT
            var resultSet = Connection.ExecuteStoredProcedure(procedure);

            var results1 = resultSet.RecordSet1;
            var result1 = results1.First() as dynamic;

            var results2 = resultSet.RecordSet2;
            var result2 = results2.First() as dynamic;

            var results3 = resultSet.RecordSet3;
            var result3 = results3.First() as dynamic;

            // ASSERT
            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1.Firstname, typeof(string));
            Assert.IsInstanceOfType(result1.Age, typeof(int));
            Assert.IsInstanceOfType(result1.DateOfBirth, typeof(DateTime));

            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2.Active, typeof(bool));
            Assert.IsInstanceOfType(result2.Price, typeof(decimal));

            Assert.IsNotNull(result3);
            Assert.IsInstanceOfType(result3.UniqueIdentifier, typeof(Guid));
            Assert.IsInstanceOfType(result3.Count, typeof(int));
        }
    }
}