using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Dibware.StoredProcedureFramework.Tests.Examples.Tests
{
    [TestClass]
    public class StoredProcedureWithoutParametersTests
    {
        [TestMethod]
        public void EXAMPLE_ExecuteStoredProcedureWithoutParametersOnSqlConnection()
        {
            // NOTE:
            // You need a record in the [dbo].[Blah] table with the following values for this test to pass!
            // |--------------------|
            // |    Id  |   Name    |
            // |====================|
            // |    1   |   Sid     |
            // |--------------------|

            // ARRANGE
            var procedure = new StoredProcedureWithoutParameters();
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
            //StoredProcedureWithoutParametersResultSet resultSet;
            List<StoredProcedureWithoutParametersReturnType> resultList;

            // ACT
            using (DbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //resultSet = connection.ExecuteStoredProcedure(procedure);
                resultList = connection.ExecuteStoredProcedure(procedure);
            }
            //var resultList = resultSet.RecordSet;
            //var firstResult = resultList.First();
            var firstResult = resultList.First();

            // ASSERT
            Assert.AreEqual(1, firstResult.Id);
            Assert.AreEqual("Sid", firstResult.Name);
        }
    }
}
