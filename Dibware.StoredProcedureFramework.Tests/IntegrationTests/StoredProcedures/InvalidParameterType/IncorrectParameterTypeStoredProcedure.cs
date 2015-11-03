using System.Collections.Generic;
using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.InvalidParameterType
{
    [Schema("app")]
    [Name("DecimalPrecisionAndScale")]
    internal class IncorrectParameterTypeStoredProcedure
        : StoredProcedureBase<List<NullStoredProcedureResult>, IncorrectParameterTypeStoredProcedureParameters>
    {
        public IncorrectParameterTypeStoredProcedure(IncorrectParameterTypeStoredProcedureParameters parametersType)
            : base(parametersType)
        {
        }
    }

    
    //[Schema("app")]
    //[Name("DecimalPrecisionAndScale")]
    //internal class IncorrectParameterTypeStoredProcedure
    //    : StoredProcedureBase<IncorrectParameterTypeStoredProcedureResultSet, IncorrectParameterTypeStoredProcedureParameters>
    //{
    //    public IncorrectParameterTypeStoredProcedure(IncorrectParameterTypeStoredProcedureParameters parametersType)
    //        : base(parametersType)
    //    {
    //    }
    //}

    internal class IncorrectParameterTypeStoredProcedureParameters
    {
        [Size(10)]
        [ParameterDbType(SqlDbType.VarChar)]
        public object Value1 { get; set; }

        //[Size(10)]
        [ParameterDbType(SqlDbType.Decimal)]
        public object Value2 { get; set; }
    }

    //internal class IncorrectParameterTypeStoredProcedureResultSet
    //{
    //    public List<NullStoredProcedureResult> RecordSet1 { get; set; }

    //    public IncorrectParameterTypeStoredProcedureResultSet()
    //    {
    //        RecordSet1 = new List<NullStoredProcedureResult>();
    //    }
    //}
}
