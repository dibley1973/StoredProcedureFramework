using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFrameworkForEF.Base;
using System.Collections.Generic;
using System.Data.Entity;

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