using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures.Parameters
{
    internal class NullableIntegerParameters
    {
        [DbType(SqlDbType.Int)]
        public int? Value1 { get; set; }

        [DbType(SqlDbType.Int)]
        public int? Value2 { get; set; }
    }
}