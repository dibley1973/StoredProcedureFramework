
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.DecimalTests
{
    [Schema("app")]
    [Name("DecimalTest")]
    internal class DecimalTestStoredProcedure
        : StoredProcedureBase<DecimalTestExtendedReturnType, NullStoredProcedureParameters>
    {
        public DecimalTestStoredProcedure()
            : base(new NullStoredProcedureParameters())
        {
        }
    }
}
