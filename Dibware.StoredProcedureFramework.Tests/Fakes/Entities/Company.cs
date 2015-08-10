
using System;

namespace Dibware.StoredProcedureFramework.Tests.Fakes.Entities
{
    internal class Company
    {
        public int CompanyId { get; set; }
        public int TenantID { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; }
        public DateTime RecordCreatedDateTime { get; set; }

    }
}