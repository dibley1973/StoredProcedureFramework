using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Dibware.StoredProcedureFramework.Helpers
{
    public static class DynamicObjectHelper
    {
        public static bool HasProperty(dynamic obj, string name)
        {
            Type objType = obj.GetType();

            if (objType == typeof(ExpandoObject))
            {
                return ((IDictionary<string, object>)obj).ContainsKey(name);
            }

            return objType.GetProperty(name) != null;
        }
    }
}
