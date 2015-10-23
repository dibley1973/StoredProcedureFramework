using System;
using System.Data;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.ResultSets.CompanyResultSets;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.CompanyForTenantProcedures
{
    [Schema("app")]
    [Name("CompanyForTenant_GetForTenantID")]
    [ReturnType(typeof(CompanyForTenantResultRow))]
    internal class GetForTenantID
    {
        [Name("TenantID")]
        [ParameterDbType(SqlDbType.UniqueIdentifier)]
        public Guid TenantId { get; set; }
    }
}
