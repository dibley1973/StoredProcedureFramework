using System;
using System.Data;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.CompanyResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.CompanyProcedures
{
    [Schema("app")]
    [Name("Company_GetAllForTenantID")]
    [ReturnType(typeof(CompanyResultRow))]
    internal class GetAllForTenantID
    {
        [Name("TenantID")]
        [ParameterDbType(SqlDbType.UniqueIdentifier)]
        public Guid TenantId { get; set; }
    }
}