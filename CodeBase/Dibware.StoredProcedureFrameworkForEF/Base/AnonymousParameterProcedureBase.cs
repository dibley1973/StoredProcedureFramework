using System.Data.Entity;
using Dibware.StoredProcedureFramework;

namespace Dibware.StoredProcedureFrameworkForEF.Base
{
    public abstract class AnonymousParameterProcedureBase<TReturn>
        : StoredProcedureBaseForEf<TReturn, object>
        where TReturn : class, new()
    {
        protected AnonymousParameterProcedureBase(DbContext context)
            : base(context)
        {

        }
    }

    public abstract class AnonymousParameterProcedureBase
        : StoredProcedureBaseForEf<NullStoredProcedureResult, object>
    {
        protected AnonymousParameterProcedureBase(DbContext context)
            : base(context)
        {

        }
    }
}