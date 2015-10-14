using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Tests.Examples.Tests
{
    [TestClass]
    public class StoredProcedureWithParametersButNoReturnTests
    {
        [TestMethod]
        public void EXAMPLE_ExecuteStoredProcedureWithParametersButNoReturnOnSqlConnection()
        {
            // ARRANGE
            var parameters = new StoredProcedureWithParametersButNoReturnParameters
            {
                Id = 2
            };
            var procedure = new StoredProcedureWithParametersButNoReturn(parameters);
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

            // ACT
            using (DbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ExecuteStoredProcedure(procedure);
            }
        }
    }
}