using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.IntegrationTests.Functions
{
    //internal class ScalarValueFunctionWithParameterAndReturn
    //    : SqlFunctionBase<
    //        List<ScalarValueFunctionWithParameterAndReturn.Return>,
    //        ScalarValueFunctionWithParameterAndReturn.Parameter>
    //{
    //    public ScalarValueFunctionWithParameterAndReturn(Parameter parameters)
    //        : base(parameters)
    //    { }

    //    internal class Parameter
    //    {
    //        public int Value1 { get; set; }
    //    }

    //    internal class Return
    //    {
    //        public int Value1 { get; set; }
    //    }
    //}

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

        internal class Return
        {
            public int Value1 { get; set; }
        }
    }
}