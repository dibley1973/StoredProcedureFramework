using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets;
using System.Data;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures
{
    [Schema("app")]
    [Name("Tenant_GetForTenantName")]
    [ReturnType(typeof(TenantResultRow))]
    internal class GetTenantForTenantName
    {
        [Name("TenantName")]
        [ParameterDbType(SqlDbType.VarChar)]
        public string TenantName { get; set; }
    }

    [Schema("app")]
    [Name("Tenant_GetForTenantName")]
    internal class GetTenantForTenantNameProcedure
        : StoredProcedureBase<TenantResultRow, GetTenantForTenantNameParameters>
    {
        public GetTenantForTenantNameProcedure(
            GetTenantForTenantNameParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class GetTenantForTenantNameParameters
    {
        [Name("TenantName")]
        [Size(100)]
        [ParameterDbType(SqlDbType.VarChar)]
        public string TenantName { get; set; }
    }
}