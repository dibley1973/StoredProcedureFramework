
namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.DecimalTests
{
    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("DecimalTest")]
    internal class DecimalTestStoredProcedure
        : StoredProcedureBase<DecimalTestExtendedReturnType, NullStoredProcedureParameters>
    {
        public DecimalTestStoredProcedure()
            : base(new NullStoredProcedureParameters())
        {
        }
    }
}
