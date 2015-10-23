
namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.NullValueParameter
{
    internal class NullValueParameterNullableReturnType
    {
        public int? Value1 { get; set; }

        public int? Value2 { get; set; }
    }

    internal class NullValueParameterNonNullableReturnType
    {
        public int Value1 { get; set; }

        public int Value2 { get; set; }
    }
}