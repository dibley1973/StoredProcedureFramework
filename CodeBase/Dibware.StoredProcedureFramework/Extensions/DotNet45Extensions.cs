using System;
using System.Collections.Generic;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Extensions
{
    public static class DotNet45Extensions
    {
        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element)
            where T : Attribute
        {
            var attributes = element.GetCustomAttributes(typeof(T), false) as IEnumerable<T>;
            return attributes;
        }
    }
}