using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.SizeAttributeProcedures
{
    internal class CorrectSizeAttributeParameters
    {
        [Size(20)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string Value1 { get; set; }
    }

    internal class TooSmallSizeAttributeParameters
    {
        [Size(10)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string Value1 { get; set; }
    }

    internal class TooLargeSizeAttributeParameters
    {
        [Size(30)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string Value1 { get; set; }
    }
}
