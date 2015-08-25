
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.NullValueParameter;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.CountCharsInOutputParameterProcedures
{
    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("CountCharsInOutputParameter")]
    internal class CountCharsInOutputParameterStoredProcedure
        : StoredProcedureBase<NullValueParameterNullableReturnType, CountCharsInOutputParameterParameters>
    {
        public CountCharsInOutputParameterStoredProcedure(CountCharsInOutputParameterParameters parameters)
            : base(parameters)
        {
        }
    }
}