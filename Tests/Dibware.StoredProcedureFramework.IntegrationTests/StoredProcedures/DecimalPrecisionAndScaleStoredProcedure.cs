﻿using System.Collections.Generic;
using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

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
            [DbType(SqlDbType.Decimal)]
            public decimal Value1 { get; set; }

            [Precision(7)]
            [Scale(1)]
            [DbType(SqlDbType.Decimal)]
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