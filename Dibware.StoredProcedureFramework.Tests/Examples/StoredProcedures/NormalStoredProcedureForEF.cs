using System.Collections.Generic;
using Dibware.StoredProcedureFrameworkForEF.Base;
using System.Data.Entity;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures
{
    internal class NormalStoredProcedureForEf
        : StoredProcedureBaseForEf<List<NormalStoredProcedureRecordSet1ReturnType>, NormalStoredProcedureParameters>
    {
        public NormalStoredProcedureForEf(DbContext context)
            : base(context, null)
        {
        }
    }

    [Name("NormalStoredProcedureForEf")]
    internal class AnonParamNormalStoredProcedureForEf
       : StoredProcedureBaseForEf<List<NormalStoredProcedureRecordSet1ReturnType>, object>
    {
        public AnonParamNormalStoredProcedureForEf(DbContext context)
            : base(context, null)
        {
        }
    }
}