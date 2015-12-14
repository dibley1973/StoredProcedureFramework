using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.IntegrationTests.Functions
{
    internal class ScalarValueFunctionWithParameterAndReturn
        : SqlFunctionBase<
            int,
            ScalarValueFunctionWithParameterAndReturn.Parameter>
    {
        public ScalarValueFunctionWithParameterAndReturn(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            public int Value1 { get; set; }
        }
    }
}