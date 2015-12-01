using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Data;

namespace Dibware.StoredProcedureFramework.Tests.UnitTests.StoredProcedures
{
    internal class OutputParameterStoredProcedure
        : NoReturnTypeStoredProcedureBase<OutputParameterStoredProcedure.Parameter>
    {
        public OutputParameterStoredProcedure(OutputParameterStoredProcedure.Parameter parameters)
            : base(parameters)
        {
        }

        public class Parameter
        {
            [Size(100)]
            public string Value1 { get; set; }

            [Direction(ParameterDirection.Output)]
            public int Value2 { get; set; }
        }
    }
}