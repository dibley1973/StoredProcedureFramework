using System;
using System.ComponentModel.DataAnnotations;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.TenantResultSets
{
    [MetadataType(typeof(TenantResultRowMetaData))]
    public class TenantResultRow
    {
        public Guid TenantID { get; set; }

        public bool IsActive { get; set; }

        public string TenantName { get; set; }

        public DateTime RecordCreatedDateTime { get; set; }
    }
}
