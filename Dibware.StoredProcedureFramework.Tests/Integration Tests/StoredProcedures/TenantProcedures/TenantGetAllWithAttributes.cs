using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures
{
    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("Tenant_GetAll")]
    public class TenantGetAllWithAttributes : StoredProcedureBase<TenantResultRow, NullStoredProcedureParameters>
    {
        public TenantGetAllWithAttributes(NullStoredProcedureParameters parameters)
            : base(parameters)
        {

        }
    }
}