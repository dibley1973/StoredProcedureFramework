//using Dibware.StoredProcedureFramework.Extensions;
//using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Configuration;
//using System.Data.Common;
//using System.Data.SqlClient;
//using Dibware.StoredProcedureFramework.Tests.IntegrationTests.Base;
//using Dibware.StoredProcedureFrameworkForEF;
//using Dibware.StoredProcedureFrameworkForEF.Extensions;

//namespace Dibware.StoredProcedureFramework.Tests.Examples.Tests
//{
//    [TestClass]
//    public class MostBasicStoredProcedureTestsForSqlConnection
//    {
//        [TestMethod]
//        public void EXAMPLE_ExecuteMostBasicStoredProcedureOnSqlConnection()
//        {
//            // ARRANGE
//            var procedure = new MostBasicStoredProcedure();
//            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

//            // ACT
//            using (DbConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                connection.ExecuteStoredProcedure(procedure);
//            }

//            // ASSERT
//            // Should get here as Exception should not have been thrown
//        }
//    }

//    [TestClass]
//    public class MostBasicStoredProcedureTestsWithDbContextForDbContext : BaseIntegrationTestWithDbContext
//    {
//        [TestMethod]
//        public void EXAMPLE_ExecuteMostBasicStoredProcedureOnDbConext()
//        {
//            // ARRANGE
//            var procedure = new MostBasicStoredProcedure();
//            var connectionString = ConfigurationManager.ConnectionStrings["IntegrationTestConnection"].ConnectionString;

//            // ACT
//            Context.ExecuteStoredProcedure(procedure);
            
//            // ASSERT
//            // Should get here as Exception should not have been thrown
//        }
//    }
//}