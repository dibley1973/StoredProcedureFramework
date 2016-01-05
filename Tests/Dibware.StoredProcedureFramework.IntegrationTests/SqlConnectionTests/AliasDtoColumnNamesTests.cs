using System;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.TestBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class AliasDtoColumnNamesTests
         : BaseSqlConnectionIntegrationTest
    {
        [TestMethod]
        public void DTO_WithoutAliasButWithMatchingNames_ReturnsResults()
        {
            // ARRANGE
            var procedure = new DtoColumnNoAliasButMatchedNamesTestProcedure();

            // ACT
            var result = Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingFieldException))]
        public void DTO_WithAliasAndWithMatchingNames_ThrowsException()
        {
            // ARRANGE
            var procedure = new DtoColumnWithAliasAndWithMatchedNamesTestProcedure();

            // ACT
            var result = Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingFieldException))]
        public void DTO_WithoutAliasAndWithoutMatchingNames_ThrowsException()
        {
            // ARRANGE
            var procedure = new DtoColumnWithoutAliasAndWithoutMatchingNamesTestProcedure();

            // ACT
            var result = Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DTO_WithAliasAndWithNamesMatchingAlias_ReturnsResults()
        {
            // ARRANGE
            var procedure = new DtoColumnWithAliasAndWithNamesMatchingTestProcedure();

            // ACT
            var result = Connection.ExecuteStoredProcedure(procedure);

            // ASSERT
            Assert.IsNotNull(result);
        }
    }
}
