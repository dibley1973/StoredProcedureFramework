using Dibware.StoredProcedureFramework;
using Dibware.StoredProcedureFrameworkForEF.Base;
using System.Data.Entity;

namespace Dibware.StoredProcedureFrameworkForEF.Generic
{
    public class StoredProcedure
        : AnonymousParameterProcedureBase<NullStoredProcedureResult>
    {
        public StoredProcedure(DbContext context)
            : base(context)
        { }
    }

    public class StoredProcedure<TReturn>
        : AnonymousParameterProcedureBase<TReturn>
        where TReturn : class, new()
    {
        public StoredProcedure(DbContext context)
            : base(context)
        { }
    }

    public class StoredProcedure<TReturn, TParameters>
        : StoredProcedureBaseForEf<TReturn, TParameters>
        where TReturn : class, new()
        where TParameters : class, new()
    {
        public StoredProcedure(DbContext context)
            : base(context, null)
        { }
    }
}