using System.Collections.Generic;
using System.Data;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class IncorrectParameterTypeStoredProcedure
        : StoredProcedureBase<
            List<NullStoredProcedureResult>,
            IncorrectParameterTypeStoredProcedure.Parameter>
    {
        public IncorrectParameterTypeStoredProcedure(Parameter parametersType)
            : base(parametersType)
        { }

        internal class Parameter
        {
            [Size(10)]
            [DbType(SqlDbType.VarChar)]
            public object Value1 { get; set; }

            //[Size(10)]
            [DbType(SqlDbType.Decimal)]
            public object Value2 { get; set; }
        }
    }
}
