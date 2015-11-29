using Dibware.StoredProcedureFramework.Base;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.Functions
{
    internal class ScalarValueFunctionWithParameterAndReturn
        : StoredProcedureBase<
            List<ScalarValueFunctionWithParameterAndReturn.Return>,
            ScalarValueFunctionWithParameterAndReturn.Parameter>
    {
        public ScalarValueFunctionWithParameterAndReturn(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            public int Value1 { get; set; }
        }

        internal class Return
        {
            public int Value1 { get; set; }
        }
    }
}