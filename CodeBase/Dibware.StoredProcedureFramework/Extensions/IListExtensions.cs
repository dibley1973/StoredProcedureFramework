using System;
using System.Collections;

namespace Dibware.StoredProcedureFramework.Extensions
{
    public static class IListExtensions
    {
        public static Type GetUnderlyingType(this IList instance)
        {
            Type listInstanceType = instance.GetType();
            Type listUnderlyingType = listInstanceType.GetListTypeUnderlyingType();
            return listUnderlyingType;
        }
    }
}