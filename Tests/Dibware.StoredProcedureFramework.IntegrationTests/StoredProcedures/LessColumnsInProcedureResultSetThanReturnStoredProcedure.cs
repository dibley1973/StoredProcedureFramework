using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class LessColumnsInProcedureResultSetThanReturnStoredProcedure
        : NoParametersStoredProcedureBase<
            List<LessColumnsInProcedureResultSetThanReturnStoredProcedure.Return>>
    {

        internal class Return
        {
            [Precision(9)]
            [Scale(3)]
            public decimal Value1 { get; set; }

            [Precision(15)]
            [Scale(2)]
            public decimal Value2 { get; set; }

            [Precision(5)]
            [Scale(2)]
            public decimal Value3 { get; set; }

            [Precision(18)]
            [Scale(6)]
            public decimal Value4 { get; set; }
        }
    }
}