using System.Data;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.SizeAttributeProcedures
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