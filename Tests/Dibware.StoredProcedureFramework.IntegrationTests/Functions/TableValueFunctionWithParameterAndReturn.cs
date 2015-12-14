using Dibware.StoredProcedureFramework.Base;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.Functions
{
    internal class TableValueFunctionWithParameterAndReturn
        : SqlFunctionBase<
            List<TableValueFunctionWithParameterAndReturn.Return>,
            TableValueFunctionWithParameterAndReturn.Parameter>
    {
        public TableValueFunctionWithParameterAndReturn(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            public int Value1 { get; set; }
        }

        internal class Return
        {
            public int Value1 { get; set; }
            public string Value2 { get; set; }
        }
    }
}