using System;
using System.Collections;
using Dibware.StoredProcedureFramework.Helpers;

namespace Dibware.StoredProcedureFramework.Extensions
{
    public static class IListExtensions
    {
        public static Type GetUnderlyingType(this IList instance)
        {
            if (instance == null) throw new NullReferenceException();

            var a = new IListTypeDefinitionFinder(instance);
            var listUnderlyingType = a.GenericListType;

            return listUnderlyingType;
        }
    }
}