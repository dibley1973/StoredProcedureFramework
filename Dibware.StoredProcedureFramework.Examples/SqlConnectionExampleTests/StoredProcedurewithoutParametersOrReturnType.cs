using Dibware.StoredProcedureFramework.Examples.Properties;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using System.Data.SqlClient;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests
{
    [TestClass]
    public class StoredProcedureWithoutParametersOrReturnType
    {
        #region Fields

        private string _connectionString;

        #endregion

        #region TestInitialize

        [TestInitialize]
        public void TestInitialize()
        {
            _connectionString = Settings.Default.ExampleDatabaseConnection;
        }

        #endregion

        [TestMethod]
        public void AccountLastUpdatedDateTimeReset()
        {
            // ARRANGE
            var procedure = new AccountLastUpdatedDateTimeReset();

            // ACT
            using (DbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.ExecuteStoredProcedure(procedure);
                connection.Close();
            }

            // ASSERT
            // Nothing to assert
        }
    }
}
