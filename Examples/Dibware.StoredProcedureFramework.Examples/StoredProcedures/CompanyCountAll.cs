using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Examples.StoredProcedures
{
    [Schema("app")]
    internal class CompanyCountAll
        : NoParametersStoredProcedureBase<List<CompanyCountAll.Result>>
    {
        internal class Result
        {
            public int CountOfCompanies { get; set; }
        }
    }
}