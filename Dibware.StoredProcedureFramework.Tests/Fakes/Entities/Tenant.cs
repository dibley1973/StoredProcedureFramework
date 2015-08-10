
using System;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Tests.Fakes.Entities
{
    internal class Tenant
    {
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        public string TenantName { get; set; }
        public DateTime RecordCreatedDateTime { get; set; }
        public List<Company> Companies { get; set; }
    }
}