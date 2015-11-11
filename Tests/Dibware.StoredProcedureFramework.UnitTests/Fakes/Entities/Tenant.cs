using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dibware.StoredProcedureFramework.Tests.Fakes.Entities
{
    [Table("Tenant", Schema = "app")]
    internal class Tenant
    {
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        [MaxLength(100)]
        public string TenantName { get; set; }
        public DateTime RecordCreatedDateTime { get; set; }
        public List<Company> Companies { get; set; }
    }
}