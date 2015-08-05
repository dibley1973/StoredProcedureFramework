using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.Fakes.StoredProcedureParameters
{
    internal class ParametersWithoutNameMapping
    {
        [StoredProcAttributes.NameAttribute("Id")]
        [StoredProcAttributes.ParameterTypeAttribute(SqlDbType.VarChar)]
        public string Id { get; set; }

        [StoredProcAttributes.NameAttribute("Name")]
        [StoredProcAttributes.ParameterTypeAttribute(SqlDbType.VarChar)]
        public string Name { get; set; }
    }
}
