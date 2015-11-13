using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.TableValueParameter
{
    [Schema("app")]
    internal class CompaniesAdd
        : NoReturnTypeStoredProcedureBase<CompaniesAddParameters>
    {
        public CompaniesAdd(CompaniesAddParameters parameters)
            : base(parameters)
        {
        }
    }

    internal class CompaniesAddParameters
    {
        [ParameterDbType(SqlDbType.Structured)]
        public List<CompanyTableType> Companies { get; set; }
    }

    internal class CompanyTableType
    {
        public int TenantId { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; }
    }
}
