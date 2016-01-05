using System.Collections.Generic;
using System.Data.Entity;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFrameworkForEF.Base;

namespace Dibware.StoredProcedureFramework.Examples.StoredProcedures.EfSpecific
{
    //[Schema("app")]
    internal class AccountGetAllForCompanyId
        : StoredProcedureBaseForEf<List<AccountDto>, CompanyIdParameters>
    {
        public AccountGetAllForCompanyId(DbContext context)
            : base(context, null)
        {
        }
    }
}