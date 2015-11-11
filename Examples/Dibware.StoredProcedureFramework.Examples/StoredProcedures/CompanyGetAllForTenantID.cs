using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.Examples.Dtos;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Examples.StoredProcedures
{
    [Schema("app")]
    internal class CompanyGetAllForTenantID
        : StoredProcedureBase<List<CompanyDto>, CompanyGetAllForTenantID.TenantIdParameters>
    {
        public CompanyGetAllForTenantID(TenantIdParameters parameters)
            : base(parameters)
        {
            
        }

        public class TenantIdParameters
        {
            public int TenantId { get; set; }
        }
    }
}
