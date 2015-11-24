using System;

namespace Dibware.StoredProcedureFramework.Examples.Dtos
{
    /// <summary>
    /// Encapsulates Account data
    /// </summary>
    internal class AccountDto
    {
        public int AccountId { get; set; }
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
        public string AccountName { get; set; }
        public DateTime RecordCreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
    }
}
