using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class DoublesAndDecimalsTestStoredProcedure
        : NoParametersStoredProcedureBase<List<DoublesAndDecimalsTestStoredProcedure.Return>>
    {
        internal class Return
        {
            public double Value1 { get; set; }

            [Precision(5)]
            [Scale(2)]
            public decimal Value2 { get; set; }

            public double Value3 { get; set; }

            [Precision(5)]
            [Scale(2)]
            public decimal Value4 { get; set; }
        }
    }
}