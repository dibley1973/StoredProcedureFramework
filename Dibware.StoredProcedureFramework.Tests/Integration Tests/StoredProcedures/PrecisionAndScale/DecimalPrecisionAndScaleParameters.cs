
using System.Data;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.PrecisionAndScale
{
    internal class DecimalPrecisionAndScaleParameters
    {
        [Precision(10)]
        [Scale(3)]
        [ParameterDbType(SqlDbType.Decimal)]
        public decimal Value1 { get; set; }

        [Precision(7)]
        [Scale(1)]
        [ParameterDbType(SqlDbType.Decimal)]
        public decimal Value2 { get; set; }
    }
}