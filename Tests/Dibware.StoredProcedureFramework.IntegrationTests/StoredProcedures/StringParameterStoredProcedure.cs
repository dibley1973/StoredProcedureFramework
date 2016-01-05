using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class StringParameterStoredProcedure
        : StoredProcedureBase<
            List<StringParameterStoredProcedure.Return>,
            StringParameterStoredProcedure.Parameter>
    {
        public StringParameterStoredProcedure(Parameter parameters)
            : base(parameters)
        {
        }

        public class Parameter
        {
            public string Parameter1 { get; set; }
        }

        public class Return
        {
            public string Value1 { get; set; }
        }
    }
}