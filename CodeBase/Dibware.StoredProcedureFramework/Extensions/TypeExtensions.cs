using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
    }
}
