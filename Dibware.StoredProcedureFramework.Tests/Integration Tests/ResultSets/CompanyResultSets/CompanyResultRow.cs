using System;
using System.ComponentModel.DataAnnotations;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.CompanyResultSets
{
    [MetadataType(typeof(CompanyResultRowMetaData))]
    public class CompanyResultRow
    {
        public Guid CompanyID { get; set; }

        public Guid TenantID { get; set; }

        public bool IsActive { get; set; }

        public string CompanyName { get; set; }

        public DateTime RecordCreatedDateTime { get; set; }
    }
}