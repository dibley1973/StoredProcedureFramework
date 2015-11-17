using Dibware.StoredProcedureFramework.Base;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.NullValueParameter
{
    internal class StringParameterStoredProcedure
        : StoredProcedureBase<List<StringParameterStoredProcedure.StringParameterStoredProcedureReturnType>, StringParameterStoredProcedure.StringParameterStoredProcedureParameters>
    {
        public StringParameterStoredProcedure(StringParameterStoredProcedure.StringParameterStoredProcedureParameters parameters)
            : base(parameters)
        {
        }

        public class StringParameterStoredProcedureParameters
        {
            public string Parameter1 { get; set; }
        }

        public class StringParameterStoredProcedureReturnType
        {
            public string Value1 { get; set; }
        }
    }
}
