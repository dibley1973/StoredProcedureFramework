using System.Collections.Generic;
using Dibware.StoredProcedureFramework.Base;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class DecimalWrongReturnTestStoredProcedure
        : NoParametersStoredProcedureBase<List<DecimalWrongReturnTestStoredProcedure.Return>>
    {
        internal class Return
        {
            [Size(255)]
            //[ParameterDbType(SqlDbType.VarChar)]
            public string Value1 { get; set; }

            //[ParameterDbType(SqlDbType.Bit)]
            public bool Active { get; set; }
        }
    }
}
