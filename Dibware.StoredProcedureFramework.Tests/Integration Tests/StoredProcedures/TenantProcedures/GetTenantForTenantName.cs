using System.Data;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures
{
    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("Tenant_GetForTenantName")]
    [StoredProcAttributes.ReturnTypeAttribute(typeof(TenantResultRow))]
    internal class GetTenantForTenantName
    {
        [StoredProcAttributes.Name("TenantName")]
        [StoredProcAttributes.ParameterType(SqlDbType.VarChar)]
        public string TenantName { get; set; }
    }
}
