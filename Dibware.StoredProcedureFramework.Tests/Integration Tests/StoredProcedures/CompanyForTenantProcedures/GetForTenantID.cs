using System;
using System.Data;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.CompanyResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.CompanyForTenantProcedures
{
    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("CompanyForTenant_GetForTenantID")]
    [StoredProcAttributes.ReturnTypeAttribute(typeof(CompanyForTenantResultRow))]
    internal class GetForTenantID
    {
        [StoredProcAttributes.Name("TenantID")]
        [StoredProcAttributes.ParameterDbType(SqlDbType.UniqueIdentifier)]
        public Guid TenantId { get; set; }
    }
}
