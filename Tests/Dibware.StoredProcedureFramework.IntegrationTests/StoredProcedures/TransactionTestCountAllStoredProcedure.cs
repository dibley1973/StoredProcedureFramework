using Dibware.StoredProcedureFramework.Base;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class TransactionTestCountAllStoredProcedure
        : StoredProcedureBase<List<TransactionTestCountAllStoredProcedure.Return>,
            NullStoredProcedureParameters>
    {

        internal class Return
        {
            public int Count { get; set; }
        }
    }
}
