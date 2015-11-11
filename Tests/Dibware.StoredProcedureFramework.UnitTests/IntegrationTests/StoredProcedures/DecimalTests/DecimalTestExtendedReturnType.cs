using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.DecimalTests
{
    internal class DecimalTestExtendedReturnType
    {
        [Precision(9)]
        [Scale(3)]
        public decimal Value1 { get; set; }

        [Precision(15)]
        [Scale(2)]
        public decimal Value2 { get; set; }

        [Precision(5)]
        [Scale(2)]
        public decimal Value3 { get; set; }

        [Precision(18)]
        [Scale(6)]
        public decimal Value4 { get; set; }
    }
}