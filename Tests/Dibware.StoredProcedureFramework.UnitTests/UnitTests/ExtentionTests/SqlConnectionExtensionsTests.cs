using System;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures;


namespace Dibware.StoredProcedureFramework.Tests.UnitTests.ExtentionTests
{
    [TestClass]
    public class SqlConnectionExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteStoredProcedure_WithNullStoredProcedure_ThrowsArgumentNullException()
        {
            // ARRANGE
            MostBasicStoredProcedure procedure = null;
            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

            // ACT
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ExecuteStoredProcedure(procedure);
            }

            // ASSERT
            // Exception should have been thrown
        }
    }
}
