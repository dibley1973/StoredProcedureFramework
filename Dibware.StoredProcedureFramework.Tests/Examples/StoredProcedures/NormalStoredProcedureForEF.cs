using System.Data.Entity;
using Dibware.StoredProcedureFrameworkForEF;
using Dibware.StoredProcedureFrameworkForEF.Base;

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
}
