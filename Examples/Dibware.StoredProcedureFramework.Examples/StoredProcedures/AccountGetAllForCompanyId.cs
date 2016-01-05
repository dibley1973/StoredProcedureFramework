using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Examples.StoredProcedures
{
    [Schema("app")]
    internal class AccountGetAllForCompanyId
        : StoredProcedureBase<List<AccountDto>, CompanyIdParameters>
    {
        public AccountGetAllForCompanyId(CompanyIdParameters parameters)
            : base(parameters)
        {
        }
    }
}