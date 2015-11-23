using System;

namespace Dibware.StoredProcedureFramework.Examples.Dtos
{
    /// <summary>
    /// Encapsulates compnay data
    /// </summary>
    internal class CompanyDto
    {
        public int CompanyID { get; set; }
        public int TenantID { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; }
        public DateTime RecordCreatedDateTime { get; set; }
    }
}