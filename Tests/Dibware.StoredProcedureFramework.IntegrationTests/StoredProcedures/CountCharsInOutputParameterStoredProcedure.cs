using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    public class CountCharsInOutputParameterStoredProcedure
        : NoReturnTypeStoredProcedureBase<CountCharsInOutputParameterStoredProcedure.Parameter>
    {
        public CountCharsInOutputParameterStoredProcedure(Parameter parameters)
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