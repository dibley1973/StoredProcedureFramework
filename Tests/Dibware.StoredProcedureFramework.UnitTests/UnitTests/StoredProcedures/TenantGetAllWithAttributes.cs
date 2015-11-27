using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures.ResultSets.TenantResultSets;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures
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