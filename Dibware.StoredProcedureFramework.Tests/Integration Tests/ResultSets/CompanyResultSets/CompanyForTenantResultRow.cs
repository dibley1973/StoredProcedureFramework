using System.ComponentModel.DataAnnotations;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.CompanyResultSets
{
    [MetadataType(typeof(CompanyForTenantResultRowMetaData))]
    public class CompanyForTenantResultRow : CompanyResultRow
    {
        public string TenantName { get; set; }
    }
}