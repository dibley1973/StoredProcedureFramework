using System;
using System.Data;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.CompanyResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.CompanyProcedures
{
    [StoredProcAttributes.SchemaAttribute("app")]
    [StoredProcAttributes.Name("Company_GetAllForTenantID")]
    [StoredProcAttributes.ReturnTypeAttribute(typeof(CompanyResultRow))]
    internal class GetAllForTenantID
    {
        [StoredProcAttributes.Name("TenantID")]
        [StoredProcAttributes.ParameterDbType(SqlDbType.UniqueIdentifier)]
        public Guid TenantId { get; set; }
    }
}