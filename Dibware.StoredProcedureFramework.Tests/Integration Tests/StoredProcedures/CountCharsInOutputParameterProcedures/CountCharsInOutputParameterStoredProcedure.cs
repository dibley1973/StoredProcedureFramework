
using System.Collections.Generic;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.NullValueParameter;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.CountCharsInOutputParameterProcedures
{
    [Schema("app")]
    [Name("CountCharsInOutputParameter")]
    internal class CountCharsInOutputParameterStoredProcedure
        : StoredProcedureBase<NullValueParameterNullableResultSet, CountCharsInOutputParameterParameters>
    {
        public CountCharsInOutputParameterStoredProcedure(CountCharsInOutputParameterParameters parameters)
            : base(parameters)
        {
        }
    }


    internal class NullValueParameterNullableResultSet
    {
        public List<NullValueParameterNullableReturnType> RecordSet1 { get; set; }

        public NullValueParameterNullableResultSet()
        {
            RecordSet1 = new List<NullValueParameterNullableReturnType>();
        }
    }
}