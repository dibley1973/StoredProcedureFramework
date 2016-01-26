using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Data;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class CountCharsInReturnParameterStoredProcedure
        : NoReturnTypeStoredProcedureBase<CountCharsInReturnParameterStoredProcedure.Parameter>
    {
        public CountCharsInReturnParameterStoredProcedure(Parameter parameters)
            : base(parameters)
        {
        }

        public class Parameter
        {
            [Size(100)]
            public string Value1 { get; set; }

            [Direction(ParameterDirection.ReturnValue)]
            public int Value2 { get; set; }
        }
    }
}