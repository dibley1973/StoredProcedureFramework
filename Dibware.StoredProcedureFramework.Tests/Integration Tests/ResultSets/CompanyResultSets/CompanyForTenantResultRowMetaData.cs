namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.CompanyResultSets
{
    /// <summary>
    /// Defines meta data for the CompanyForTenantResultRow
    /// </summary>
    public class CompanyForTenantResultRowMetaData : CompanyResultRowMetaData
    {
        [StoredProcAttributes.Name("TenantName")]
        public string TenantName { get; set; }
    }
}
