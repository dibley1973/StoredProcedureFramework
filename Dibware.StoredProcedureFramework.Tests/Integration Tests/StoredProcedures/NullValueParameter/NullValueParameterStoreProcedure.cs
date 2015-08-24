
namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.NullValueParameter
{
    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("NullValueParameterAndResult")]
    internal class NullValueParameterStoreProcedure
        : StoredProcedureBase<NullValueParameterReturnType, NullValueParameterParameters>
    {
        public NullValueParameterStoreProcedure(NullValueParameterParameters parameters)
            : base(parameters)
        {

        }
    }
}