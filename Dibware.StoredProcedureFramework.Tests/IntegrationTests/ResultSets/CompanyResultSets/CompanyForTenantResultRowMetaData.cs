using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.ResultSets.CompanyResultSets
{
    /// <summary>
    /// Defines meta data for the CompanyForTenantResultRow
    /// </summary>
    public class CompanyForTenantResultRowMetaData : CompanyResultRowMetaData
    {
        [Name("TenantName")]
        public string TenantName { get; set; }
    }
}
