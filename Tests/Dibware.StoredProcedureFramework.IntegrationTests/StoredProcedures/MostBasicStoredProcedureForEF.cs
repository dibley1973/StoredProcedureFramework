using System.Data.Entity;
using Dibware.StoredProcedureFrameworkForEF.Base;

namespace Dibware.StoredProcedureFramework.IntegrationTests.StoredProcedures
{
    internal class MostBasicStoredProcedureForEf
        : NoParametersNoReturnTypeStoredProcedureBaseForEf
    {
        public MostBasicStoredProcedureForEf(DbContext context)
            : base(context)
        { }
    }
}