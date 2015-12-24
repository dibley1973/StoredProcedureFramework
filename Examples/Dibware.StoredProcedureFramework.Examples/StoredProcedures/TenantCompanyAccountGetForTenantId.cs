using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.Examples.StoredProcedures.Parameters;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Examples.StoredProcedures
{
    [Schema("app")]
    internal class TenantCompanyAccountGetForTenantId
        : StoredProcedureBase<
            TenantCompanyAccountGetForTenantId.TenantCompanyAccountGetForTenantIdResultSet,
            TenantIdParameters>
    {

        public TenantCompanyAccountGetForTenantId(TenantIdParameters parameters)
            : base(parameters)
        { }

        internal class TenantCompanyAccountGetForTenantIdResultSet
        {
            public List<TenantDto> Tenants { get; private set; }
            public List<CompanyDto> Companies { get; private set; }
            public List<AccountDto> Accounts { get; private set; }

            public TenantCompanyAccountGetForTenantIdResultSet()
            {
                Tenants = new List<TenantDto>();
                Companies = new List<CompanyDto>();
                Accounts = new List<AccountDto>();
            }
        }
    }
}