using Dibware.StoredProcedureFramework.StoredProcAttributes;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.NullValueParameter
{
    internal class NullValueParameterParameters
    {
        [ParameterDbType(SqlDbType.Int)]
        public int? Value1 { get; set; }

        [ParameterDbType(SqlDbType.Int)]
        public int? Value2 { get; set; }
    }
}