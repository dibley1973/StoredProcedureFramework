using Dibware.StoredProcedureFramework.Base;
using System.Collections.Generic;

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
