using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures
{
    [Schema("app")]
    [Name("Tenant_GetAll")]
    public class TenantGetAllWithAttributes : StoredProcedureBase<TenantResultRow, NullStoredProcedureParameters>
    {
        public TenantGetAllWithAttributes(NullStoredProcedureParameters parameters)
            : base(parameters)
        {

        }
    }
}