using Dibware.StoredProcedureFramework.Base;
using System.Collections.Generic;
using System.Dynamic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    public class DynamicColumnStoredProcedure
        : NoParametersStoredProcedureBase<List<ExpandoObject>>
    {
    }
}