using System.Data.Entity;

namespace Dibware.StoredProcedureFrameworkForEF.Base
{
    public abstract class AnonymousParameterProcedureBase<TReturn>
        : StoredProcedureBaseForEf<TReturn, object>
        where TReturn : class, new()
    {
        protected AnonymousParameterProcedureBase(DbContext context)
            : base(context, null)
        {

        }
    }
}