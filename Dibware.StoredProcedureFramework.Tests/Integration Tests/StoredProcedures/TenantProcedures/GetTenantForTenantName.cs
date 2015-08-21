using Dibware.StoredProcedureFramework.StoredProcAttributes;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.TenantProcedures
{
    [SchemaAttribute("app")]
    [Name("Tenant_GetForTenantName")]
    [ReturnTypeAttribute(typeof(TenantResultRow))]
    internal class GetTenantForTenantName
    {
        [Name("TenantName")]
        [ParameterDbType(SqlDbType.VarChar)]
        public string TenantName { get; set; }
    }

    [SchemaAttribute("app")]
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