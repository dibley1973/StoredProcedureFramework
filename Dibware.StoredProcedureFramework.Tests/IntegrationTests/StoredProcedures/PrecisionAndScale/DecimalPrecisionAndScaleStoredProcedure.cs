using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.IntegrationTests.StoredProcedures.PrecisionAndScale
{
    [Schema("app")]
    [Name("DecimalPrecisionAndScale")]
    internal class DecimalPrecisionAndScaleStoredProcedure
        : StoredProcedureBase<List<DecimalPrecisionAndScaleReturnType>, DecimalPrecisionAndScaleParameters>
    {
        public DecimalPrecisionAndScaleStoredProcedure(DecimalPrecisionAndScaleParameters parameters)
            : base(parameters)
        {

        }
    }

    //[Schema("app")]
    //[Name("DecimalPrecisionAndScale")]
    //internal class DecimalPrecisionAndScaleStoredProcedure
    //    : StoredProcedureBase<DecimalPrecisionAndScaleStoredProcedureResultSet, DecimalPrecisionAndScaleParameters>
    //{
    //    public DecimalPrecisionAndScaleStoredProcedure(DecimalPrecisionAndScaleParameters parameters)
    //        : base(parameters)
    //    {

    //    }
    //}

    //internal class DecimalPrecisionAndScaleStoredProcedureResultSet
    //{
    //    public List<DecimalPrecisionAndScaleReturnType> RecordSet1 { get; set; }

    //    public DecimalPrecisionAndScaleStoredProcedureResultSet()
    //    {
    //        RecordSet1 = new List<DecimalPrecisionAndScaleReturnType>();
    //    }
    //}
}
