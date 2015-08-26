using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.SizeAttributeProcedures
{

    internal class CorrectSizeAttributeReturnType
    {
        [Size(255)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string Value1 { get; set; }
    }

    internal class TooSmallSizeAttributeReturnType
    {
        [Size(255)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string Value1 { get; set; }
    }

    internal class TooLargeSizeAttributeReturnType
    {
        [Size(255)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string Value1 { get; set; }
    }
}