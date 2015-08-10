using System.Data;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures
{
    [StoredProcAttributes.Schema("app")]
    [StoredProcAttributes.Name("Tenant_GetAll")]
    [StoredProcAttributes.ReturnTypes(typeof(TenantResultRow))]
    internal class GetTenantForAll
    {
        [StoredProcAttributes.Name("TenantName")]
        [StoredProcAttributes.ParameterType(SqlDbType.VarChar)]
        public string TenantName { get; set; }
    }
}
