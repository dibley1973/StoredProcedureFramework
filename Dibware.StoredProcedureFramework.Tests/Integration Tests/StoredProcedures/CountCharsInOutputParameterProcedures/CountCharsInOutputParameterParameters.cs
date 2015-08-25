using System.Data;
using Dibware.StoredProcedureFramework.StoredProcAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.CountCharsInOutputParameterProcedures
{
    internal class CountCharsInOutputParameterParameters
    {
        [Size(100)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string Value1 { get; set; }

        [ParameterDbType(SqlDbType.Int)]
        [Direction(ParameterDirection.Output)]
        public int Value2 { get; set; }
    }
}