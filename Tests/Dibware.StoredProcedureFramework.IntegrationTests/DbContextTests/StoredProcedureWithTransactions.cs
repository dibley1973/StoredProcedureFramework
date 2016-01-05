
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests.Context;
using Dibware.StoredProcedureFramework.IntegrationTests.Properties;
using Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures;
using Dibware.StoredProcedureFramework.IntegrationTests.UserDefinedTypes;
using Dibware.StoredProcedureFrameworkForEF.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dibware.StoredProcedureFramework.IntegrationTests.DbContextTests
{
    [TestClass]
    public class StoredProcedureWithTransactions
    {
        private string _connectionName;

        #region Test Pre and Clear down

        [TestInitialize]
        public void TestSetup()
        {
            _connectionName = Settings.Default.IntegrationTestConnection;            
        }

        #endregion


        [TestMethod]
        public void StoredProcedure_WithTransactionScopeNotCommited_DoesNotWriteRecords()
        {
            // ARRANGE
            const int expectedIntermediateCount = 3;
            int originalCount;
            int intermediateCount;
            int finalCount;
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
            using (new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (var context = new IntegrationTestDbContext(_connectionName))
                {
                    originalCount = context.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                    context.ExecuteStoredProcedure(transactionTestAddProcedure);
                    intermediateCount = context.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                }
            }
            using (var context = new IntegrationTestDbContext(_connectionName))
            {
                finalCount = context.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
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
                using (var context = new IntegrationTestDbContext(_connectionName))
                {
                    originalCount = context.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                    context.ExecuteStoredProcedure(transactionTestAddProcedure);
                    transactionScope.Complete();
                }
            }
            using (var context = new IntegrationTestDbContext(_connectionName))
            {
                
                intermediateCount = context.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                context.ExecuteStoredProcedure(transactionDeleteProcedure);
                finalCount = context.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                
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
            using (var connection = new SqlConnection(_connectionName))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (var context = new IntegrationTestDbContext(connection, false))
                    {
                        originalCount = context.ExecuteStoredProcedure(transactionTestCountProcedure, transaction: transaction).First().Count;
                        context.ExecuteStoredProcedure(transactionTestAddProcedure, transaction: transaction);
                        intermediateCount = context.ExecuteStoredProcedure(transactionTestCountProcedure, transaction: transaction).First().Count;
                        transaction.Rollback();
                    }
                }
            }
            using (var context = new IntegrationTestDbContext(_connectionName))
            {
                finalCount = context.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
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
            using (var connection = new SqlConnection(_connectionName))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (var context = new IntegrationTestDbContext(connection, false))
                    {
                        originalCount = context.ExecuteStoredProcedure(transactionTestCountProcedure, transaction: transaction).First().Count;
                        context.ExecuteStoredProcedure(transactionTestAddProcedure, transaction: transaction);
                        transaction.Commit();
                    }
                }
            }

            using (var context = new IntegrationTestDbContext(_connectionName))
            {
                intermediateCount = context.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
                context.ExecuteStoredProcedure(transactionDeleteProcedure);
                finalCount = context.ExecuteStoredProcedure(transactionTestCountProcedure).First().Count;
            }

            // ASSERT
            Assert.AreEqual(originalCount, finalCount);
            Assert.AreEqual(expectedIntermediateCount, intermediateCount);
        }
    }
}
