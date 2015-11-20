using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System.Collections.Generic;
using System.Data;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class DecimalPrecisionAndScaleStoredProcedure
        : StoredProcedureBase<List<DecimalPrecisionAndScaleStoredProcedure.Return>, DecimalPrecisionAndScaleStoredProcedure.Parameter>
    {
        public DecimalPrecisionAndScaleStoredProcedure(Parameter parameters)
            : base(parameters)
        { }

        internal class Parameter
        {
            [Precision(10)]
            [Scale(3)]
            [ParameterDbType(SqlDbType.Decimal)]
            public decimal Value1 { get; set; }

            [Precision(7)]
            [Scale(1)]
            [ParameterDbType(SqlDbType.Decimal)]
            public decimal Value2 { get; set; }
        }

        internal class Return
        {
            [Precision(10)]
            [Scale(3)]
            public decimal Value1 { get; set; }

            [Precision(10)]
            [Scale(3)]
            public decimal Value2 { get; set; }
        }
    }
}