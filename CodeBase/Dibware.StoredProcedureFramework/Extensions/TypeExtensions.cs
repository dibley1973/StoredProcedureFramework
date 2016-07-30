using Dibware.StoredProcedureFramework.StoredProcedureAttributes;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Dibware.StoredProcedureFramework.Extensions
{
    /// <summary>
    /// provides extension methods for the <see cref="System.Type"/>
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Get properties of a type that do not have the 'NotMapped' attribute
        /// </summary>
        /// <param name="instance">Type to examine for properites</param>
        /// <returns>Array of properties that can be filled</returns>
        public static PropertyInfo[] GetMappedProperties(this Type instance)
        {
            var allProperties = instance.GetProperties();
            var mappedProperties = allProperties
                .Where(p => p.GetAttribute<NotMappedAttribute>() == null)
                .Select(p => p);

            return mappedProperties.ToArray();
        }

        /// <summary>
        /// Gets a value indicating if the instance implementses the ICollection interface.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">instance</exception>
        public static bool ImplementsICollectionInterface(this Type instance)
        {
            if (instance == null) throw new ArgumentNullException("instance");

            foreach (Type @interface in instance.GetInterfaces())
            {
                if (@interface.IsGenericType)
                {
                    if (@interface.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsGenericType(this Type instance)
        {
            return instance.GetGeneGetGenricArgumentCount() > 0;
        }

        public static int GetGeneGetGenricArgumentCount(this Type instance)
        {
            return instance.GetGenericArguments().Length;
        }

        public static bool IsDynamicType(this Type instance)
        {
            var isDynamic = instance == typeof(ExpandoObject);
            return isDynamic;
        }

        public static bool IsGenericTypeWithFirstDynamicTypeArgument(this Type instance)
        {
            if (instance.GetGeneGetGenricArgumentCount() != 1) return false;

            var genericArgument = instance.GetGenericArguments()[0];
            var isDynamic = genericArgument.IsDynamicType();

            return isDynamic;
        }
    }
}
