using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Examples.StoredProcedures
{
    [Schema("app")]
    internal class CompanyDeleteForTenantId
        : NoReturnTypeStoredProcedureBase<TenantIdParameters>
    {
        public CompanyDeleteForTenantId(TenantIdParameters parameters)
            : base(parameters)
        {
        }
    }
}