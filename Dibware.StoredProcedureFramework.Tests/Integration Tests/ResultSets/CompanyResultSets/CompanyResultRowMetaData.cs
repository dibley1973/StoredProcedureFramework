using System;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.CompanyResultSets
{
    /// <summary>
    /// Defines meta data for the CompanyResultRow
    /// </summary>
    public class CompanyResultRowMetaData
    {
        [StoredProcAttributes.Name("CompanyID")]
        public Guid CompanyID { get; set; }

        [StoredProcAttributes.Name("TenantID")]
        public Guid TenantID { get; set; }

        [StoredProcAttributes.Name("IsActive")]
        public bool IsActive { get; set; }

        [StoredProcAttributes.Name("CompanyName")]
        public string CompanyName { get; set; }

        [StoredProcAttributes.Name("RecordCreatedDateTime")]
        public DateTime RecordCreatedDateTime { get; set; }
    }
}