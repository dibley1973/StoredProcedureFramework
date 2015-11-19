using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Data;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    public class CountCharsInOutputParameterStoredProcedure
        : NoReturnTypeStoredProcedureBase<CountCharsInOutputParameterStoredProcedure.Parameter>
    {
        public CountCharsInOutputParameterStoredProcedure(CountCharsInOutputParameterStoredProcedure.Parameter parameters)
            : base(parameters)
        {
        }

        public class Parameter
        {
            [Size(100)]
            //[ParameterDbType(SqlDbType.VarChar)]
            public string Value1 { get; set; }

            //[ParameterDbType(SqlDbType.Int)]
            [Direction(ParameterDirection.Output)]
            public int Value2 { get; set; }
        }
    }
}
