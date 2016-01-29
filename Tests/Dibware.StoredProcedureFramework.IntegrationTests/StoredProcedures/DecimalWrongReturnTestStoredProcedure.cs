using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class DecimalWrongReturnTestStoredProcedure
        : NoParametersStoredProcedureBase<List<DecimalWrongReturnTestStoredProcedure.Return>>
    {
        internal class Return
        {
            [Size(255)]
            public string Value1 { get; set; }

            public bool Active { get; set; }
        }
    }
}