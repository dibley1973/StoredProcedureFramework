
using System.Collections.Generic;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures;

namespace Dibware.StoredProcedureFramework.Tests.Integration_Tests.StoredProcedures.PrecisionAndScale
{
    [Schema("app")]
    [Name("DecimalPrecisionAndScale")]
    internal class DecimalPrecisionAndScaleStoredProcedure
        : StoredProcedureBase<DecimalPrecisionAndScaleStoredProcedureResultSet, DecimalPrecisionAndScaleParameters>
    {
        public DecimalPrecisionAndScaleStoredProcedure(DecimalPrecisionAndScaleParameters parameters)
            : base(parameters)
        {

        }
    }

    internal class DecimalPrecisionAndScaleStoredProcedureResultSet
    {
        public List<DecimalPrecisionAndScaleReturnType> RecordSet1 { get; set; }

        public DecimalPrecisionAndScaleStoredProcedureResultSet()
        {
            RecordSet1 = new List<DecimalPrecisionAndScaleReturnType>();
        }
    }
}
