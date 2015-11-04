using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Examples.StoredProcedures
{
    [Schema("app")]
    public class TenantGetAll
        : NoParametersStoredProcedureBase<List<TenantDto>>
    {
    }
}