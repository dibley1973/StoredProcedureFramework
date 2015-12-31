using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.Functions
{
    internal class ScalarValueFunctionWithParameterAndNullReturn
        : SqlFunctionBase<
            int?,
            ScalarValueFunctionWithParameterAndNullReturn.Parameter>
    {
        public ScalarValueFunctionWithParameterAndNullReturn(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            public int Value1 { get; set; }
        }
    }
}