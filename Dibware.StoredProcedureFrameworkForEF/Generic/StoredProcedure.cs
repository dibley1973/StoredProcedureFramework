using Dibware.StoredProcedureFrameworkForEF.Base;
using System.Data.Entity;

namespace Dibware.StoredProcedureFrameworkForEF.Generic
{
    public class StoredProcedure<TReturn>
        : AnonymousParameterProcedureBase<TReturn>
        where TReturn : class, new()
    {
        public StoredProcedure(DbContext context)
            : base(context)
        { }
    }

    //public class StoredProcedure<TReturn, TParameters>
    //    : StoredProcedureBaseForEf<NullStoredProcedureResult, TParameters>
    //    where TReturn : class, new()
    //    where TParameters : class
    //{
    //    public StoredProcedure(DbContext context)
    //        : base(context)
    //    { }
    //}
}