using System;
using System.Data;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.CompanyResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.CompanyForTenantProcedures
{
    [StoredProcAttributes.Schema("app")]
    [StoredProcAttributes.Name("CompanyForTenant_GetForTenantID")]
    [StoredProcAttributes.ReturnTypes(typeof(CompanyForTenantResultRow))]
    internal class GetForTenantID
    {
        [StoredProcAttributes.Name("TenantID")]
        [StoredProcAttributes.ParameterType(SqlDbType.UniqueIdentifier)]
        public Guid TenantId { get; set; }
    }
}
