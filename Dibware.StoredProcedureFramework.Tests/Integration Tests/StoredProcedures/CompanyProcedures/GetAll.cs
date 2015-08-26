﻿using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.ResultSets.CompanyResultSets;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.CompanyProcedures
{
    // for schema setting 
    // http://www.codeproject.com/Articles/179481/Code-First-Stored-Procedures?msg=4746061#xx4746061xx
    [Schema("app")]
    [Name("Company_GetAll")]
    [ReturnType(typeof(CompanyResultRow))]
    internal class GetAll
    {
    }
}
