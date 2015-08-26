using System;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets
{
    /// <summary>
    /// Defines meta data for the TenantResultRow
    /// </summary>
    public class TenantResultRowMetaData
    {
        [Name("TenantId")]
        public int TenantId { get; set; }

        [Name("IsActive")]
        public bool IsActive { get; set; }

        [Name("TenantName")]
        public string TenantName { get; set; }

        [Name("RecordCreatedDateTime")]
        public DateTime RecordCreatedDateTime { get; set; }
    }
}
