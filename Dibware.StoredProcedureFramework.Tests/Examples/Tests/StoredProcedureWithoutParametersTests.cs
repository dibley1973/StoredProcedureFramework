using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
        public void EXAMPLE_ExecuteMostBasicStoredProcedureWithoutParametersOnSqlConnection()
        {
            // ARRANGE
            var procedure = new StoredProcedureWithoutParameters();
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;
            StoredProcedureWithoutParametersResultSet resultSet;
            List<StoredProcedureWithoutParametersReturnType> resultList;
            StoredProcedureWithoutParametersReturnType firstResult;

            // ACT
            using (DbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                resultSet = connection.ExecuteStoredProcedure(procedure);
            }
            resultList = resultSet.RecordSet;
            firstResult = resultList.First();

            // ASSERT
            Assert.AreEqual(1, firstResult.Id);
            Assert.AreEqual("Sid", firstResult.Name);
        }
    }
}
