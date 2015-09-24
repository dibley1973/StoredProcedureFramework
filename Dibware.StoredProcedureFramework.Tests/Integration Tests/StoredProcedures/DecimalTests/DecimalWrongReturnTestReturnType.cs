using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.DecimalTests
{
    internal class DecimalWrongReturnTestReturnType
    {
        [Size(255)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string Value1 { get; set; }

        [ParameterDbType(SqlDbType.Bit)]
        public bool Active { get; set; }
    }
}
