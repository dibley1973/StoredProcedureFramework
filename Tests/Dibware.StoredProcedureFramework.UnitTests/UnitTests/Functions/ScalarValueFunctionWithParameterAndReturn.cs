using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Functions
{
    internal class ScalarValueFunctionWithParameterAndReturn
        : SqlFunctionBase<
            int,
            ScalarValueFunctionWithParameterAndReturn.Parameter>
    {
        public ScalarValueFunctionWithParameterAndReturn(Parameter parameters)
            : base(parameters)
        { }

        public ScalarValueFunctionWithParameterAndReturn(
             string sqlFunctionName, Parameter parameters)
            : base(sqlFunctionName, parameters)
        { }

        internal class Parameter
        {
            public int Value1 { get; set; }
        }
    }
}
