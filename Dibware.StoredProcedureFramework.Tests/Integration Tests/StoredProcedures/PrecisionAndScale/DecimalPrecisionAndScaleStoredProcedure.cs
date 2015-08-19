
namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.PrecisionAndScale
{
    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("DecimalPrecisionAndScale")]
    internal class DecimalPrecisionAndScaleStoredProcedure
        : StoredProcedureBase<DecimalPrecisionAndScaleReturnType, DecimalPrecisionAndScaleParameters>
    {
        public DecimalPrecisionAndScaleStoredProcedure(DecimalPrecisionAndScaleParameters parameters)
            : base(parameters)
        {

        }
    }
}
