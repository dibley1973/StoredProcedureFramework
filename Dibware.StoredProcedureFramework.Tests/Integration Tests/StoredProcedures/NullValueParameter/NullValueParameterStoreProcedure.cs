
namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.NullValueParameter
{
    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("NullValueParameterAndResult")]
    internal class NullValueParameterStoreProcedure
        : StoredProcedureBase<NullValueParameterNullableReturnType, NullValueParameterParameters>
    {
        public NullValueParameterStoreProcedure(NullValueParameterParameters parameters)
            : base(parameters)
        {

        }
    }

    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("NullValueParameterAndResult")]
    internal class NullValueParameterStoreProcedure2
        : StoredProcedureBase<NullValueParameterNonNullableReturnType, NullValueParameterParameters>
    {
        public NullValueParameterStoreProcedure2(NullValueParameterParameters parameters)
            : base(parameters)
        {

        }
    }
}