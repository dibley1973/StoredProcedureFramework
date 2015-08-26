
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.NullValueParameter;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.CountCharsInOutputParameterProcedures
{
    [Schema("app")]
    [Name("CountCharsInOutputParameter")]
    internal class CountCharsInOutputParameterStoredProcedure
        : StoredProcedureBase<NullValueParameterNullableReturnType, CountCharsInOutputParameterParameters>
    {
        public CountCharsInOutputParameterStoredProcedure(CountCharsInOutputParameterParameters parameters)
            : base(parameters)
        {
        }
    }
}