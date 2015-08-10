using System;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets
{
    /// <summary>
    /// Defines meta data for the TenantResultRow
    /// </summary>
    public class TenantResultRowMetaData
    {
        [StoredProcAttributes.Name("TenantID")]
        public Guid TenantID { get; set; }

        [StoredProcAttributes.Name("IsActive")]
        public bool IsActive { get; set; }

        [StoredProcAttributes.Name("TenantName")]
        public string TenantName { get; set; }

        [StoredProcAttributes.Name("RecordCreatedDateTime")]
        public DateTime RecordCreatedDateTime { get; set; }
    }
}
