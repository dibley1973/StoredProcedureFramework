using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Extensions;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Dibware.StoredProcedureFramework.PackageLoadTest
{
    public class PackageReferenceTest
    {
        public void Test1()
        {
            string connectionName = Properties.Settings.Default.ExampleDatabaseConnection;
            var parameters = new TenantIdParameters
            {
                TenantId = 1
            };
            var procedure = new CompanyGetAllForTenantID(parameters);
            const int expectedCompanyCount = 2;
            CompanyDto company1;

            // ACT
            using (SqlConnection connection = new SqlConnection(connectionName))
            {
                var companies = connection.ExecuteStoredProcedure(procedure);
                company1 = companies.FirstOrDefault();
            }
        }
    }

    public class TenantIdParameters
    {
        public int TenantId { get; set; }
    }

    /// <summary>
    /// Encapsulates company data
    /// </summary>
    internal class CompanyDto
    {
        public int CompanyID { get; set; }
        public int TenantID { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; }
        public DateTime RecordCreatedDateTime { get; set; }
    }

    [Schema("app")]
    internal class CompanyGetAllForTenantID
        : StoredProcedureBase<List<CompanyDto>, TenantIdParameters>
    {
        public CompanyGetAllForTenantID(TenantIdParameters parameters)
            : base(parameters)
        {
        }
    }
}