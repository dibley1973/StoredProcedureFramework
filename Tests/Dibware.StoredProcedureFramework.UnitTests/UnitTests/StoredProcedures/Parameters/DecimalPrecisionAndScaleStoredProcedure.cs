using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures.Parameters
{
    internal class DecimalPrecisionAndScaleParameter
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