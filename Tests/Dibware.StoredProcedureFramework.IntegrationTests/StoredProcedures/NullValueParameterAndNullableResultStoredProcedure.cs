﻿using System.Collections.Generic;
using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    [Name("NullValueParameterAndResult")]
    internal class NullValueParameterAndNullableResultStoredProcedure
        : StoredProcedureBase<
            List<NullValueParameterAndNullableResultStoredProcedure.Return>, 
            NullValueParameterAndNullableResultStoredProcedure.Parameter>
    {
        public NullValueParameterAndNullableResultStoredProcedure(Parameter parameters)
            : base(parameters)
        {}

        internal class Parameter
        {
            [DbType(SqlDbType.Int)]
            public int? Value1 { get; set; }

            [DbType(SqlDbType.Int)]
            public int? Value2 { get; set; }
        }

        internal class Return
        {
            public int? Value1 { get; set; }
            public int? Value2 { get; set; }
        }
    }
}
