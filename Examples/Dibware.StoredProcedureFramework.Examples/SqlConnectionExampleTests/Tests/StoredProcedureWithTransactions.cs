using Dibware.StoredProcedureFramework.Examples.StoredProcedures;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFramework.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;

namespace Dibware.StoredProcedureFramework.Examples.SqlConnectionExampleTests.Tests
{
    [TestClass]
    public class StoredProcedureWithTransactions
    {
        [TestMethod]
        public void StoredProcedure_WithTransactionScopeNotCommited_DoesNotWriteRecords()
        {
            // ARRANGE
            const int expectedIntermediateCount = 5;
            int originalCount;
            int intermediateCount;
            int finalCount;
            string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
            var companiesToAdd = new List<CompaniesAdd.CompanyTableType>
            {
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 1", IsActive = true, TenantId = 2 },
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 2", IsActive = false, TenantId = 2 },
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 3", IsActive = true, TenantId = 2 }
            };
            var parameters = new CompaniesAdd.CompaniesAddParameters
            {
                Companies = companiesToAdd
            };
            var companyAddProcedure = new CompaniesAdd(parameters);
            var companyCountProcedure = new CompanyCountAll();

            // ACT
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (var connection = new SqlConnection(connectionName))
                {
                    connection.Open();
                    originalCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
                    connection.ExecuteStoredProcedure(companyAddProcedure);
                    intermediateCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
                }
            }
            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                finalCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
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
            const int expectedIntermediateCount = 5;
            int originalCount;
            int intermediateCount;
            int finalCount;
            string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
            var companiesToAdd = new List<CompaniesAdd.CompanyTableType>
            {
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 1", IsActive = true, TenantId = 2 },
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 2", IsActive = false, TenantId = 2 },
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 3", IsActive = true, TenantId = 2 }
            };
            var companiesAddParameters = new CompaniesAdd.CompaniesAddParameters
            {
                Companies = companiesToAdd
            };
            var companyAddProcedure = new CompaniesAdd(companiesAddParameters);
            var companyCountProcedure = new CompanyCountAll();
            var companyDeleteParameters = new TenantIdParameters
            {
                TenantId = 2
            };
            var companyDeleteProcedure = new CompanyDeleteForTenantId(companyDeleteParameters);

            // ACT
            using (var transactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                using (var connection = new SqlConnection(connectionName))
                {
                    connection.Open();
                    originalCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
                    connection.ExecuteStoredProcedure(companyAddProcedure);
                    transactionScope.Complete();
                }
            }
            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                intermediateCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
                connection.ExecuteStoredProcedure(companyDeleteProcedure);
                finalCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
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
            const int expectedIntermediateCount = 5;
            int originalCount;
            int intermediateCount;
            int finalCount;
            string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
            var companiesToAdd = new List<CompaniesAdd.CompanyTableType>
            {
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 1", IsActive = true, TenantId = 2 },
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 2", IsActive = false, TenantId = 2 },
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 3", IsActive = true, TenantId = 2 }
            };
            var parameters = new CompaniesAdd.CompaniesAddParameters
            {
                Companies = companiesToAdd
            };
            var companyAddProcedure = new CompaniesAdd(parameters);
            var companyCountProcedure = new CompanyCountAll();

            // ACT
            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                SqlTransaction transaction;
                using (transaction = connection.BeginTransaction())
                {
                    originalCount = connection.ExecuteStoredProcedure(companyCountProcedure, transaction: transaction).First().CountOfCompanies;
                    connection.ExecuteStoredProcedure(companyAddProcedure, transaction: transaction);
                    intermediateCount = connection.ExecuteStoredProcedure(companyCountProcedure, transaction: transaction).First().CountOfCompanies;
                    transaction.Rollback();
                }
            }

            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                finalCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
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
            const int expectedIntermediateCount = 5;
            int originalCount;
            int intermediateCount;
            int finalCount;
            string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
            var companiesToAdd = new List<CompaniesAdd.CompanyTableType>
            {
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 1", IsActive = true, TenantId = 2 },
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 2", IsActive = false, TenantId = 2 },
                new CompaniesAdd.CompanyTableType { CompanyName = "Company 3", IsActive = true, TenantId = 2 }
            };
            var companiesAddParameters = new CompaniesAdd.CompaniesAddParameters
            {
                Companies = companiesToAdd
            };
            var companyAddProcedure = new CompaniesAdd(companiesAddParameters);
            var companyCountProcedure = new CompanyCountAll();
            var companyDeleteParameters = new TenantIdParameters
            {
                TenantId = 2
            };
            var companyDeleteProcedure = new CompanyDeleteForTenantId(companyDeleteParameters);

            // ACT

            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                SqlTransaction transaction;
                using (transaction = connection.BeginTransaction())
                {
                    originalCount = connection.ExecuteStoredProcedure(companyCountProcedure, transaction: transaction).First().CountOfCompanies;
                    connection.ExecuteStoredProcedure(companyAddProcedure, transaction: transaction);
                    transaction.Commit();
                }
            }

            using (var connection = new SqlConnection(connectionName))
            {
                connection.Open();
                intermediateCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
                connection.ExecuteStoredProcedure(companyDeleteProcedure);
                finalCount = connection.ExecuteStoredProcedure(companyCountProcedure).First().CountOfCompanies;
                connection.Close();
            }

            // ASSERT
            Assert.AreEqual(originalCount, finalCount);
            Assert.AreEqual(expectedIntermediateCount, intermediateCount);
        }
    }
}