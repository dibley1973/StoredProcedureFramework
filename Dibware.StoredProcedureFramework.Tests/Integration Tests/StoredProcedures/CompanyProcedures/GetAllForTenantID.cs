using System;
using System.Data;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.CompanyResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.CompanyProcedures
{
    [StoredProcAttributes.Schema("app")]
    [StoredProcAttributes.Name("Company_GetAllForTenantID")]
    [StoredProcAttributes.ReturnTypes(typeof(CompanyResultRow))]
    internal class GetAllForTenantID
    {
        [StoredProcAttributes.Name("TenantID")]
        [StoredProcAttributes.ParameterType(SqlDbType.UniqueIdentifier)]
        public Guid TenantId { get; set; }
    }
}