using System;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.CompanyForTenantProcedures
{
    internal class GetForTenantIDParameters
    {
        [StoredProcAttributes.Name("TenantID")]
        [StoredProcAttributes.ParameterDbType(SqlDbType.UniqueIdentifier)]
        public Guid TenantId { get; set; }
    }
}