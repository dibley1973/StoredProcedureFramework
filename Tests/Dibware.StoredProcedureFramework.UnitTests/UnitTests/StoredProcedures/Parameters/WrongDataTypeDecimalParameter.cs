using System.Data;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures.Parameters
{
    internal class WrongDataTypeDecimalParameter
    {
        [Precision(10)]
        [Scale(3)]
        [DbType(SqlDbType.Decimal)]
        public int Value1 { get; set; }

        [Precision(7)]
        [Scale(1)]
        [DbType(SqlDbType.Decimal)]
        public string Value2 { get; set; }
    }
}