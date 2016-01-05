using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class TransactionTestCountAllStoredProcedure
        : NoParametersStoredProcedureBase<List<TransactionTestCountAllStoredProcedure.Return>>
    {

        internal class Return
        {
            public int Count { get; set; }
        }
    }
}
