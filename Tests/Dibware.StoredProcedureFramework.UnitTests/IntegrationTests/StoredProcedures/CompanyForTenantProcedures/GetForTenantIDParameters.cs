using System;
using System.Data;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.CompanyForTenantProcedures
{
    internal class GetForTenantIDParameters
    {
        [Name("TenantID")]
        [ParameterDbType(SqlDbType.UniqueIdentifier)]
        public Guid TenantId { get; set; }
    }
}