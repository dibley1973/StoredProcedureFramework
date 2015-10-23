using Dibware.StoredProcedureFrameworkForEF.Base;
using System.Data.Entity;
using Dibware.StoredProcedureFramework.StoredProcedureAttributes;

namespace Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures
{
    internal class NormalStoredProcedureForEF
        : StoredProcedureBaseForEF<NormalStoredProcedureResultSet, NormalStoredProcedureParameters>
    {
        public NormalStoredProcedureForEF(DbContext context)
            : base(context, null)
        {
        }
    }

    [Name("NormalStoredProcedureForEF")]
    internal class AnonParamNormalStoredProcedureForEF
       : StoredProcedureBaseForEF<NormalStoredProcedureResultSet, object>
    {
        public AnonParamNormalStoredProcedureForEF(DbContext context)
            : base(context, null)
        {
        }
    }
}