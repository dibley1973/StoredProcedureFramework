using Dibware.StoredProcedureFramework.Base;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    //public class DynamicColumnStoredProcedure2
    //    : NoParametersStoredProcedureBase<List<dynamic>>
    //{
    //}
    public class DynamicColumnStoredProcedure2
        : StoredProcedureBase<List<dynamic>, NullStoredProcedureParameters>
    {
    }
}