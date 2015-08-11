using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dibware.StoredProcedureFramework.Tests.Fakes.Entities
{
    [Table("Company", Schema = "app")]
    internal class Company
    {
        public int CompanyId { get; set; }
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        [MaxLength(100)]
        public string CompanyName { get; set; }
        public DateTime RecordCreatedDateTime { get; set; }
    }
}