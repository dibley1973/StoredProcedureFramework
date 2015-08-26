using System.Data;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Fakes.StoredProcedureParameters
{
    internal class ParametersWithoutNameMapping
    {
        [Name("Id")]
        [ParameterDbType(SqlDbType.VarChar)]
        public string Id { get; set; }

        [Name("Name")]
        [ParameterDbType(SqlDbType.VarChar)]
        public string Name { get; set; }
    }
}
