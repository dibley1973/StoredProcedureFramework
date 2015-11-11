using System;
using System.ComponentModel.DataAnnotations;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.ResultSets.TenantResultSets
{
    [MetadataType(typeof(TenantResultRowMetaData))]
    public class TenantResultRow
    {
        public int TenantId { get; set; }

        public bool IsActive { get; set; }

        public string TenantName { get; set; }

        public DateTime RecordCreatedDateTime { get; set; }
    }
}
