using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.IntegrationTests.Functions
{
    internal class TableValueFunctionWithoutParameterButWithReturn
        : NoParametersSqlFunctionBase<List<TableValueFunctionWithoutParameterButWithReturn.Return>>
    {
        internal class Return
        {
            public int Value1 { get; set; }
            public string Value2 { get; set; }
        }
    }
}