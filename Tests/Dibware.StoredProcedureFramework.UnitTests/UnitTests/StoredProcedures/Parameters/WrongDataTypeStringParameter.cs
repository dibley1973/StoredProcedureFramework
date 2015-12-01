using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures.Parameters
{
    internal class WrongDataTypeStringParameter
    {
        [ParameterDbType(SqlDbType.NVarChar)]
        public int Value1 { get; set; }

        [ParameterDbType(SqlDbType.NVarChar)]
        public bool Value2 { get; set; }
    }
}
