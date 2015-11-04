using System;

namespace Dibware.StoredProcedureFramework.Examples.Dtos
{
    /// <summary>
    /// Encapsulates tenant data
    /// </summary>
    public class TenantDto
    {
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        public string TenantName { get; set; }
        public DateTime RecordCreatedDateTime { get; set; }
    }
}