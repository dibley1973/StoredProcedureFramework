using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.IntegrationTests.Functions
{
    internal class TableValueFunctionWithParameterAndNullableReturn
        : SqlFunctionBase<
            List<TableValueFunctionWithParameterAndNullableReturn.Return>,
            TableValueFunctionWithParameterAndNullableReturn.Parameter>
    {
        public TableValueFunctionWithParameterAndNullableReturn(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            public int Value1 { get; set; }
        }

        internal class Return
        {
            public int? Value1 { get; set; }
            public string Value2 { get; set; }
        }
    }
}