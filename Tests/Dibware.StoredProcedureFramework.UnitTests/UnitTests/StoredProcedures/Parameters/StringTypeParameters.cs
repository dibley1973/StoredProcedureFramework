using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures.Parameters
{
    internal class StringTypeParameters
    {
        [Size(30)]
        [DbType(SqlDbType.NVarChar)]
        public string Value1 { get; set; }

        [Size(50)]
        [DbType(SqlDbType.NVarChar)]
        public string Value2 { get; set; }
    }
}