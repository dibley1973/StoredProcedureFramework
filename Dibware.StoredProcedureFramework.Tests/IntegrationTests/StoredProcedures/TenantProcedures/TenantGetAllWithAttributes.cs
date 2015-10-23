using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFramework.Tests.IntegrationTests.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.TenantProcedures
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