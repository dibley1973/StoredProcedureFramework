using Dibware.StoredProcedureFrameworkForEF.Base;
using System.Data.Entity;

namespace Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures
{
    internal class MostBasicStoredProcedureForEF
        : NoParametersNoReturnTypeStoredProcedureBaseForEF
    {
        public MostBasicStoredProcedureForEF(DbContext context)
            : base(context)
        {}
    }
}
