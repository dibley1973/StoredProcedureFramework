﻿using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;

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