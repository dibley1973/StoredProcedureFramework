
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.DecimalTests
{
    [Schema("app")]
    [Name("DecimalTest")]
    internal class DecimalWrongReturnTestStoredProcedure
        : StoredProcedureBase<DecimalWrongReturnTestReturnType, NullStoredProcedureParameters>
    {
        public DecimalWrongReturnTestStoredProcedure()
            : base(new NullStoredProcedureParameters())
        {
        }
    }
}
