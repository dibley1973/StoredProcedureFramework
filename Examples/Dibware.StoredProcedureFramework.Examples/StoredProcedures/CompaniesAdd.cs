using System.Collections.Generic;
using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Examples.StoredProcedures
{
    [Schema("app")]
    internal class CompaniesAdd
        : NoReturnTypeStoredProcedureBase<CompaniesAdd.CompaniesAddParameters>
    {
        public CompaniesAdd(CompaniesAddParameters parameters)
            : base(parameters)
        {
        }
        internal class CompaniesAddParameters
        {
            [DbType(SqlDbType.Structured)]
            public List<CompanyTableType> Companies { get; set; }
        }

        internal class CompanyTableType
        {
            public int TenantId { get; set; }
            public bool IsActive { get; set; }
            public string CompanyName { get; set; }
        }
    }
}