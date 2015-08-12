using System.Data;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures
{
    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("Tenant_GetAll")]
    [StoredProcAttributes.ReturnTypeAttribute(typeof(TenantResultRow))]
    internal class GetTenantForAll
    {

    }
}
