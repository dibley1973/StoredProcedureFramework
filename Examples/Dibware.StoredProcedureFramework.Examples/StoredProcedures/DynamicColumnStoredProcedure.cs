using Dibware.StoredProcedureFramework.Base;
using System.Collections.Generic;
using System.Dynamic;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Examples.StoredProcedures
{
    [Schema("app")]
    internal class GetDynamicColumnStoredProcedure
        : NoParametersStoredProcedureBase<List<ExpandoObject>>
    {
    }
}