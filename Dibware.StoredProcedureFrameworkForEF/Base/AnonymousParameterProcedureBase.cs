using System.Data.Entity;

namespace Dibware.StoredProcedureFrameworkForEF.Base
{
    public abstract class AnonymousParameterProcedureBase<TReturn>
        : StoredProcedureBaseForEF<TReturn, object>
        where TReturn : class, new()
    {
        public AnonymousParameterProcedureBase(DbContext context)
            : base(context, null)
        {

        }
    }
}