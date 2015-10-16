using System.Data.Entity;
using Dibware.StoredProcedureFrameworkForEF;

namespace Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures
{
    internal class NormalStoredProcedureForEF
        : StoredProcedureBaseForEF<NormalStoredProcedureResultSet, NormalStoredProcedureParameters>
    {
        public NormalStoredProcedureForEF(DbContext context, 
            NormalStoredProcedureParameters parameters)
            : base(context, parameters)
        {
        }
    }
}
