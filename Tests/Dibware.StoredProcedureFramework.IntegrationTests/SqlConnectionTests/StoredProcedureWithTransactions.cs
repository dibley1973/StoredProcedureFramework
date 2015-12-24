using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.UserDefinedTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace Dibware.StoredProcedureFramework.IntegrationTests.SqlConnectionTests
{
    [TestClass]
    public class StoredProcedureWithTransactions
    {
        [TestMethod]
        public void StoredProcedure_WithTransactionScopeNotCommited_DoesNotWriteRecords()
        {
            // ARRANGE
            const int expectedIntermediateCount = 3;
            int originalCount;
            int intermediateCount;
            int finalCount;
            string connectionName = Properties.Settings.Default.IntegrationTestConnection;
            var itemsToAdd = new List<TransactionTestParameterTableType>
            {
                new TransactionTestParameterTableType { Name = "Company 1", IsActive = true, Id = 1 },
                new TransactionTestParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
                new TransactionTestParameterTableType { Name = "Company 3", IsActive = true, Id = 3 }
            };
            var transactionAddParameters = new TransactionTestAddStoredProcedure.Parameter
            {
                TvpParameters = itemsToAdd
            };
            var transactionTestAddProcedure = new TransactionTestAddStoredProcedure(transactionAddParameters);
            var transactionTestCountProcedure = new TransactionTestCountAllStoredProcedure();

            // ACT
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (var connection = new SqlConnection(connectionName))
                {
                    connection.Open();
                    originalCount = connection.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                    connection.ExecuteStoredProcedure(transactionTestAddProcedure);
                    intermediateCount = connection.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                }
            }
            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                finalCount = connection.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                connection.Close();
            }

            // ASSERT
            Assert.AreEqual(originalCount, finalCount);
            Assert.AreEqual(expectedIntermediateCount, intermediateCount);
        }

        [TestMethod]
        public void StoredProcedure_WithTransactionScopeCompleted_DoesWriteRecords()
        {
            // ARRANGE
            const int expectedIntermediateCount = 3;
            int originalCount;
            int intermediateCount;
            int finalCount;
            string connectionName = Properties.Settings.Default.IntegrationTestConnection;
            var itemsToAdd = new List<TransactionTestParameterTableType>
            {
                new TransactionTestParameterTableType { Name = "Company 1", IsActive = true, Id = 1 },
                new TransactionTestParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
                new TransactionTestParameterTableType { Name = "Company 3", IsActive = true, Id = 3 }
            };
            var transactionAddParameters = new TransactionTestAddStoredProcedure.Parameter
            {
                TvpParameters = itemsToAdd
            };
            var transactionTestAddProcedure = new TransactionTestAddStoredProcedure(transactionAddParameters);
            var transactionTestCountProcedure = new TransactionTestCountAllStoredProcedure();
            var transactionDeleteProcedure = new TransactionTestDeleteAllStoredProcedure();

            // ACT
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (var connection = new SqlConnection(connectionName))
                {
                    connection.Open();
                    originalCount = connection.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                    connection.ExecuteStoredProcedure(transactionTestAddProcedure);
                    transactionScope.Complete();
                }
            }
            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                intermediateCount = connection.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                connection.ExecuteStoredProcedure(transactionDeleteProcedure);
                finalCount = connection.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                connection.Close();
            }

            // ASSERT
            Assert.AreEqual(originalCount, finalCount);
            Assert.AreEqual(expectedIntermediateCount, intermediateCount);
        }

        [TestMethod]
        public void StoredProcedure_WithSqlTransactionRolledBack_DoesNotWriteRecords()
        {
            // ARRANGE
            const int expectedIntermediateCount = 3;
            int originalCount;
            int intermediateCount;
            int finalCount;
            string connectionName = Properties.Settings.Default.IntegrationTestConnection;
            var itemsToAdd = new List<TransactionTestParameterTableType>
            {
                new TransactionTestParameterTableType { Name = "Company 1", IsActive = true, Id = 1 },
                new TransactionTestParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
                new TransactionTestParameterTableType { Name = "Company 3", IsActive = true, Id = 3 }
            };
            var transactionAddParameters = new TransactionTestAddStoredProcedure.Parameter
            {
                TvpParameters = itemsToAdd
            };
            var transactionTestAddProcedure = new TransactionTestAddStoredProcedure(transactionAddParameters);
            var transactionTestCountProcedure = new TransactionTestCountAllStoredProcedure();

            // ACT
            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                SqlTransaction transaction;
                using (transaction = connection.BeginTransaction())
                {
                    originalCount = connection.ExecuteStoredProcedure(transactionTestCountProcedure, transaction: transaction).First().Count;
                    connection.ExecuteStoredProcedure(transactionTestAddProcedure, transaction: transaction);
                    intermediateCount = connection.ExecuteStoredProcedure(transactionTestCountProcedure, transaction: transaction).First().Count;
                    transaction.Rollback();
                }
            }

            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                finalCount = connection.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                connection.Close();
            }

            // ASSERT
            Assert.AreEqual(originalCount, finalCount);
            Assert.AreEqual(expectedIntermediateCount, intermediateCount);
        }

        [TestMethod]
        public void StoredProcedure_WithSqlTransactionCommitted_DoesWriteRecords()
        {
            // ARRANGE
            const int expectedIntermediateCount = 3;
            int originalCount;
            int intermediateCount;
            int finalCount;
            string connectionName = Properties.Settings.Default.IntegrationTestConnection;
            var itemsToAdd = new List<TransactionTestParameterTableType>
            {
                new TransactionTestParameterTableType { Name = "Company 1", IsActive = true, Id = 1 },
                new TransactionTestParameterTableType { Name = "Company 2", IsActive = false, Id = 2 },
                new TransactionTestParameterTableType { Name = "Company 3", IsActive = true, Id = 3 }
            };
            var transactionAddParameters = new TransactionTestAddStoredProcedure.Parameter
            {
                TvpParameters = itemsToAdd
            };
            var transactionTestAddProcedure = new TransactionTestAddStoredProcedure(transactionAddParameters);
            var transactionTestCountProcedure = new TransactionTestCountAllStoredProcedure();
            var transactionDeleteProcedure = new TransactionTestDeleteAllStoredProcedure();

            // ACT

            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                SqlTransaction transaction;
                using (transaction = connection.BeginTransaction())
                {
                    originalCount = connection.ExecuteStoredProcedure(transactionTestCountProcedure, transaction: transaction).First().Count;
                    connection.ExecuteStoredProcedure(transactionTestAddProcedure, transaction: transaction);
                    transaction.Commit();
                }
            }

            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                intermediateCount = connection.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                connection.ExecuteStoredProcedure(transactionDeleteProcedure);
                finalCount = connection.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                connection.Close();
            }

            // ASSERT
            Assert.AreEqual(originalCount, finalCount);
            Assert.AreEqual(expectedIntermediateCount, intermediateCount);
        }
    }
}