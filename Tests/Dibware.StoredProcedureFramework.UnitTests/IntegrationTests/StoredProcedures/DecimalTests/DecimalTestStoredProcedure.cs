using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.DecimalTests
{
    [Schema("app")]
    [Name("DecimalTest")]
    internal class DecimalTestStoredProcedure
        : StoredProcedureBase<List<DecimalTestExtendedReturnType>, NullStoredProcedureParameters>
    {
        public DecimalTestStoredProcedure()
            : base(new NullStoredProcedureParameters())
        {
        }
    }
}
