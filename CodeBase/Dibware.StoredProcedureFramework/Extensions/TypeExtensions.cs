using System;
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
    }
}
