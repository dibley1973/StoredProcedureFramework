using Dibware.StoredProcedureFrameworkForEF.Base;
using System.Data.Entity;

namespace Dibware.StoredProcedureFramework.Tests.Examples.StoredProcedures
{
    internal class MostBasicStoredProcedureForEf
        : NoParametersNoReturnTypeStoredProcedureBaseForEf
    {
        public MostBasicStoredProcedureForEf(DbContext context)
            : base(context)
        { }
    }
}
